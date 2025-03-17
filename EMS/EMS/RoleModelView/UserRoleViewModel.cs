using Microsoft.AspNetCore.Identity;
using EMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMS
{
    public class UserRoleViewModel
    {

        public string UserName { get; set; }
        public List<IdentityRole> Roles { get; set; }

        public List<string> UserRoles { get; set; }
        public List<string> SelectedRoles { get; set; }


        // UserRoles

        public string Id { get; set; }
        public string RoleName { get; set; }

        public int RoleCount { get; set; }

        // For UserRoles

        public string UserId { get; set; }
        public List<d> VideoDataList { get; set; }

        //for User INformation 

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
        public string DVMRegiNo { get; set; }
        public string KhamarType { get; set; }
        public string? PhotoUrl { get; set; }
        public string WonPhotoUrl { get; set; }

        public int OccupationId { get; set; }
        public string Ocupations { get; set; }
        public string OcuPaUserName { get; set; }


        //for status 
        public bool IsOnline { get; set; }
        public DateTime LastUpdated { get; set; }

        public int ServicesId { get; set; }
        public string ServiceUserName { get; set; }

        public string ServiceTranjectionId { get; set; }
        public string Confirmation { get; set; }

        public int TotalDays { get; set; }
        public int TotalAmount { get; set; }

        public string? TranjPhotoUrl { get; set; }

        [Display(Name = "TranjProfile Photo")]
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







