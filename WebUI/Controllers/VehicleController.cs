using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using WebUI.Auth;
using WebUI.Models.Vehicle;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Models.DataTable;
using Domain.Common;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Linq;
using System.Linq.Expressions;
using WebUI.Extensions;
using System.Globalization;
using System.ComponentModel;

namespace WebUI.Controllers;

[AuditEntity("Vehicle")]
[ServiceFilter(typeof(AuditLogActionFilter))]
[RoleAuthorization]
public class VehicleController : Controller
{
	private readonly IVehicleService _vehicleService;
	private readonly IBranchService _branchService;
	private readonly IVehicleTypeService _vehicleTypeService;
	private readonly IVehicleBrandService _vehicleBrandService;
	private readonly IVehicleModelService _vehicleModelService;
	private readonly IVehicleYearService _vehicleYearService;
	private readonly IVehicleColorService _vehicleColorService;
	private readonly IKeyLocationService _keyLocationService;
	private readonly ILinkingRegionService _linkingRegionService;
	private readonly ILinkingReasonService _linkingReasonService;
	private readonly IParkLocationService _parkLocationService;
	private readonly IEnforcementOfficeService _enforcementOfficeService;
	private readonly IEnforcementRecordService _enforcementRecordService;
	private readonly IVehicleRepository _vehicleRepository;
	private readonly IWebHostEnvironment _webHostEnvironment;
	private readonly IDocumentService _documentService;
	private readonly IVehicleImageService _vehicleImageService;
	private readonly ITransactionService _transactionService;

	public VehicleController(
		IVehicleService vehicleService,
		IBranchService branchService,
		IVehicleTypeService vehicleTypeService,
		IVehicleBrandService vehicleBrandService,
		IVehicleModelService vehicleModelService,
		IVehicleYearService vehicleYearService,
		IVehicleColorService vehicleColorService,
		IKeyLocationService keyLocationService,
		ILinkingRegionService linkingRegionService,
		ILinkingReasonService linkingReasonService,
		IParkLocationService parkLocationService,
		IEnforcementOfficeService enforcementOfficeService,
		IEnforcementRecordService enforcementRecordService,
		IVehicleRepository vehicleRepository,
		IWebHostEnvironment webHostEnvironment,
		IDocumentService documentService,
		IVehicleImageService vehicleImageService,
		ITransactionService transactionService)
	{
		_vehicleService = vehicleService;
		_branchService = branchService;
		_vehicleTypeService = vehicleTypeService;
		_vehicleBrandService = vehicleBrandService;
		_vehicleModelService = vehicleModelService;
		_vehicleYearService = vehicleYearService;
		_vehicleColorService = vehicleColorService;
		_keyLocationService = keyLocationService;
		_linkingRegionService = linkingRegionService;
		_linkingReasonService = linkingReasonService;
		_parkLocationService = parkLocationService;
		_enforcementOfficeService = enforcementOfficeService;
		_enforcementRecordService = enforcementRecordService;
		_vehicleRepository = vehicleRepository;
		_webHostEnvironment = webHostEnvironment;
		_documentService = documentService;
		_vehicleImageService = vehicleImageService;
		_transactionService = transactionService;
	}

	[RoleAuthorization("Admin,Vehicle_View")]
	public async Task<IActionResult> Index(
		string plateNumber = null,
		string vehicleOwner = null,
		DateTime? startDate = null,
		DateTime? endDate = null,
		int pageIndex = 1,
		int pageSize = 10,
		string sortOrder = "desc")
	{

		try
		{
			var query = _vehicleRepository.GetQueryable();

			// Filtreleme
			if (!string.IsNullOrWhiteSpace(plateNumber))
				query = query.Where(v => v.PlateNumber.Contains(plateNumber));

			if (!string.IsNullOrWhiteSpace(vehicleOwner))
				query = query.Where(v => v.VehicleOnwer.Contains(vehicleOwner));

			if (startDate.HasValue)
			{
				var startDateTime = startDate.Value.Date;
				query = query.Where(v => v.EntryDate.Date >= startDateTime);
			}

			if (endDate.HasValue)
			{
				var endDateTime = endDate.Value.Date.AddDays(1).AddTicks(-1);
				query = query.Where(v => v.EntryDate.Date <= endDateTime);
			}

			// Sıralama
			query = sortOrder.ToLower() == "asc"
				? query.OrderBy(v => v.EntryDate)
				: query.OrderByDescending(v => v.EntryDate);

			// Include işlemleri
			query = query
				.Include(v => v.Branch)
				.Include(v => v.VehicleBrand)
				.Include(v => v.VehicleModel)
				.Include(v => v.ParkLocation)
				.Include(v=>v.Transactions);

			var vehicles = await PaginatedList<Vehicle>.CreateAsync(query, pageIndex, pageSize);

			return View(vehicles);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = "Araçlar listelenirken bir hata oluştu: " + ex.Message;
			return View(new PaginatedList<Vehicle>(new List<Vehicle>(), 0, 1, 10));
		}
	}
	[RoleAuthorization("Admin,Vehicle_View")]
	public async Task<IActionResult> Details(int id)
	{
		try
		{
            var transationModel = new VehiclePaymentDetailViewModel();
            transationModel.IsPay = false;
            var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				TempData["ErrorMessage"] = "Araç bulunamadı.";
				return RedirectToAction(nameof(Index));
			}

			if (vehicle.ExitDate != null)
			{
                var totalDay = transationModel.TotalDays = (int)(vehicle.ExitDate.Value - vehicle.EntryDate).TotalDays;
                var paymentData = vehicle.Transactions;
                if (paymentData.Where(x => x.TransactionCategoryId == 1).Count() > 0)
                {
                    var first = paymentData.First(x=>x.TransactionCategoryId==1);
                    transationModel.Amount = first.Amount;
                    transationModel.PaymentDate = first.TransactionDate;
                    transationModel.Description = first.Description;
                    transationModel.TotalDays = totalDay;
                    transationModel.IsPay = true;
                }
                else
                {
                   
                }
            }



            TowingExpenseViewModel towingExpense = new TowingExpenseViewModel();

            if (vehicle.Transactions.Where(x => x.TransactionCategoryId == 2).Count() > 0)
			{
				var record = vehicle.Transactions.First(x => x.TransactionCategoryId == 2);
                towingExpense.TowingDate=record.TransactionDate;
				towingExpense.Description = record.Description;
				towingExpense.Amount = record.Amount;
				towingExpense.VehicleId = vehicle.Id;
				towingExpense.IsPaid = true;
			}
			else
				towingExpense.IsPaid = false;
			


            var viewModel = new VehicleDetailsViewModel
			{
				Id = vehicle.Id,
				PlateNumber = vehicle.PlateNumber,
				VehicleOnwer = vehicle.VehicleOnwer,
				VehicleOnwerIdentityNumber = vehicle.VehicleOnwerIdentityNumber,
				BookSequenceNo = vehicle.BookSequenceNo,
				LinkingTeamNumber = vehicle.LinkingTeamNumber,
				EntryDate = vehicle.EntryDate,
				ExitDate = vehicle.ExitDate,
				LinkingAdditionalInformation = vehicle.LinkingAdditionalInformation,
				MuhammenBedeli = vehicle.MuhammenBedeli,
				Description = vehicle.Description,
				BranchName = vehicle.Branch.Name,
				VehicleTypeName = vehicle.VehicleType.Name,
				VehicleTypeId = vehicle.VehicleTypeId,
				VehicleBrandName = vehicle.VehicleBrand.Name,
				VehicleModelName = vehicle.VehicleModel.Name,
				VehicleYearName = vehicle.VehicleYear.Name,
				VehicleColorName = vehicle.VehicleColor.Name,
				KeyLocationName = vehicle.KeyLocation.Name,
				LinkingRegionName = vehicle.LinkingRegion.Name,
				LinkingReasonName = vehicle.LinkingReason.Name,
				ParkLocationName = vehicle.ParkLocation?.Name,
				CreatedBy = vehicle.CreatedBy,
				UpdatedBy = vehicle.UpdatedBy,
				CreatedDate = vehicle.CreatedDate,
				UpdatedDate = vehicle.UpdatedDate,
				PaymentDetail= transationModel,
				IsForSale = vehicle.IsForSale,
				SaleDate = vehicle.SaleDate,
				SalePrice = vehicle.SalePrice,
				TowingExpense = towingExpense,
				Documents = vehicle.Documents.Select(d => new DocumentViewModel
				{
					Id = d.Id,
					Name = d.Name,
					FilePath = d.FilePath,
					CreatedAt = d.CreatedAt,
					VehicleId = d.VehicleId
				}).ToList(),
				Images = vehicle.Images.Select(i => new VehicleImageViewModel
				{
					Id = i.Id,
					FilePath = i.FilePath,
					UploadedAt = i.UploadedAt,
					VehicleId = i.VehicleId
				}).ToList(),
				EnforcementRecords = vehicle.EnforcementRecords.Select(er => new EnforcementRecordViewModel
				{
					Id = er.Id,
					EnforcementNumber = er.DossierNumber,
					EnforcementOfficeName = er.EnforcementOffice.Name,
					DecisionDate = er.RecordDate,
					Description = "", // EnforcementRecord entity'sinde Description alanı yok
					VehicleId = er.VehicleId,
					EnforcementOfficeId = er.EnforcementOfficeId
				}).ToList()
			};

			ViewBag.VehicleId = vehicle.Id;
			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Araç detayları görüntülenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Index));
		}
	}

	[RoleAuthorization("Admin,Vehicle_Add")]
	public async Task<IActionResult> Create()
	{
		try
		{
			var viewModel = new VehicleCreateViewModel
			{
				EntryDate = DateTime.Now,
				Branches = new SelectList(await _branchService.GetAllAsync(), "Id", "Name"),
				VehicleTypes = new SelectList(await _vehicleTypeService.GetAllAsync(), "Id", "Name"),
				VehicleBrands = new SelectList(await _vehicleBrandService.GetAllAsync(), "Id", "Name"),
				VehicleModels = new SelectList(await _vehicleModelService.GetAllAsync(), "Id", "Name"),
				VehicleYears = new SelectList(await _vehicleYearService.GetAllAsync(), "Id", "Name"),
				VehicleColors = new SelectList(await _vehicleColorService.GetAllAsync(), "Id", "Name"),
				KeyLocations = new SelectList(await _keyLocationService.GetAllAsync(), "Id", "Name"),
				LinkingRegions = new SelectList(await _linkingRegionService.GetAllAsync(), "Id", "Name"),
				LinkingReasons = new SelectList(await _linkingReasonService.GetAllAsync(), "Id", "Name"),
				ParkLocations = new SelectList(await _parkLocationService.GetAllAsync(), "Id", "Name")
			};

			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Araç oluşturma formu yüklenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Index));
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin,Vehicle_Add")]
	public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
	{
		try
		{
			if (ModelState.IsValid)
			{
				// Form'dan gelen tarih ve saat bilgisini parse et
				var entryDate = DateTime.ParseExact(
					Request.Form["EntryDate"].ToString(),
					"yyyy-MM-dd HH:mm",
					System.Globalization.CultureInfo.InvariantCulture
				);

				DateTime? exitDate = null;
				if (!string.IsNullOrEmpty(Request.Form["ExitDate"].ToString()))
				{
					exitDate = DateTime.ParseExact(
						Request.Form["ExitDate"].ToString(),
						"yyyy-MM-dd HH:mm",
						System.Globalization.CultureInfo.InvariantCulture
					);
				}

				var vehicle = new Vehicle
				{
					PlateNumber = viewModel.PlateNumber,
					VehicleOnwer = viewModel.VehicleOnwer,
					VehicleOnwerIdentityNumber = viewModel.VehicleOnwerIdentityNumber,
					BookSequenceNo = viewModel.BookSequenceNo,
					LinkingTeamNumber = viewModel.LinkingTeamNumber,
					EntryDate = entryDate,
					ExitDate = exitDate,
					LinkingAdditionalInformation = viewModel.LinkingAdditionalInformation,
					MuhammenBedeli = viewModel.MuhammenBedeli,
					Description = viewModel.Description,
					BranchId = viewModel.BranchId,
					VehicleTypeId = viewModel.VehicleTypeId,
					VehicleBrandId = viewModel.VehicleBrandId,
					VehicleModelId = viewModel.VehicleModelId,
					VehicleYearId = viewModel.VehicleYearId,
					VehicleColorId = viewModel.VehicleColorId,
					KeyLocationId = viewModel.KeyLocationId,
					LinkingRegionId = viewModel.LinkingRegionId,
					LinkingReasonId = viewModel.LinkingReasonId,
					ParkLocationId = viewModel.ParkLocationId
				};

				var isPlateFind = _vehicleService.GetPlateNumber(viewModel.PlateNumber);
				if (isPlateFind)
				{
					TempData["ErrorMessage"] = "Araç Zaten Mevcut ve Çıkışı Yapılmamış";
					return View(viewModel);
				}


				await _vehicleService.AddAsync(vehicle);
				TempData["SuccessMessage"] = "Araç başarıyla oluşturuldu.";
				return RedirectToAction(nameof(Details), new { id = vehicle.Id });
			}

			await PopulateDropdowns(viewModel);
			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Araç oluşturulurken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Index));
		}
	}

	private async Task PopulateDropdowns(VehicleCreateViewModel viewModel)
	{
		viewModel.Branches = new SelectList(await _branchService.GetAllAsync(), "Id", "Name", viewModel.BranchId);
		viewModel.VehicleTypes = new SelectList(await _vehicleTypeService.GetAllAsync(), "Id", "Name", viewModel.VehicleTypeId);
		viewModel.VehicleBrands = new SelectList(await _vehicleBrandService.GetAllAsync(), "Id", "Name", viewModel.VehicleBrandId);
		viewModel.VehicleModels = new SelectList(await _vehicleModelService.GetAllAsync(), "Id", "Name", viewModel.VehicleModelId);
		viewModel.VehicleYears = new SelectList(await _vehicleYearService.GetAllAsync(), "Id", "Name", viewModel.VehicleYearId);
		viewModel.VehicleColors = new SelectList(await _vehicleColorService.GetAllAsync(), "Id", "Name", viewModel.VehicleColorId);
		viewModel.KeyLocations = new SelectList(await _keyLocationService.GetAllAsync(), "Id", "Name", viewModel.KeyLocationId);
		viewModel.LinkingRegions = new SelectList(await _linkingRegionService.GetAllAsync(), "Id", "Name", viewModel.LinkingRegionId);
		viewModel.LinkingReasons = new SelectList(await _linkingReasonService.GetAllAsync(), "Id", "Name", viewModel.LinkingReasonId);
		viewModel.ParkLocations = new SelectList(await _parkLocationService.GetAllAsync(), "Id", "Name", viewModel.ParkLocationId);
	}

	[RoleAuthorization("Admin,Vehicle_Edit")]
	public async Task<IActionResult> Edit(int id)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				TempData["ErrorMessage"] = "Araç bulunamadı.";
				return RedirectToAction(nameof(Index));
			}

			var viewModel = new VehicleEditViewModel
			{
				Id = vehicle.Id,
				PlateNumber = vehicle.PlateNumber,
				VehicleOnwer = vehicle.VehicleOnwer,
				VehicleOnwerIdentityNumber = vehicle.VehicleOnwerIdentityNumber,
				BookSequenceNo = vehicle.BookSequenceNo,
				LinkingTeamNumber = vehicle.LinkingTeamNumber,
				EntryDate = vehicle.EntryDate,
				ExitDate = vehicle.ExitDate,
				LinkingAdditionalInformation = vehicle.LinkingAdditionalInformation,
				MuhammenBedeli = vehicle.MuhammenBedeli,
				Description = vehicle.Description,
				BranchId = vehicle.BranchId,
				VehicleTypeId = vehicle.VehicleTypeId,
				VehicleBrandId = vehicle.VehicleBrandId,
				VehicleModelId = vehicle.VehicleModelId,
				VehicleYearId = vehicle.VehicleYearId,
				VehicleColorId = vehicle.VehicleColorId,
				KeyLocationId = vehicle.KeyLocationId,
				LinkingRegionId = vehicle.LinkingRegionId,
				LinkingReasonId = vehicle.LinkingReasonId,
				ParkLocationId = vehicle.ParkLocationId,
				Branches = new SelectList(await _branchService.GetAllAsync(), "Id", "Name", vehicle.BranchId),
				VehicleTypes = new SelectList(await _vehicleTypeService.GetAllAsync(), "Id", "Name", vehicle.VehicleTypeId),
				VehicleBrands = new SelectList(await _vehicleBrandService.GetAllAsync(), "Id", "Name", vehicle.VehicleBrandId),
				VehicleModels = new SelectList(await _vehicleModelService.GetAllAsync(), "Id", "Name", vehicle.VehicleModelId),
				VehicleYears = new SelectList(await _vehicleYearService.GetAllAsync(), "Id", "Name", vehicle.VehicleYearId),
				VehicleColors = new SelectList(await _vehicleColorService.GetAllAsync(), "Id", "Name", vehicle.VehicleColorId),
				KeyLocations = new SelectList(await _keyLocationService.GetAllAsync(), "Id", "Name", vehicle.KeyLocationId),
				LinkingRegions = new SelectList(await _linkingRegionService.GetAllAsync(), "Id", "Name", vehicle.LinkingRegionId),
				LinkingReasons = new SelectList(await _linkingReasonService.GetAllAsync(), "Id", "Name", vehicle.LinkingReasonId),
				ParkLocations = new SelectList(await _parkLocationService.GetAllAsync(), "Id", "Name", vehicle.ParkLocationId)
			};

			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Araç düzenleme formu yüklenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Index));
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin,Vehicle_Edit")]
	public async Task<IActionResult> Edit(int id, VehicleEditViewModel viewModel)
	{
		if (id != viewModel.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			try
			{
				var vehicle = await _vehicleService.GetByIdAsync(id);
				if (vehicle == null)
				{
					return NotFound();
				}

				// Form'dan gelen tarih ve saat bilgisini parse et
				var entryDate = DateTime.ParseExact(
					Request.Form["EntryDate"].ToString(),
					"yyyy-MM-dd HH:mm",
					System.Globalization.CultureInfo.InvariantCulture
				);

				DateTime? exitDate = null;
				if (!string.IsNullOrEmpty(Request.Form["ExitDate"].ToString()))
				{
					exitDate = DateTime.ParseExact(
						Request.Form["ExitDate"].ToString(),
						"yyyy-MM-dd HH:mm",
						System.Globalization.CultureInfo.InvariantCulture
					);
				}

				// Update basic properties
				vehicle.PlateNumber = viewModel.PlateNumber;
				vehicle.VehicleOnwer = viewModel.VehicleOnwer;
				vehicle.VehicleOnwerIdentityNumber = viewModel.VehicleOnwerIdentityNumber;
				vehicle.BookSequenceNo = viewModel.BookSequenceNo;
				vehicle.LinkingTeamNumber = viewModel.LinkingTeamNumber;
				vehicle.EntryDate = entryDate;
				vehicle.ExitDate = exitDate;
				vehicle.LinkingAdditionalInformation = viewModel.LinkingAdditionalInformation;
				vehicle.MuhammenBedeli = viewModel.MuhammenBedeli;
				vehicle.Description = viewModel.Description;

				// Update foreign key relationships
				vehicle.BranchId = viewModel.BranchId;
				vehicle.VehicleTypeId = viewModel.VehicleTypeId;
				vehicle.VehicleBrandId = viewModel.VehicleBrandId;
				vehicle.VehicleModelId = viewModel.VehicleModelId;
				vehicle.VehicleYearId = viewModel.VehicleYearId;
				vehicle.VehicleColorId = viewModel.VehicleColorId;
				vehicle.KeyLocationId = viewModel.KeyLocationId;
				vehicle.LinkingRegionId = viewModel.LinkingRegionId;
				vehicle.LinkingReasonId = viewModel.LinkingReasonId;
				vehicle.ParkLocationId = viewModel.ParkLocationId;

				await _vehicleService.UpdateAsync(vehicle);
				TempData["SuccessMessage"] = "Araç başarıyla güncellendi.";
				return RedirectToAction(nameof(Details), new { id = vehicle.Id });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Araç güncellenirken bir hata oluştu: " + ex.Message);
			}
		}

		return View(viewModel);
	}

	[RoleAuthorization("Admin,Vehicle_Remove")]
	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				TempData["ErrorMessage"] = "Araç bulunamadı.";
				return RedirectToAction(nameof(Index));
			}

			return View(vehicle);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Araç silme formu yüklenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Index));
		}
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin,Vehicle_Remove")]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		try
		{
			await _vehicleService.DeleteAsync(id);
			TempData["SuccessMessage"] = "Araç başarıyla silindi.";
			return RedirectToAction(nameof(Index));
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Araç silinirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Index));
		}
	}

	// İcra Kaydı Ekleme
	[RoleAuthorization("Admin,EnforcementRecord_Add")]
	public async Task<IActionResult> AddEnforcementRecord(int vehicleId)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(vehicleId);
			if (vehicle == null)
			{
				TempData["ErrorMessage"] = "Araç bulunamadı.";
				return RedirectToAction(nameof(Index));
			}

			var viewModel = new EnforcementRecordCreateViewModel
			{
				VehicleId = vehicleId,
				DecisionDate = DateTime.Now,
				EnforcementOffices = new SelectList(await _enforcementOfficeService.GetAllAsync(), "Id", "Name")
			};

			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"İcra kaydı formu yüklenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Details), new { id = vehicleId });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin,EnforcementRecord_Add")]
	public async Task<IActionResult> AddEnforcementRecord(EnforcementRecordCreateViewModel viewModel)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var enforcementRecord = new EnforcementRecord
				{
					DossierNumber = viewModel.EnforcementNumber,
					RecordDate = viewModel.DecisionDate,
					EnforcementOfficeId = viewModel.EnforcementOfficeId,
					VehicleId = viewModel.VehicleId,
					CreatedAt = DateTime.Now
				};

				await _enforcementRecordService.AddAsync(enforcementRecord);
				TempData["SuccessMessage"] = "İcra kaydı başarıyla eklendi.";
				return RedirectToAction(nameof(Details), new { id = viewModel.VehicleId });
			}

			viewModel.EnforcementOffices = new SelectList(await _enforcementOfficeService.GetAllAsync(), "Id", "Name", viewModel.EnforcementOfficeId);
			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"İcra kaydı eklenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Details), new { id = viewModel.VehicleId });
		}
	}

	// İcra Kaydı Düzenleme
	[RoleAuthorization("Admin,EnforcementRecord_Edit")]
	public async Task<IActionResult> EditEnforcementRecord(int id)
	{
		try
		{
			var enforcementRecord = await _enforcementRecordService.GetByIdAsync(id);
			if (enforcementRecord == null)
			{
				TempData["ErrorMessage"] = "İcra kaydı bulunamadı.";
				return RedirectToAction(nameof(Index));
			}

			var viewModel = new EnforcementRecordEditViewModel
			{
				Id = enforcementRecord.Id,
				EnforcementNumber = enforcementRecord.DossierNumber,
				DecisionDate = enforcementRecord.RecordDate,
				EnforcementOfficeId = enforcementRecord.EnforcementOfficeId,
				VehicleId = enforcementRecord.VehicleId,
				EnforcementOffices = new SelectList(await _enforcementOfficeService.GetAllAsync(), "Id", "Name", enforcementRecord.EnforcementOfficeId)
			};

			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"İcra kaydı düzenleme formu yüklenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Details), new { id = (await _enforcementRecordService.GetByIdAsync(id))?.VehicleId });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin,EnforcementRecord_Edit")]
	public async Task<IActionResult> EditEnforcementRecord(EnforcementRecordEditViewModel viewModel)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var enforcementRecord = await _enforcementRecordService.GetByIdAsync(viewModel.Id);
				if (enforcementRecord == null)
				{
					TempData["ErrorMessage"] = "İcra kaydı bulunamadı.";
					return RedirectToAction(nameof(Details), new { id = viewModel.VehicleId });
				}

				enforcementRecord.DossierNumber = viewModel.EnforcementNumber;
				enforcementRecord.RecordDate = viewModel.DecisionDate;
				enforcementRecord.EnforcementOfficeId = viewModel.EnforcementOfficeId;

				await _enforcementRecordService.UpdateAsync(enforcementRecord);
				TempData["SuccessMessage"] = "İcra kaydı başarıyla güncellendi.";
				return RedirectToAction(nameof(Details), new { id = viewModel.VehicleId });
			}

			viewModel.EnforcementOffices = new SelectList(await _enforcementOfficeService.GetAllAsync(), "Id", "Name", viewModel.EnforcementOfficeId);
			return View(viewModel);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"İcra kaydı güncellenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Details), new { id = viewModel.VehicleId });
		}
	}

	// İcra Kaydı Silme
	[RoleAuthorization("Admin,EnforcementRecord_Remove")]
	public async Task<IActionResult> DeleteEnforcementRecord(int id, int vehicleId)
	{
		try
		{
			await _enforcementRecordService.DeleteAsync(id);
			TempData["SuccessMessage"] = "İcra kaydı başarıyla silindi.";
			return RedirectToAction(nameof(Details), new { id = vehicleId });
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"İcra kaydı silinirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(Details), new { id = vehicleId });
		}
	}

	[RoleAuthorization("Admin,EnforcementRecord_Remove")]
	public async Task<IActionResult> DeletedVehicles()
	{
		try
		{
			var deletedVehicles = await _vehicleService.GetDeletedVehiclesAsync();
			return View(deletedVehicles);
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Silinmiş araçlar listelenirken bir hata oluştu: {ex.Message}";
			return RedirectToAction("Index", "Home");
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin")]
	public async Task<IActionResult> Restore(int id)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				TempData["ErrorMessage"] = "Araç bulunamadı.";
				return RedirectToAction(nameof(DeletedVehicles));
			}

			vehicle.DeletedDate = null;
			await _vehicleService.UpdateAsync(vehicle);
			TempData["SuccessMessage"] = "Araç başarıyla geri getirildi.";
			return RedirectToAction(nameof(DeletedVehicles));
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = $"Araç geri getirilirken bir hata oluştu: {ex.Message}";
			return RedirectToAction(nameof(DeletedVehicles));
		}
	}


    [RoleAuthorization("Admin,Document_Add")]
    public IActionResult AddDocument(int vehicleId)
	{
		var model = new DocumentUploadViewModel
		{
			VehicleId = vehicleId
		};
		return View(model);
	}

	[HttpPost]
    [RoleAuthorization("Admin,Document_Add")]
    public async Task<IActionResult> AddDocument(int vehicleId, List<IFormFile> files)
	{
		// Debug için gelen verileri kontrol edelim
		var request = HttpContext.Request;
		var formFiles = request.Form.Files;

		if (formFiles == null || !formFiles.Any())
		{
			return Json(new { success = false, message = "Form dosyaları boş." });
		}

		try
		{
			foreach (var file in formFiles)
			{
				if (file != null && file.Length > 0)
				{
					// Dosya adını benzersiz yap
					string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

					// Dosya yolunu oluştur
					string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Files");
					string filePath = Path.Combine(uploadsFolder, uniqueFileName);

					// Dosyayı kaydet
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}

					// Veritabanına kaydet
					var document = new Document
					{
						VehicleId = vehicleId,
						Name = file.FileName,
						FilePath = "/Files/" + uniqueFileName,
						CreatedAt = DateTime.Now
					};

					await _documentService.AddAsync(document);
				}
			}

			return Json(new { success = true });
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = "Dosya yükleme sırasında bir hata oluştu: " + ex.Message });
		}
	}

	[HttpGet]
    [RoleAuthorization("Admin,VehicleImage_Add")]
    public async Task<IActionResult> AddImage(int vehicleId)
	{
		var vehicle = await _vehicleService.GetByIdAsync(vehicleId);
		if (vehicle == null)
		{
			return NotFound();
		}

		var model = new VehicleImageUploadViewModel
		{
			VehicleId = vehicleId
		};

		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,VehicleImage_Add")]
    public async Task<IActionResult> AddImage(int vehicleId, List<IFormFile> files)
	{

		var request = HttpContext.Request;
		var formFiles = request.Form.Files;
		if (formFiles == null || !formFiles.Any())
		{
			return Json(new { success = false, message = "Form dosyaları boş." });
		}

		try
		{
			foreach (var file in formFiles)
			{
				if (file.Length > 0)
				{
					// Resim dosyası kontrolü
					if (!file.ContentType.StartsWith("image/"))
					{
						return Json(new { success = false, message = "Sadece resim dosyaları yüklenebilir." });
					}

					// Dosya adını benzersiz yap
					var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
					var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

					// Dizin yoksa oluştur
					Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

					// Dosyayı kaydet
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}

					// Veritabanına kaydet
					var vehicleImage = new VehicleImage
					{
						VehicleId = vehicleId,
						FilePath = $"/Images/{fileName}",
						UploadedAt = DateTime.Now
					};

					await _vehicleImageService.AddAsync(vehicleImage);
				}
			}

			return Json(new { success = true });
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = "Resim yükleme sırasında bir hata oluştu: " + ex.Message });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin,VehicleImage_Remove")]
	public async Task<IActionResult> DeleteImage(int id)
	{
		try
		{
			var image = await _vehicleImageService.GetByIdAsync(id);
			if (image == null)
			{
				TempData["ErrorMessage"] = "Resim bulunamadı.";
				return RedirectToAction(nameof(Index));
			}

			// Fiziksel dosyayı sil
			var filePath = Path.Combine(_webHostEnvironment.WebRootPath, image.FilePath.TrimStart('/'));
			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}

			// Veritabanından sil
			await _vehicleImageService.DeleteAsync(id);

			TempData["SuccessMessage"] = "Resim başarıyla silindi.";
			return RedirectToAction(nameof(Details), new { id = image.VehicleId });
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = "Resim silme sırasında bir hata oluştu: " + ex.Message;
			return RedirectToAction(nameof(Index));
		}
	}

	[HttpPost]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> DeliverVehicle(int id)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				return Json(new { success = false, message = "Araç bulunamadı." });
			}

			if (vehicle.ExitDate.HasValue)
			{
				return Json(new { success = false, message = "Araç zaten teslim edilmiş." });
			}

			vehicle.ExitDate = DateTime.Now;
			await _vehicleService.UpdateAsync(vehicle);

			DevliverPriceCalculate(vehicle);

			return Json(new { success = true });
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = "Araç teslim edilirken bir hata oluştu: " + ex.Message });
		}
	}

	private void DevliverPriceCalculate(Vehicle vehicle)
	{

		var entryDate = vehicle.EntryDate;
		var exitDate = vehicle.ExitDate;
		var vehicleTypePrice = vehicle.VehicleType.UkomePrice;

		var totalDays = (exitDate.Value - entryDate).TotalDays;
		if (totalDays < 1)
			totalDays = 1; // En az 1 gün

		var totalPrice = (int)Math.Ceiling(totalDays) * vehicleTypePrice;

		var transaction = new Transaction
		{
			VehicleId = vehicle.Id,
			TransactionCategoryId= 1, // Ödeme kategorisi
			BranchId = vehicle.BranchId,
			Amount = (decimal)totalPrice,
			TransactionDate = DateTime.Now,
			Description = $"{vehicle.PlateNumber} - Otopark ücreti"
		};

		_transactionService.AddAsync(transaction);
		// Ödeme işlemi burada yapılabilir




	}

	[HttpGet]
	public async Task<IActionResult> CheckEnforcementRecord(int id)
	{
		var vehicle = await _vehicleService.GetByIdAsync(id);
		if (vehicle == null)
			return Json(false);

		// İcra kaydı kontrolü burada yapılacak
		// Örnek olarak false dönüyoruz, gerçek uygulamada veritabanından kontrol edilmeli
		return Json(false);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[RoleAuthorization("Admin")]
	public async Task<IActionResult> HardDeleteVehicle(int id)
	{
		await _vehicleService.HardDeleteAsync(id); // Serviste bu metodu yazmalısın
		TempData["SuccessMessage"] = "Araç kalıcı olarak silindi.";
		return RedirectToAction(nameof(DeletedVehicles));
	}
		
	[HttpPost]
	[ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> SetForSale(int id, DateTime saleDate, decimal salePrice)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				return Json(new { success = false, message = "Araç bulunamadı." });
			}

			vehicle.IsForSale = true;
			vehicle.SaleDate = saleDate;
			vehicle.SalePrice = salePrice;

			await _vehicleService.UpdateAsync(vehicle);

			return Json(new { success = true });
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> UpdateSale(int id, DateTime saleDate, decimal salePrice)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				return Json(new { success = false, message = "Araç bulunamadı." });
			}

			vehicle.SaleDate = saleDate;
			vehicle.SalePrice = salePrice;

			await _vehicleService.UpdateAsync(vehicle);

			return Json(new { success = true });
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> DeleteSale(int id)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				return Json(new { success = false, message = "Araç bulunamadı." });
			}

			vehicle.IsForSale = false;
			vehicle.SaleDate = null;
			vehicle.SalePrice = null;

			await _vehicleService.UpdateAsync(vehicle);

			return Json(new { success = true });
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CalculatePayment(int vehicleId, string entryDate, string exitDate, bool isForSale)
	{
		var vehicle = await _vehicleService.GetByIdAsync(vehicleId);
		if (vehicle == null)
			return Json(new { success = false });

		// Tarihleri güvenli şekilde parse et
		if (!DateTime.TryParse(entryDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var entry) || 
			!DateTime.TryParse(exitDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var exit))
		{
			return Json(new { success = false, message = "Tarih formatı hatalı." });
		}

		// Toplam gün hesabı
		int totalDays = (int)Math.Ceiling((exit - entry).TotalDays);
		if (totalDays < 1) totalDays = 1;

		decimal dailyPrice = isForSale ? (decimal)vehicle.VehicleType.PolicePrice : (decimal)vehicle.VehicleType.UkomePrice;
		decimal totalAmount = dailyPrice * totalDays;

		return Json(new { success = true, totalDays, dailyPrice, totalAmount });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> SavePayment(int id, DateTime exitDate,DateTime paymentDate, string description)
	{
		try
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null)
			{
				return Json(new { success = false, message = "Araç bulunamadı." });
			}

			// Araç çıkış tarihini güncelle
			vehicle.ExitDate = exitDate;
			await _vehicleService.UpdateAsync(vehicle);

			// Ödeme kaydını oluştur
			var transaction = new Transaction
			{
				VehicleId = vehicle.Id,
				TransactionCategoryId = 1, // Ödeme kategorisi
				BranchId = vehicle.BranchId,
				Amount = (decimal)(vehicle.IsForSale ? 
					vehicle.VehicleType.PolicePrice * (int)Math.Ceiling((vehicle.ExitDate.Value - vehicle.EntryDate).TotalDays) :
					vehicle.VehicleType.UkomePrice * (int)Math.Ceiling((vehicle.ExitDate.Value - vehicle.EntryDate).TotalDays)),
				TransactionDate = paymentDate,
				Description = description ?? $"{vehicle.PlateNumber} - Otopark ücreti"
			};

			await _transactionService.AddAsync(transaction);

			return Json(new { success = true });
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> UpdatePayment(int vehicleId, decimal amount, DateTime paymentDate, int totalDays, string? description)
	{
		var vehicle = await _vehicleService.GetByIdAsync(vehicleId);

		var transaction = await _transactionService.GetByIdAsync(vehicle.Transactions.First(x=>x.TransactionCategoryId==1).Id);
		if (transaction == null)
			return Json(new { success = false, message = "Ödeme kaydı bulunamadı." });

		transaction.Amount = amount;
		transaction.TransactionDate = paymentDate;
		transaction.Description = description;
		// totalDays sadece ViewModel'de gösterim için

		await _transactionService.UpdateAsync(transaction);
		return Json(new { success = true });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> DeletePayment(int vehicleId)
    {
        var vehicle = await _vehicleService.GetByIdAsync(vehicleId);
        var id = await _transactionService.GetByIdAsync(vehicle.Transactions.First(x=>x.TransactionCategoryId==1).Id);

        var transaction = await _transactionService.GetByIdAsync(id.Id);
		if (transaction == null)
			return Json(new { success = false, message = "Ödeme kaydı bulunamadı." });

		vehicle.ExitDate = null;

		await _transactionService.DeleteAsync(transaction.Id);
		await _vehicleService.UpdateAsync(vehicle);

		return Json(new { success = true });
	}


    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> SaveTowingExpense(int id, DateTime towingDate, decimal towingAmount, string towingDescription)
	{
		try
		{

			if(id== 0)
				return Json(new { success = false, message = "Araç bulunamadı." });

            if (towingAmount == null || towingAmount == 0)
                return Json(new { success = false, message = "Çekici Ücreti Boş Geçilemez" });


            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return Json(new { success = false, message = "Araç bulunamadı." });
            }


            var transaction = new Transaction
            {
                VehicleId = vehicle.Id,
                TransactionCategoryId = 2,
                BranchId = vehicle.BranchId,
                Amount = towingAmount,
                TransactionDate = towingDate,
                Description = towingDescription ?? $"{vehicle.PlateNumber} - Çekici Gideri"
            };

            await _transactionService.AddAsync(transaction);

            return Json(new { success = true });
        }
		catch (Exception ex)
		{
            return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
        }


	}

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> UpdateTowingExpense(int id, DateTime towingDate, decimal amount, string towingDescription)
	{

		try
		{
					var vehicle = await _vehicleService.GetByIdAsync(id);

            var transaction = await _transactionService.GetByIdAsync(vehicle.Transactions.First(x => x.TransactionCategoryId == 2).Id);
            if (transaction == null)
                return Json(new { success = false, message = "Ödeme kaydı bulunamadı." });

            transaction.Amount = amount;
            transaction.TransactionDate = towingDate;
            transaction.Description = towingDescription;
            await _transactionService.UpdateAsync(transaction);
            return Json(new { success = true });
        }
		catch (Exception ex)
		{
            return Json(new { success = false, message = "Hata Oluştu Hata:" + ex });
        }
        
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Vehicle_Edit")]
    public async Task<IActionResult> DeleteTowingExpense(int id)
    {
		try
		{
            var vehicle = await _vehicleService.GetByIdAsync(id);
            var transationId = await _transactionService.GetByIdAsync(vehicle.Transactions.First(x => x.TransactionCategoryId == 2).Id);

            var transaction = await _transactionService.GetByIdAsync(transationId.Id);
            if (transaction == null)
                return Json(new { success = false, message = "Ödeme kaydı bulunamadı." });

            vehicle.ExitDate = null;

            await _transactionService.DeleteAsync(transaction.Id);
            await _vehicleService.UpdateAsync(vehicle);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Hata Oluştu Hata:" + ex });
        }

    }

    [HttpGet]
    [RoleAuthorization("Admin")]
    public async Task<IActionResult> PricingSettings()
    {
        var vehicleTypes = await _vehicleTypeService.GetAllAsync();
        return View(vehicleTypes);
    }
}