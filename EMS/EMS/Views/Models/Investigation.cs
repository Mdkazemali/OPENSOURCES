using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Investigation
    {
        [Key]
        public int InvestiId { get; set; }
        public string InvestigatorName { get; set; }
        public string InvUserName { get; set; }

    }
}
