using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public StatisticsController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStatistics()
    {
        var today = DateTime.Today;
        var vehicles = await _vehicleService.GetAllAsync();
        
        var dailyVehicles = vehicles.Where(v => v.EntryDate.Date == today);
        var activeVehicles = vehicles.Where(v => v.ExitDate == null);
        
        var statistics = new
        {
            dailyEntryCount = dailyVehicles.Count(),
            dailyIncome = dailyVehicles.Sum(v => v.MuhammenBedeli),
            activeVehicleCount = activeVehicles.Count(),
            occupancyRate = CalculateOccupancyRate(activeVehicles.Count())
        };

        return Ok(statistics);
    }

    [HttpGet("weekly-entries")]
    public async Task<IActionResult> GetWeeklyEntries()
    {
        var vehicles = await _vehicleService.GetAllAsync();
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1); // Pazartesi günü

        var weeklyData = new int[7];
        for (int i = 0; i < 7; i++)
        {
            var currentDate = startOfWeek.AddDays(i);
            weeklyData[i] = vehicles.Count(v => v.EntryDate.Date == currentDate);
        }

        return Ok(new
        {
            labels = new[] { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" },
            data = weeklyData
        });
    }

    [HttpGet("income-distribution")]
    public async Task<IActionResult> GetIncomeDistribution()
    {
        var vehicles = await _vehicleService.GetAllAsync();
        var today = DateTime.Today;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);

        // Saatlik gelir (son 24 saat)
        var hourlyIncome = vehicles
            .Where(v => v.EntryDate >= today.AddHours(-24))
            .Sum(v => v.MuhammenBedeli);

        // Günlük gelir (bugün)
        var dailyIncome = vehicles
            .Where(v => v.EntryDate.Date == today)
            .Sum(v => v.MuhammenBedeli);

        // Aylık gelir (bu ay)
        var monthlyIncome = vehicles
            .Where(v => v.EntryDate >= startOfMonth)
            .Sum(v => v.MuhammenBedeli);

        return Ok(new
        {
            labels = new[] { "Saatlik", "Günlük", "Aylık" },
            data = new[] { hourlyIncome, dailyIncome, monthlyIncome }
        });
    }

    private double CalculateOccupancyRate(int activeVehicles)
    {
        const int totalParkingSpaces = 100; // Toplam otopark kapasitesi
        return Math.Round((double)activeVehicles / totalParkingSpaces * 100, 1);
    }
} 