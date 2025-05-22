using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Models.Transaction;

public class TransactionCreateModel
{
    [Required(ErrorMessage = "Kategori seçilmelidir.")]
    public int TransactionCategoryId { get; set; }

    [Required(ErrorMessage = "Tutar alanı zorunludur.")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
    [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "İşlem tarihi zorunludur.")]
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Referans numarası zorunludur.")]
    [StringLength(50, ErrorMessage = "Referans numarası en fazla 50 karakter olabilir.")]
    public string ReferenceNumber { get; set; } = default!;

    public int? BranchId { get; set; }
    public int? VehicleId { get; set; }
} 