using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Model;

public class LoginViewmodel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email Adresi")]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    [StringLength(100, MinimumLength = 6, ErrorMessage ="{0} en az {2} karakter olmalıdır.")]
    public string? Password { get; set; }

}
