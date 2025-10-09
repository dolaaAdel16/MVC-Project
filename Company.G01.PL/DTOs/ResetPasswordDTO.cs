using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.DTOs
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "Password is required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match the password")]
        public string ConfirmPassword { get; set; }
    }
}
