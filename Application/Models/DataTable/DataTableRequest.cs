namespace Application.Models.DataTable;

public class DataTableRequest
{
    public int Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public List<DataTableColumn> Columns { get; set; } = new();
    public List<DataTableOrder> Order { get; set; } = new();
    public DataTableSearch Search { get; set; } = new();
}

public class DataTableColumn
{
    public string Data { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public DataTableSearch Search { get; set; } = new();
}

public class DataTableOrder
{
    public int Column { get; set; }
    public string Dir { get; set; } = string.Empty;
}

public class DataTableSearch
{
    public string Value { get; set; } = string.Empty;
    public bool Regex { get; set; }
} 