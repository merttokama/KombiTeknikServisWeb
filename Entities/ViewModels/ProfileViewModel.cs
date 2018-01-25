using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class ProfileViewModel
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


        [Display(Name = "Eski Şifre")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [StringLength(100)]
        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z]\w{4,14}$", ErrorMessage = @"	
Parolanın ilk karakteri bir harf olmalıdır, en az 5 karakter içermeli ve en fazla 15 karakter içermelidir ve harfler, rakamlar ve altçizgi dışındaki karakterler kullanılmamalıdır.")]
        public string NewPassword { get; set; }


        [StringLength(100)]
        [Display(Name = "Yeni Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmNewPassword { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Girilen telefon formatı geçerli değil.")]
        public string PhoneNumber { get; set; }
    }
}
