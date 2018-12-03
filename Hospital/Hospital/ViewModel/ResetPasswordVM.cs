using System.ComponentModel.DataAnnotations;

namespace Hospital.ViewModel
{
    public class ResetPasswordVM
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }

    }
}