using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace WebUI.Models.TransactionCategory;

public class TransactionCategoryCreateModel
{
    [Required(ErrorMessage = "İsim alanı zorunludur.")]
    [StringLength(100, ErrorMessage = "İsim en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;

    [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "İşlem tipi seçilmelidir.")]
    public TransactionType Type { get; set; }

    public bool IsActive { get; set; } = true;
} 