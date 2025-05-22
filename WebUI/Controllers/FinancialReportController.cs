using Microsoft.AspNetCore.Mvc;
using Application.Services;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Domain.Entities;

namespace WebUI.Controllers
{
    [Authorize]
    public class FinancialReportController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IBranchService _branchService;

        public FinancialReportController(
            ITransactionService transactionService,
            ITransactionCategoryService transactionCategoryService,
            IBranchService branchService)
        {
            _transactionService = transactionService;
            _transactionCategoryService = transactionCategoryService;
            _branchService = branchService;
        }

        public async Task<IActionResult> Index(
            int? branchId = null,
            int? categoryId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // Get all branches for filter
            var branches = await _branchService.GetAllAsync();
            ViewBag.Branches = branches;

            // Get all categories for filter
            var categories = await _transactionCategoryService.GetAllAsync();
            ViewBag.Categories = categories;

            // Get transactions with filters
            var transactions = await _transactionService.GetAllAsync();
            var query = transactions.AsQueryable();

            if (branchId.HasValue)
                query = query.Where(t => t.BranchId == branchId);

            if (categoryId.HasValue)
                query = query.Where(t => t.TransactionCategoryId == categoryId);

            if (startDate.HasValue)
                query = query.Where(t => t.TransactionDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.TransactionDate <= endDate.Value);

            var filteredTransactions = query.ToList();

            // Calculate summary values with null checks
            var totalIncome = filteredTransactions
                .Where(t => t.TransactionCategory != null && t.TransactionCategory.Type == TransactionType.Gelir)
                .Sum(t => t.Amount);

            var totalExpense = filteredTransactions
                .Where(t => t.TransactionCategory != null && t.TransactionCategory.Type == TransactionType.Gider)
                .Sum(t => t.Amount);

            var netAmount = totalIncome - totalExpense;

            // Get category-wise totals with null checks
            var incomeCategories = filteredTransactions
                .Where(t => t.TransactionCategory != null && t.TransactionCategory.Type == TransactionType.Gelir)
                .GroupBy(t => t.TransactionCategory)
                .Select(g => new
                {
                    CategoryName = g.Key?.Name ?? "Bilinmeyen Kategori",
                    Amount = g.Sum(t => t.Amount)
                })
                .ToList();

            var expenseCategories = filteredTransactions
                .Where(t => t.TransactionCategory != null && t.TransactionCategory.Type == TransactionType.Gider)
                .GroupBy(t => t.TransactionCategory)
                .Select(g => new
                {
                    CategoryName = g.Key?.Name ?? "Bilinmeyen Kategori",
                    Amount = g.Sum(t => t.Amount)
                })
                .ToList();

            ViewBag.TotalIncome = totalIncome;
            ViewBag.TotalExpense = totalExpense;
            ViewBag.NetAmount = netAmount;
            ViewBag.IncomeCategories = incomeCategories;
            ViewBag.ExpenseCategories = expenseCategories;

            return View();
        }
    }
} 