using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LengerProje.Models
{
    public class User
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        [EmailAddress]
        [Required(ErrorMessage ="Email alanı boş geçilemez")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password alanı boş geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username alanı boş geçilemez")]
        public string Username { get; set; }
    }
}