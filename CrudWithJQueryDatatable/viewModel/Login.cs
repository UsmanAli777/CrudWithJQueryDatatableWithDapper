using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudWithJQueryDatatable.viewModel
{
    public class Login
    {
        [Key]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}