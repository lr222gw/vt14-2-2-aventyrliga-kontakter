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

        //regex från http://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp
        [Required(ErrorMessage="Du måste ange en Email")]
        [StringLength(50)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage="Du har inte anvigit en giltig Epost-address...")] 
        public string EmailAddress
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