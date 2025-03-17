using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class FollowUp
    {
        [Key]
        public int FollowUpId { get; set; }
        public string FollowUpName { get; set; }
        public string FollowUpUserName  { get; set; }
    }
}
