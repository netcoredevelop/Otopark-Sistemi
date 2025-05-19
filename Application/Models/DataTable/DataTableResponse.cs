namespace Application.Models.DataTable;

public class DataTableResponse<T>
{
    public int Draw { get; set; }
    public int RecordsTotal { get; set; }
    public int RecordsFiltered { get; set; }
    public List<T> Data { get; set; } = new();
    public string? Error { get; set; }
} 