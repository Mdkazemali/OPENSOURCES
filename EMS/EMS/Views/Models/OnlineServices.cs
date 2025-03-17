using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class OnlineServices
    {
        [Key]
        public int ServicesId { get; set; }
        public string ServiceUserName { get; set; } 

        public string ServiceTranjectionId { get; set; }
        public string Confirmation {  get; set; }

        public int TotalDays { get; set; }
        public int TotalAmount { get; set; }

        public string? TranjPhotoUrl { get; set; }

        [Display(Name = "TranjProfilePhoto")]
        [NotMapped]
        public IFormFile TranjProfilePhoto { get; set; }

        [NotMapped]
        public string? TranjBreifPhotoName { get; set; }


        [ForeignKey("Occupation")]
        public int ServiceUserId { get; set; }
        public virtual Occupation Occupation { get; set; }


        [ForeignKey("UserInformation")]
        public int UserInformationId { get; set; }
        public virtual UserInformation UserInformation { get; set; }

        public DateTime ExapairDate { get; set; }

    }
}
