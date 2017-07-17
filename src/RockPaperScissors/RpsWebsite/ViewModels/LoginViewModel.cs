using System.ComponentModel.DataAnnotations;

namespace RpsWebsite.ViewModels
{
    public sealed class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
