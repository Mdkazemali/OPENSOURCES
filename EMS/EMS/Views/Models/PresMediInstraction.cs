using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class PresMediInstraction
    {
        [Key]
        public int MediInstrac { get; set; }
        public string MediInstracName { get; set; }
        public string MediInsUserName { get; set; }
    }
}
