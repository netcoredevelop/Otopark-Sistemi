using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using WebUI.Extensions;

public class AuditLogActionFilter : ActionFilterAttribute
{
    private readonly ApplicationDbContext _db;

    public AuditLogActionFilter(ApplicationDbContext db)
    {
        _db = db;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Sadece POST işlemlerinde çalışsın
        if (!string.Equals(context.HttpContext.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
            return;

        var httpContext = context.HttpContext;
        var userName = httpContext.User.Identity?.Name ?? "Anonim";
        var actionName = context.ActionDescriptor.RouteValues["action"];
        var controllerName = context.ActionDescriptor.RouteValues["controller"];

        // İşlem tipi belirle
        string? operationType = null;
        if (actionName != null)
        {
            if (actionName.ToLower().Contains("create")) operationType = "Create";
            else if (actionName.ToLower().Contains("edit")) operationType = "Edit";
            else if (actionName.ToLower().Contains("delete")) operationType = "Delete";
        }
        if (operationType == null)
            return;

        // ActionArguments'ten entityId ve model bul
        string? entityId = null;
        object? entity = null;
        foreach (var arg in context.ActionArguments)
        {
            if (arg.Key.ToLower().Contains("id"))
                entityId = arg.Value?.ToString();
            if (arg.Value != null && arg.Value.GetType().IsClass && arg.Value.GetType() != typeof(string))
                entity = arg.Value;
        }

        // Attribute ile entity adını bul
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var auditAttr = descriptor?.MethodInfo.GetCustomAttributes(typeof(AuditEntityAttribute), false)
            .FirstOrDefault() as AuditEntityAttribute
            ?? descriptor?.ControllerTypeInfo.GetCustomAttributes(typeof(AuditEntityAttribute), false)
            .FirstOrDefault() as AuditEntityAttribute;

        string? entityName = auditAttr?.EntityName;

        // Eski değer (veritabanından)
        string? oldData = null;
        if (!string.IsNullOrEmpty(entityName) && entityId != null && (operationType == "Edit" || operationType == "Delete"))
        {
            var dbSetProp = _db.GetType().GetProperty(entityName + "s");
            if (dbSetProp != null)
            {
                var dbSet = dbSetProp.GetValue(_db) as IQueryable<object>;
                if (dbSet != null)
                {
                    var elementType = dbSet.ElementType;
                    var keyProp = elementType.GetProperty("Id");
                    var oldEntity = dbSet.Cast<object>()
                        .ToList()
                        .FirstOrDefault(x => keyProp.GetValue(x) != null && keyProp.GetValue(x).ToString() == entityId); if (oldEntity != null)
                        oldData = JsonConvert.SerializeObject(oldEntity);
                }
            }
        }

        // Yeni değer (kullanıcıdan gelen model)
        string? newData = entity != null ? JsonConvert.SerializeObject(entity) : null;

        // Log kaydı oluştur
        var log = new AuditLog
        {
            UserName = userName,
            ActionName = actionName,
            ControllerName = controllerName,
            OperationType = operationType,
            EntityName = entityName,
            EntityId = entityId,
            OldData = oldData,
            NewData = newData,
            OperationDate = DateTime.Now
        };

        _db.AuditLogs.Add(log);
        _db.SaveChanges();

        base.OnActionExecuting(context);
    }
}