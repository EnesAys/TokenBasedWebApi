using LengerProje.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LengerProje.Models
{
    public class Text
    {

        [Required(ErrorMessage ="Title Alanı Boş Bırakılamaz")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content Alanı Boş Bırakılamaz")]
        public string Content { get; set; }
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "TextTypeID Boş Bırakılamaz")]
        public int TextTypeID { get; set; }
        public bool? IsRelease { get; set; }

        public virtual TextTypes TextTypes { get; set; }
        public virtual Users Users { get; set; }
    }
}