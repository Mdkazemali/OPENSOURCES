using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Trainingvideo
    {
        [Key]
        public int vdoId { get; set; }

        public string VideoName { get; set; }

        public string videoLink { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime CreateDate { get; set; }
        public string Username { get; set; }
    }
}
