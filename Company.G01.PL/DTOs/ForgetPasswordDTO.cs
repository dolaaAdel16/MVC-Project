using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.DTOs
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
