using System.ComponentModel.DataAnnotations;

namespace Guest_book.Models
{
    public class Registr
    {
        
            [Required]
            public string? FirstName { get; set; }

            [Required]
            public string? LastName { get; set; }

            [Required]
            public string? Login { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            [Required]
            [Compare("Password", ErrorMessage = "Passwords do not match")]
            [DataType(DataType.Password)]
            public string? PasswordConfirm { get; set; }
        
    }
}
