using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models;

public class UserEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
    [Display(Name = "Kullanıcı Adı")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Ad gereklidir.")]
    [Display(Name = "Ad")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad gereklidir.")]
    [Display(Name = "Soyad")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "E-posta gereklidir.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Şifre Onayı")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string? ConfirmPassword { get; set; }

    [Display(Name = "Roller")]
    public List<int> SelectedRoleIds { get; set; } = new List<int>();

    // Kullanıcıya atanabilecek tüm roller listesi
    public List<Role> AvailableRoles { get; set; } = new List<Role>();
} 