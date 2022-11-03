using System;
using System.ComponentModel.DataAnnotations;

namespace CrudWithJQueryDatatable.viewModel
{
    public class ResetPassword
    {
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password Required")]
        [DataType(DataType.Password)]
        public string Newpassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [Compare("Newpassword")]
        public string confirmpassword { get; set; }

        [Required]
        public String ResetCode { get; set; }
    }
}