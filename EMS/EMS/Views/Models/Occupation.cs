using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Occupation
    {
        [Key]
        public int OccupationId { get; set; }
        public string Ocupations { get; set; }

        public int OccupationPrices { get; set; }
        public string OcuPaUserName { get; set; }

    }
}
