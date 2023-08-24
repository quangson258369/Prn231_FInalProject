using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class LoginRequest
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
