using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EMS.Models
{
    public class Settings
    {

        // Setting Model 
        public int SettingsId { get; set; }
        public string Notice { get; set; }
        public string NoticeColor { get; set; }
        public string NoticeFontSize { get; set; }
        public string NoticeSpeed { get; set; }

        public string HedGonoproja { get; set; }
        public string GonoFontSize { get; set; }
        public string GonoColor { get; set; }

        public string UnionName { get; set; }
        public string UnionColor { get; set; }
        public string UnionFontSize  { get; set; }

        public string UpozelaName { get; set; }
        public string UpozelaFontSize { get; set; }
        public string UpozelaColor { get; set; }

        public string ZelaName { get; set; }
        public string ZelaFontSize { get; set; }
        public string ZelaColor { get; set; }

        public string EmailAddress { get; set; }
        public string EmailFontSize { get; set; }
        public string EmailColor { get; set; }

        public string WebSite { get; set; }

        public string ChairmanName { get; set; }
        public string ChairmanFontSize { get; set; }
        public string ChairmanColor { get; set; }
       
        public string UnionFooterSize { get; set; }
        public string UpozelaZelaFooterFontSize { get; set; }


        public string BorderColor { get; set; }
        public string BorderSize    { get; set; }
        public string wardNo { get; set; }
        public string barcodelink { get; set; }



        public string? HeadLPhotoUrl { get; set; }

        [Display(Name = "HeadLProfile Photo")]
        [NotMapped]
        public IFormFile HeadLProfilePhoto { get; set; }

        [NotMapped]
        public string? HeadLBreifPhotoName { get; set; }




        public string? HeadRPhotoUrl { get; set; }


        [Display(Name = "HeadRProfile Photo")]
        [NotMapped]
        public IFormFile HeadRProfilePhoto { get; set; }

        [NotMapped]
        public string? HeadRBreifPhotoName { get; set; }




        public string? LoginPhotoUrl { get; set; }


        [Display(Name = "LoginProfile Photo")]
        [NotMapped]
        public IFormFile LoginProfilePhoto { get; set; }

        [NotMapped]
        public string? LoginBreifPhotoName { get; set; }


    }
}
