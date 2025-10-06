using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.DTOs
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
