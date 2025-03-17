using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderUserName { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        // New field to track seen status
        public bool Seen { get; set; } = false;

        // To track who received this message (if needed)
        public string ReceiverUserName { get; set; }
    }
}
