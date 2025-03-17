using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Diagnosis
    {
        [Key]
        public int DiagId { get; set; }
        public string DiagName { get; set; }
        public string DiagUserName { get; set; }
    }
}
