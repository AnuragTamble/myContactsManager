using ContactWebModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactModels
{
    public  class Contacts
    {
        public int Id { get; set; }

        [Display(Name = "Insert First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(ContactManagerCon.MAX_FIRST_NAME_LENGTH)]
        public string FirstName { get; set; }

        [Display(Name = "Insert Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(ContactManagerCon.MAX_LAST_NAME_LENGTH)]
        public string LastName { get; set; }



        [Display(Name = "Insert Email id")]
        [Required(ErrorMessage = "Email id  is Required")]
        [StringLength(ContactManagerCon.MAX_EMAIL_LENGTH)]
        [RegularExpression("(^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$)",
        ErrorMessage = "Please enter correct email address")]
        public string email { get; set; }


        [Display(Name = "Insert Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is Required")]
        [StringLength(ContactManagerCon.MAX_PHONE_LENGTH)]
        [Phone(ErrorMessage = "Invalid Mobile Number")]
        public string PhonePrimary { get; set; }

        [Display(Name = "Insert Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is Required")]
        [StringLength(ContactManagerCon.MAX_PHONE_LENGTH)]
        [Phone(ErrorMessage = "Invalid Mobile Number")]
        public string PhoneSecondary { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Street Address Line1")]
        [StringLength(ContactManagerCon.MAX_STREET_ADDRESS_LENGTH)]
        public string StreetAdd1 { get; set; }


        [Display(Name = "Street Address Line2")]
        [StringLength(ContactManagerCon.MAX_STREET_ADDRESS_LENGTH)]
        public string StreetAdd2 { get; set; }


        [Display(Name = "Insert City")]
        [StringLength(ContactManagerCon.MAX_CITY_LENGTH)]
        public string city { get; set; }


        [Display(Name = "State Id")]
        [Required(ErrorMessage = "State id is required")]
        public int StateId { get; set; }


        [Display(Name = "Insert zip code")]
        [Required(ErrorMessage = "Zip code is required")]
        [StringLength(ContactManagerCon.MAX_ZIP_CODE_LENGTH)]
        [RegularExpression("(^\\d{6}(-\\d{5})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$)", ErrorMessage = "Zip code is Invalid")]
        public string Zip { get; set; }

        public virtual States? State { get; set; }


        [Required(ErrorMessage = "Required user id")]
        public int userId { get; set; }


        [Display(Name = "Name")]
        public string FriendlyName => $"{FirstName} {LastName}";

        [Display(Name = "Address")]
        public string FriendlyAddress => State is null ? "" :
            string.IsNullOrWhiteSpace(StreetAdd1) ? $"{city}, {State.Abbrevation},{Zip}" :
            string.IsNullOrWhiteSpace(StreetAdd2) ? $"{StreetAdd1},{city},{State.Abbrevation},{Zip}" :
            $"{StreetAdd1} - {StreetAdd2} ,{city}, {State.Abbrevation}, {Zip}";





    }
}
