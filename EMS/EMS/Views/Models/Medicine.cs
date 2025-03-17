using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineGenericName { get; set; }
        public string MedicineType { get; set; }
     
        public string MediUserName { get; set; }
        
    }
}
