using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class RegisterRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
