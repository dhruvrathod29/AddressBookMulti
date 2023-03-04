using System.ComponentModel.DataAnnotations;

namespace AddressBookMulti.Areas.CON_Contact.Models
{
    public class CON_ContactModel
    {
        public int? ContactID { get; set; }

        [Required(ErrorMessage = "Please select Country")]
        public int CountryID { get; set; }


        [Required(ErrorMessage = "Please select State")]
        public int StateID { get; set; }


        [Required(ErrorMessage = "Please select City")]
        public int CityID { get; set; }


        [Required(ErrorMessage = "Please select Contact Category")]
        public int ContactCategoryID { get; set; }



        [Required(ErrorMessage = "Please enter Contact name")]
        [StringLength(10, MinimumLength = 3)]
        public string ContactName { get; set; }
        public string CountryName { get; set; }
        public String StateName { get; set; }
        public String CityName { get; set; }
        public string ContactCategory { get; set; }


        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Please enter Pincode")]
        public string PinCode { get; set; }


        [Required(ErrorMessage = "Please enter Mobile No")]
        public string MobileNo { get; set; }


        [Required(ErrorMessage = "Please enter Alternet Contact")]
        public string AlternetContact { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter BirthDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter LinkedIn")]
        public string? LinkedIn { get; set; }

        [Required(ErrorMessage = "Please enter Twitter")]
        public string Twitter { get; set; }

        [Required(ErrorMessage = "Please enter Instagram")]
        public string Insta { get; set; }

        [Required(ErrorMessage = "Please enter Gender")]
        public string Gender { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }

    }
}
