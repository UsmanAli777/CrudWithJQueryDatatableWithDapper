using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CrudWithJQueryDatatable.viewModel
{
    public class Signup
    {
        [Key]
        [Required(ErrorMessage = "Required")]
        [Remote("CheckUserNameExists", "Home", ErrorMessage = "Username already exists! please try another Username.")]
        [DisplayName("Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Gender")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}