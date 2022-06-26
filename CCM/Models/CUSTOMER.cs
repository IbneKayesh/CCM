using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCM.Models
{
    public class CUSTOMER
    {
        public string ID { get; set; }

        //[Display(Name = "Country Code")]
        //[Required(ErrorMessage = "{0} is required")]
        public string COUNTRY_CODE { get; set; }

        [Display(Name = "Contact")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(11, ErrorMessage = "{0} length is {1}", MinimumLength = 11)]
        [DataType(DataType.PhoneNumber)]
        public string CUSTOMER_CONTACT { get; set; }

        [Display(Name = "Your Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string CUSTOMER_NAME { get; set; }
        
    }
}