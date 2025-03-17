using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EMS.Models
{
    public class UserInformatio
    {
        [Key]
        public int UserinfoId { get; set; }
        public string UserFullName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public ulong? NID { get; set; }
        public string Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Status { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string LoginId { get; set; }
        public string TranjectionId { get; set; }
        public string Designation { get; set; }
        public string Degree { get; set; }
        public string PassingYear { get; set; }
        public string Designations { get; set; }
        public string Institute { get; set; }
        public string PresentAddrss { get; set; }
        public string EmailNo { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }

        public string NagadNo { get; set; }
        public string Bkash { get; set; }
        public string Roket { get; set; }


        public string DVMRegiNo { get; set; }
        public string KhamarType { get; set; }


        //For online/Offline

        public bool IsOnline { get; set; }
        public DateTime LastUpdated { get; set; }

        public string? PhotoUrl { get; set; }


        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

        [NotMapped]
        public string? BreifPhotoName { get; set; }


        // for Profile signature
        public string? PhotoUrlsig { get; set; }


        [Display(Name = "Profile Photosig")]
        [NotMapped]
        public IFormFile ProfilePhotosig { get; set; }

        [NotMapped]
        public string? BreifPhotoNamesig { get; set; }


        [ForeignKey("FacilityRegistry")]
        public int? FacilityRegistryId { get; set; }
        public virtual FacilityRegistry FacilityRegistry { get; set; }

    }
}
