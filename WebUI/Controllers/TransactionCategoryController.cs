using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using WebUI.Auth;
using WebUI.Models.TransactionCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Extensions;

namespace WebUI.Controllers;

[AuditEntity("TransactionCategory")]
[ServiceFilter(typeof(AuditLogActionFilter))]
[RoleAuthorization]
public class TransactionCategoryController : Controller
{
    private readonly ITransactionCategoryService _transactionCategoryService;

    public TransactionCategoryController(ITransactionCategoryService transactionCategoryService)
    {
        _transactionCategoryService = transactionCategoryService;
    }

    [RoleAuthorization("Admin,TransactionCategory_View")]
    public async Task<IActionResult> Index()
    {
        var categories = await _transactionCategoryService.GetAllAsync();
        return View(categories);
    }


    [RoleAuthorization("Admin,TransactionCategory_Add")]
    public IActionResult Create()
    {
        ViewBag.TransactionTypes = new SelectList(Enum.GetValues(typeof(TransactionType))
            .Cast<TransactionType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() }), "Id", "Name");
            
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,TransactionCategory_Add")]
    public async Task<IActionResult> Create(TransactionCategoryCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var category = new TransactionCategory
            {
                Name = model.Name,
                Description = model.Description,
                Type = model.Type,
                IsActive = true
            };

            await _transactionCategoryService.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.TransactionTypes = new SelectList(Enum.GetValues(typeof(TransactionType))
            .Cast<TransactionType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() }), "Id", "Name");
            
        return View(model);
    }
    [RoleAuthorization("Admin,TransactionCategory_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _transactionCategoryService.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        var model = new TransactionCategoryEditModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Type = category.Type,
            IsActive = category.IsActive
        };

        ViewBag.TransactionTypes = new SelectList(Enum.GetValues(typeof(TransactionType))
            .Cast<TransactionType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() }), "Id", "Name");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,TransactionCategory_Edit")]
    public async Task<IActionResult> Edit(TransactionCategoryEditModel model)
    {
        if (ModelState.IsValid)
        {
            var category = await _transactionCategoryService.GetByIdAsync(model.Id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;
            category.Description = model.Description;
            category.Type = model.Type;
            category.IsActive = model.IsActive;

            await _transactionCategoryService.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.TransactionTypes = new SelectList(Enum.GetValues(typeof(TransactionType))
            .Cast<TransactionType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() }), "Id", "Name");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin,TransactionCategory_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        if(id <= 2)
        {
            TempData["ErrorMessage"] = "Bu iþlem yapýlamaz. Bu kategori sistem kategorisidir.";
            return RedirectToAction(nameof(Index));
        }

        await _transactionCategoryService.DeleteAsync(id);
        TempData["SuccessMessage"] = "Kategori baþarýyla silindi.";
        return RedirectToAction(nameof(Index));
    }
} 