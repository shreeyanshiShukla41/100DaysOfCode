using System.ComponentModel.DataAnnotations;

namespace TicketingPortal.Models
{
    public class USER_MODEL
    {
        [Key]
        public int USER_ID { get; set; }

        [Required(ErrorMessage = "Please enter your full name.")]
        public string FULL_NAME { get; set; }= string.Empty;

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EMAIL { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your password.")]
        public string PASSWORD { get; set; } = string.Empty;

        public string? ROLE { get; set; } = "User"; // Default role is "User"

    }
}
