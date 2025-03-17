using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class DosesUnit
    {
        [Key]
        public int DosesId { get; set; }
        public string DosesName { get; set; }
        public string DosesUserName { get; set; }

    }
}
