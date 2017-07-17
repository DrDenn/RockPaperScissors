using System.ComponentModel.DataAnnotations;

namespace RpsWebsite.ViewModels
{
    public sealed class RegisterViewModel
    {
        [Required, MinLength(3), MaxLength(64)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
