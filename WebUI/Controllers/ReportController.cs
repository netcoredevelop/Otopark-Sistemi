using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Common;
using Domain.Entities;
using Application.Interfaces;
using ClosedXML.Excel;
using System.Data;

namespace WebUI.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IBranchService _branchService;
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IVehicleBrandService _vehicleBrandService;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleColorService _vehicleColorService;

        public ReportController(
            IVehicleService vehicleService,
            IBranchService branchService,
            IVehicleTypeService vehicleTypeService,
            IVehicleBrandService vehicleBrandService,
            IVehicleModelService vehicleModelService,
            IVehicleColorService vehicleColorService)
        {
            _vehicleService = vehicleService;
            _branchService = branchService;
            _vehicleTypeService = vehicleTypeService;
            _vehicleBrandService = vehicleBrandService;
            _vehicleModelService = vehicleModelService;
            _vehicleColorService = vehicleColorService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Branches = await _branchService.GetAllAsync();
            ViewBag.VehicleTypes = await _vehicleTypeService.GetAllAsync();
            ViewBag.VehicleBrands = await _vehicleBrandService.GetAllAsync();
            ViewBag.VehicleModels = await _vehicleModelService.GetAllAsync();
            ViewBag.VehicleColors = await _vehicleColorService.GetAllAsync();

            return View();
        }

        public async Task<IActionResult> ExportToExcel(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            int? vehicleTypeId = null,
            int? vehicleBrandId = null,
            int? vehicleModelId = null,
            int? vehicleColorId = null)
        {
            var vehicles = await _vehicleService.GetVehiclesForReportAsync(
                startDate,
                endDate,
                branchId,
                vehicleTypeId,
                vehicleBrandId,
                vehicleModelId,
                vehicleColorId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Araç Raporu");

                // Başlıkları ekle
                worksheet.Cell(1, 1).Value = "Plaka";
                worksheet.Cell(1, 2).Value = "Araç Sahibi";
                worksheet.Cell(1, 3).Value = "TC Kimlik No";
                worksheet.Cell(1, 4).Value = "Ruhsat Seri No";
                worksheet.Cell(1, 5).Value = "Bağlama Ekip No";
                worksheet.Cell(1, 6).Value = "Giriş Tarihi";
                worksheet.Cell(1, 7).Value = "Giriş Saati";
                worksheet.Cell(1, 8).Value = "Çıkış Tarihi";
                worksheet.Cell(1, 9).Value = "Çıkış Saati";
                worksheet.Cell(1, 10).Value = "Şube";
                worksheet.Cell(1, 11).Value = "Araç Tipi";
                worksheet.Cell(1, 12).Value = "Araç Markası";
                worksheet.Cell(1, 13).Value = "Araç Modeli";
                worksheet.Cell(1, 14).Value = "Araç Yılı";
                worksheet.Cell(1, 15).Value = "Araç Rengi";
                worksheet.Cell(1, 16).Value = "Anahtar Konumu";
                worksheet.Cell(1, 17).Value = "Bağlama Bölgesi";
                worksheet.Cell(1, 18).Value = "Bağlama Nedeni";
                worksheet.Cell(1, 19).Value = "Park Yeri";
                worksheet.Cell(1, 20).Value = "Muhammen Bedeli";
                worksheet.Cell(1, 21).Value = "Bağlama Ek Bilgisi";
                worksheet.Cell(1, 22).Value = "Açıklama";

                // Verileri ekle
                int row = 2;
                foreach (var vehicle in vehicles)
                {
                    worksheet.Cell(row, 1).Value = vehicle.PlateNumber;
                    worksheet.Cell(row, 2).Value = vehicle.VehicleOnwer;
                    worksheet.Cell(row, 3).Value = vehicle.VehicleOnwerIdentityNumber;
                    worksheet.Cell(row, 4).Value = vehicle.BookSequenceNo;
                    worksheet.Cell(row, 5).Value = vehicle.LinkingTeamNumber;
                    worksheet.Cell(row, 6).Value = vehicle.EntryDate.ToString("dd.MM.yyyy");
                    worksheet.Cell(row, 7).Value = vehicle.EntryDate.ToString("HH:mm");
                    worksheet.Cell(row, 8).Value = vehicle.ExitDate?.ToString("dd.MM.yyyy") ?? "-";
                    worksheet.Cell(row, 9).Value = vehicle.ExitDate?.ToString("HH:mm") ?? "-";
                    worksheet.Cell(row, 10).Value = vehicle.Branch?.Name;
                    worksheet.Cell(row, 11).Value = vehicle.VehicleType?.Name;
                    worksheet.Cell(row, 12).Value = vehicle.VehicleBrand?.Name;
                    worksheet.Cell(row, 13).Value = vehicle.VehicleModel?.Name;
                    worksheet.Cell(row, 14).Value = vehicle.VehicleYear?.Name;
                    worksheet.Cell(row, 15).Value = vehicle.VehicleColor?.Name;
                    worksheet.Cell(row, 16).Value = vehicle.KeyLocation?.Name;
                    worksheet.Cell(row, 17).Value = vehicle.LinkingRegion?.Name;
                    worksheet.Cell(row, 18).Value = vehicle.LinkingReason?.Name;
                    worksheet.Cell(row, 19).Value = vehicle.ParkLocation?.Name;
                    worksheet.Cell(row, 20).Value = vehicle.MuhammenBedeli?.ToString("N0") + " TL";
                    worksheet.Cell(row, 21).Value = vehicle.LinkingAdditionalInformation;
                    worksheet.Cell(row, 22).Value = vehicle.Description;
                    row++;
                }

                // Sütun genişliklerini ayarla
                worksheet.Columns().AdjustToContents();

                // Başlık formatını ayarla
                var headerRange = worksheet.Range(1, 1, 1, 22);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Excel dosyasını oluştur
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"AracRaporu_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
                    );
                }
            }
        }
    }
} 