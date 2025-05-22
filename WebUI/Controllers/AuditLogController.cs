using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;

namespace WebUI.Controllers;

[RoleAuthorization("Admin")]
public class AuditLogController : Controller
{
    private readonly IAuditLogService _auditLogService;
    public AuditLogController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 20, string search = null)
    {
        var logs = await _auditLogService.GetPagedAsync(pageIndex, pageSize, search);
        ViewBag.Search = search;
        return View(logs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var log = await _auditLogService.GetByIdAsync(id);
        if (log == null)
            return NotFound();
        return View(log);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _auditLogService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}