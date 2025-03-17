using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string Road { get; set; }
        public string Postcode { get; set; }
        public string Area { get; set; }

        public string Upazila { get; set; }

        public string District { get; set; }

        public string Division { get; set; }

        public string Country { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
