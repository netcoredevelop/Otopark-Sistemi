using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly IBranchService _branchService;
    private readonly ITransactionService _transactionService;

    public StatisticsController(
        IVehicleService vehicleService, 
        IBranchService branchService,
        ITransactionService transactionService)
    {
        _vehicleService = vehicleService;
        _branchService = branchService;
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStatistics([FromQuery] int? branchId = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
    {
        var today = DateTime.Today;
        var vehicles = await _vehicleService.GetAllAsync();
        if (branchId.HasValue)
            vehicles = vehicles.Where(v => v.BranchId == branchId.Value).ToList();
        if (startDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date >= startDate.Value).ToList();
        if (endDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date <= endDate.Value).ToList();
        var dailyVehicles = vehicles.Where(v => v.EntryDate.Date == today);
        var activeVehicles = vehicles.Where(v => v.ExitDate == null);
        long? capacity = null;
        if (branchId.HasValue)
        {
            var branch = await _branchService.GetByIdAsync(branchId.Value);
            capacity = branch?.Capacity;
        }
        else
        {
            capacity = vehicles.Select(v => v.Branch).Distinct().Sum(b => b.Capacity ?? 0);
        }
        var occupancyRate = (capacity > 0) ? (activeVehicles.Count() * 100 / capacity.Value) : 0;

        // Get today's transactions
        var todayTransactions = await _transactionService.GetTransactionsByDateRangeAsync(today, today.AddDays(1).AddSeconds(-1));
        if (branchId.HasValue)
        {
            todayTransactions = todayTransactions.Where(t => t.BranchId == branchId.Value);
        }

        // Calculate daily income and expenses
        var dailyIncome = todayTransactions
            .Where(t => t.TransactionCategory.Type == TransactionType.Gelir)
            .Sum(t => t.Amount);

        var dailyExpense = todayTransactions
            .Where(t => t.TransactionCategory.Type == TransactionType.Gider)
            .Sum(t => t.Amount);

        var dailyNetEarnings = dailyIncome - dailyExpense;

        var statistics = new
        {
            dailyEntryCount = dailyVehicles.Count(),
            dailyIncome = dailyNetEarnings,
            activeVehicleCount = activeVehicles.Count(),
            occupancyRate = occupancyRate
        };
        return Ok(statistics);
    }

    [HttpGet("weekly-entries")]
    public async Task<IActionResult> GetWeeklyEntries([FromQuery] int? branchId = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
    {
        var vehicles = await _vehicleService.GetAllAsync();
        if (branchId.HasValue)
            vehicles = vehicles.Where(v => v.BranchId == branchId.Value).ToList();
        if (startDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date >= startDate.Value).ToList();
        if (endDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date <= endDate.Value).ToList();
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1); // Pazartesi
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
    public async Task<IActionResult> GetIncomeDistribution([FromQuery] int? branchId = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
    {
        var vehicles = await _vehicleService.GetAllAsync();
        if (branchId.HasValue)
            vehicles = vehicles.Where(v => v.BranchId == branchId.Value).ToList();
        if (startDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date >= startDate.Value).ToList();
        if (endDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date <= endDate.Value).ToList();
        var today = DateTime.Today;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);
        var hourlyIncome = vehicles.Where(v => v.EntryDate >= today.AddHours(-24)).Sum(v => v.MuhammenBedeli ?? 0);
        var dailyIncome = vehicles.Where(v => v.EntryDate.Date == today).Sum(v => v.MuhammenBedeli ?? 0);
        var monthlyIncome = vehicles.Where(v => v.EntryDate >= startOfMonth).Sum(v => v.MuhammenBedeli ?? 0);
        return Ok(new
        {
            labels = new[] { "Saatlik", "Günlük", "Aylık" },
            data = new[] { hourlyIncome, dailyIncome, monthlyIncome }
        });
    }

    [HttpGet("active-vehicles-by-branch")]
    public async Task<IActionResult> GetActiveVehiclesByBranch([FromQuery] int? branchId = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
    {
        var vehicles = await _vehicleService.GetAllAsync();
        if (branchId.HasValue)
            vehicles = vehicles.Where(v => v.BranchId == branchId.Value).ToList();
        if (startDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date >= startDate.Value).ToList();
        if (endDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date <= endDate.Value).ToList();
        var grouped = vehicles
            .Where(v => v.ExitDate == null)
            .GroupBy(v => v.Branch.Name)
            .Select(g => new { Branch = g.Key, Count = g.Count() })
            .ToList();
        return Ok(new {
            labels = grouped.Select(x => x.Branch).ToArray(),
            data = grouped.Select(x => x.Count).ToArray()
        });
    }

    [HttpGet("monthly-entries")]
    public async Task<IActionResult> GetMonthlyEntries([FromQuery] int? branchId = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
    {
        var vehicles = await _vehicleService.GetAllAsync();
        if (branchId.HasValue)
            vehicles = vehicles.Where(v => v.BranchId == branchId.Value).ToList();
        if (startDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date >= startDate.Value).ToList();
        if (endDate.HasValue)
            vehicles = vehicles.Where(v => v.EntryDate.Date <= endDate.Value).ToList();
        var now = DateTime.Now;
        var months = Enumerable.Range(0, 12)
            .Select(i => now.AddMonths(-i))
            .Reverse()
            .ToList();
        var labels = months.Select(m => m.ToString("yyyy-MM")).ToArray();
        var entries = months.Select(m => vehicles.Count(v => v.EntryDate.Year == m.Year && v.EntryDate.Month == m.Month)).ToArray();
        var exits = months.Select(m => vehicles.Count(v => v.ExitDate.HasValue && v.ExitDate.Value.Year == m.Year && v.ExitDate.Value.Month == m.Month)).ToArray();
        return Ok(new {
            labels,
            entries,
            exits
        });
    }

    private double CalculateOccupancyRate(int activeVehicles)
    {
        const int totalParkingSpaces = 100; // Toplam otopark kapasitesi
        return Math.Round((double)activeVehicles / totalParkingSpaces * 100, 1);
    }
} 