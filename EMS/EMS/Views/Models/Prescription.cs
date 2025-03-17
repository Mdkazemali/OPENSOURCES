using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EMS.Models
{
    public class Prescription
    {
        //For Prescriptions Wone
        [Key]
        public int PrescripId { get; set; }
        public string PrescriptionName { get; set; }
        public string PrescriptionTemp { get; set; }

        public DateTime? PrescriptionDate { get; set; }
        public string PresUserName { get; set; }


        //For Chief Complaints

        [ForeignKey("Chief_Complaints")]
        public int PresChipId { get; set; }
        public virtual Chief_Complaints ChiefComplaints { get; set; }
        public int ChiefCompValues { get; set; }
        public string ChiefCompDuration { get; set; }
        public string ChiefCompNote { get; set; }


        //For General Examinations
        public int Temperature { get; set; }
        public int Pulse { get; set; }
        public string Bpm { get; set; }
        public int RespiratoryRate { get; set; }
        public int Height { get; set; }
        public string LymphNode { get; set; }
        public string Jundice { get; set; }
        public string Thyroid { get; set; }
        public string Spleen { get; set; }
        public string Anemia { get; set; }
        public string Others { get; set; }
        public int BPSystolic { get; set; }
        public int BPDiastolic { get; set; }
        public int OFC { get; set; }
        public int Weight { get; set; }
        public int BMI { get; set; }
        public int BMIStatus { get; set; }
        public string Clubbing { get; set; }
        public string Liver {  get; set; }


        // For Diagnosis

        [ForeignKey("Diagnosis")]
        public int PresDiagId { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }

        public string DiagOrder { get; set; }
        public string DiagCertainty { get; set; }
        public string PresDiagNote { get; set; }

        //For Investigation 

        [ForeignKey("Investigation")]
        public int PresInvId { get; set; }
        public virtual Investigation Investigation { get; set; }
        public string PresInvNote { get; set; }



        //Medication

        public int PresMediId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public int Sokal { get; set; }
        public int Dupur { get; set; }
        public int Rat { get; set; }

        [ForeignKey("DosesUnit")]
        public int PresDosesId { get; set; }
        public virtual DosesUnit DosesUnit { get; set; }

        public int PresMediDuration { get; set; }
        public string PresMediDose { get; set; }

        [ForeignKey("PresMediInstraction")]
        public int PresMediInstractionId { get; set; }
        public virtual PresMediInstraction PresMediInstraction { get; set; }


        public string PresMediNote { get; set; }
        public int PresMediQuantity { get; set; }

        //For Advices

        [ForeignKey("Advise")]
        public int PresAdvId { get; set; }
        public virtual Advise Advices { get; set; }

        //For follow Ups
        [ForeignKey("FollowUp")]
        public int PresFollowId { get; set; }
        public virtual FollowUp Folloups { get; set; }

        // For Referred To

        public string PresReferredTo { get; set; }





    }
}
