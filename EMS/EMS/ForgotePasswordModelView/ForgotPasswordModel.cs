using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
