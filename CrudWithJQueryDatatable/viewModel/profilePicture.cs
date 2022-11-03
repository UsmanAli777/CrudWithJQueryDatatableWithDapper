using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CrudWithJQueryDatatable.viewModel
{
    public class profilePicture
    {
        [Required(ErrorMessage = "please upload picture.")]
        public string image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}