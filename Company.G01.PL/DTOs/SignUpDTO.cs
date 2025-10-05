using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.DTOs
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "FirstName is required !!")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required !!")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "UserName is required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Confirm Password doesn't match the password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
