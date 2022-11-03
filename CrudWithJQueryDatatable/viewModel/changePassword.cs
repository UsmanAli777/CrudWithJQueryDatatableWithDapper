using System.ComponentModel.DataAnnotations;

namespace CrudWithJQueryDatatable.viewModel
{
    public class changePassword
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Old Password Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password Required")]
        [DataType(DataType.Password)]
        public string Newpassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [Compare("Newpassword")]
        public string confirmpassword { get; set; }
    }
}