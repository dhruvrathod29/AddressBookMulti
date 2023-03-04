using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBookMulti.Areas.LOC_City.Models
{
    #region LOC_CityModel
    public class LOC_CityModel
    {
        public int? CityID { get; set; }


        [Required(ErrorMessage = "Please select State")]
        public int StateID { get; set; }


        [Required(ErrorMessage = "Please select Country")]
        public int CountryID { get; set; }

        public string StateName { get; set; }

        [Required(ErrorMessage = "Please enter City name")]
        [DisplayName("City Name")]
        [StringLength(10, MinimumLength = 3)]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Please enter Pincode")]
        [DisplayName("Pincode")]
        public string PinCode { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    #endregion

    #region LOC_City_SelectForDropDownModel
    public class LOC_City_SelectForDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
    #endregion
}
