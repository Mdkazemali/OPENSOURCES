using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class Registrations
    {
        [Key]
        public int RegiId { get; set; }
        public Guid RegistrationGUID { get; set; }
        public string PatientName { get; set; }
        public DateTime DOB { get; set; }
        public int PatientAge { get; set; }
        public string Gender { get; set; }
        public string PtPhone { get; set; }
      
        public DateTime RegistrationDate { get; set; }= DateTime.Now;

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }


    }
}
