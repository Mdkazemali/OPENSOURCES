using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Advise
    {
        [Key]
        public int Advicesid { get; set; }
        public string AdviceName { get; set; }
        public string AdvUserName { get; set; }
    }
}
