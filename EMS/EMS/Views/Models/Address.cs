using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string Villages { get; set; }
        public string UnionName { get; set; }
        public string ThanaName { get; set; }
        public string DistrictName { get; set; }
        public string DivissionName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode =false)]
        public DateTime AddressDate { get; set; }
        public string AddressUserName { get; set; }
    }
}
