using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labb2._2.Model
{
    public class Contact
    {
        
        public int ContactID
        {
            get;
            set;
        }

        [Required(ErrorMessage="Du måste ange en Email")]
        [StringLength(50)]
        public string EmailAdress
        {
            get;
            set;
        }

        [Required(ErrorMessage="Du måste ange ett Namn")]
        [StringLength(50)]
        public string FirstName
        {
            get;
            set;
        }

        [Required(ErrorMessage="Du måste ange ett Efternamn")]
        [StringLength(50)]
        public string LastName
        {
            get;
            set;
        }

    }
}