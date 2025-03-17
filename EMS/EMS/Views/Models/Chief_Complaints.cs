using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Chief_Complaints
    {
        [Key]
        public int ChiefComplaintsId { get; set; }
        public string ChiefComplaintsName { get; set; }
        public string Chief_User_Name { get; set; }
    }
}
