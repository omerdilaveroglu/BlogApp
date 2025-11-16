using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [Display(Name = "Kullanıcı Adı")]
    [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Kullanıcı adı boşluk içeremez.")]
    [MinLength(3, ErrorMessage = "Kullanıcı adı en az 3 karakter olmalıdır.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "İsim zorunludur.")]
    [Display(Name = "İsim")]
    [StringLength(100, ErrorMessage = "İsim en fazla 100 karakter olabilir.")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "Soyisim zorunludur.")]
    [Display(Name = "Soyisim")]
    [StringLength(100, ErrorMessage = "Soyisim en fazla 100 karakter olabilir.")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Parola zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    [MinLength(6, ErrorMessage = "Parola en az 6 karakter olmalıdır.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Parola tekrar zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Parola Tekrar")]
    [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor.")]
    public string? Password2 { get; set; }


    [Required(ErrorMessage = "Email zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    [StringLength(100, ErrorMessage = "Email en fazla 100 karakter olabilir.")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

}
