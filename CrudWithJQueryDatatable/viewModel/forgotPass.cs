using System.ComponentModel.DataAnnotations;

namespace CrudWithJQueryDatatable.viewModel
{
    public class forgotPass
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email required")]
        public string email { get; set; }

        public string ResetPasswordCode { get; set; }
    }
}