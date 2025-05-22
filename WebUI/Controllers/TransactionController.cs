using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using WebUI.Auth;
using WebUI.Models.Transaction;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Extensions;

namespace WebUI.Controllers;

[AuditEntity("Transaction")]
[ServiceFilter(typeof(AuditLogActionFilter))]
[RoleAuthorization]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly ITransactionCategoryService _transactionCategoryService;
    private readonly IBranchService _branchService;
    private readonly IVehicleService _vehicleService;

    public TransactionController(
        ITransactionService transactionService,
        ITransactionCategoryService transactionCategoryService,
        IBranchService branchService,
        IVehicleService vehicleService)
    {
        _transactionService = transactionService;
        _transactionCategoryService = transactionCategoryService;
        _branchService = branchService;
        _vehicleService = vehicleService;
    }

    [HttpGet]
    [RoleAuthorization("Admin,Transaction_View")]
    public async Task<IActionResult> Index()
    {
        var transactions = await _transactionService.GetAllAsync();
        await LoadViewData(null);
        return View(transactions);
    }

    [HttpGet]
    [Route("Transaction/Filter")]
    [RoleAuthorization("Admin,Transaction_View")]
    public async Task<IActionResult> Filter(int? branchId = null, int? categoryId = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        IEnumerable<Transaction> transactions;

        if (branchId.HasValue)
        {
            transactions = await _transactionService.GetTransactionsByBranchAsync(branchId.Value);
        }
        else if (categoryId.HasValue)
        {
            transactions = await _transactionService.GetTransactionsByCategoryAsync(categoryId.Value);
        }
        else if (startDate.HasValue && endDate.HasValue)
        {
            transactions = await _transactionService.GetTransactionsByDateRangeAsync(startDate.Value, endDate.Value);
        }
        else
        {
            transactions = await _transactionService.GetAllAsync();
        }

        await LoadViewData(null);
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

        return View("Index", transactions);
    }

    [HttpGet]
    [RoleAuthorization("Admin,Transaction_Add")]
    public async Task<IActionResult> Create(string type)
    {
        await LoadViewData(type);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Transaction_Add")]
    public async Task<IActionResult> Create(TransactionCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var transaction = new Transaction
            {
                TransactionCategoryId = model.TransactionCategoryId,
                Amount = model.Amount,
                Description = model.Description,
                TransactionDate = model.TransactionDate,
                ReferenceNumber = model.ReferenceNumber,
                BranchId = model.BranchId,
                VehicleId = model.VehicleId
            };

            await _transactionService.AddAsync(transaction);
            return RedirectToAction(nameof(Index));
        }

        await LoadViewData(null);
        return View(model);
    }
    [RoleAuthorization("Admin,Transaction_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var transaction = await _transactionService.GetByIdAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        var model = new TransactionEditModel
        {
            Id = transaction.Id,
            TransactionCategoryId = transaction.TransactionCategoryId,
            Amount = transaction.Amount,
            Description = transaction.Description,
            TransactionDate = transaction.TransactionDate,
            ReferenceNumber = transaction.ReferenceNumber,
            BranchId = transaction.BranchId,
            VehicleId = transaction.VehicleId
        };

        await LoadViewData(null);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Transaction_Edit")]
    public async Task<IActionResult> Edit(TransactionEditModel model)
    {
        if (ModelState.IsValid)
        {
            var transaction = await _transactionService.GetByIdAsync(model.Id);
            if (transaction == null)
            {
                return NotFound();
            }

            transaction.TransactionCategoryId = model.TransactionCategoryId;
            transaction.Amount = model.Amount;
            transaction.Description = model.Description;
            transaction.TransactionDate = model.TransactionDate;
            transaction.ReferenceNumber = model.ReferenceNumber;
            transaction.BranchId = model.BranchId;
            transaction.VehicleId = model.VehicleId;

            await _transactionService.UpdateAsync(transaction);
            return RedirectToAction(nameof(Index));
        }

        await LoadViewData(null);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,Transaction_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        await _transactionService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadViewData(string? type = null)
    {
        var categories = await _transactionCategoryService.GetActiveCategoriesAsync();
        
        // Eğer type parametresi varsa, kategorileri filtrele
        if (!string.IsNullOrEmpty(type) && Enum.TryParse<TransactionType>(type, out var transactionType))
        {
            categories = categories.Where(c => c.Type == transactionType);
        }
        
        var branches = await _branchService.GetAllAsync();
        var vehicles = await _vehicleService.GetAllAsync();

        ViewBag.TransactionCategories = new SelectList(categories, "Id", "Name");
        ViewBag.Branches = new SelectList(branches, "Id", "Name");
        ViewBag.Vehicles = new SelectList(vehicles, "Id", "PlateNumber");
    }
} 