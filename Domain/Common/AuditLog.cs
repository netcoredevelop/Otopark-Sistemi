public class AuditLog
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? ActionName { get; set; }
    public string? ControllerName { get; set; }
    public string? OperationType { get; set; }
    public string? EntityName { get; set; }
    public string? EntityId { get; set; }
    public string? OldData { get; set; } // Eski değer (JSON)
    public string? NewData { get; set; } // Yeni değer (JSON)
    public DateTime OperationDate { get; set; }
}