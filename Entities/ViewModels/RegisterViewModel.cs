using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z]\w{4,14}$", ErrorMessage = @"	
Parolanın ilk karakteri bir harf olmalıdır, en az 5 karakter içermeli ve en fazla 15 karakter içermelidir ve harfler, rakamlar ve altçizgi dışındaki karakterler kullanılmamalıdır.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Girilen telefon formatı geçerli değil.")]
        public string PhoneNumber { get; set; }
    }
}
