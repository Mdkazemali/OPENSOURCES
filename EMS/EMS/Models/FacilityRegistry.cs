using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class FacilityRegistry
    {
        [Key]
        public int FacilityId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string FacilityEmail { get; set; }
        public string FacilityMobile { get; set; }
        public string FacilityHeadInfomations { get; set; }
        public string DivisionName { get; set; }
        public string DistricName { get; set; }
        public string CityCorporationName { get; set; }
        public string UpozillaName { get; set; }
        public string UnionName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdateDate { get; set; }
        public string FaUserName { get; set; }



        public string? FalPhotoUrl { get; set; }


        [Display(Name = "FalProfile Photo")]
        [NotMapped]
        public IFormFile FalProfilePhoto { get; set; }

        [NotMapped]
        public string? FalBreifPhotoName { get; set; }


        public string? FarPhotoUrl { get; set; }


        [Display(Name = "FarProfile Photo")]
        [NotMapped]
        public IFormFile FarProfilePhoto { get; set; }

        [NotMapped]
        public string? FarBreifPhotoName { get; set; }
    }
}
