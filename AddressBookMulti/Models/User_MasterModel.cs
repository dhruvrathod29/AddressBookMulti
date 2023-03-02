using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBookMulti.Models
{
    public class User_MasterModel
    {
        public int UserID { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }
}
