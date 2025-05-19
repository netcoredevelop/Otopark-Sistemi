using System;

namespace WebUI.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class AuditEntityAttribute : Attribute
{
    public string EntityName { get; }
    public AuditEntityAttribute(string entityName)
    {
        EntityName = entityName;
    }
}