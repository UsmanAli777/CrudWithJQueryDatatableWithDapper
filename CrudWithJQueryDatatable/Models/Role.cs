using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudWithJQueryDatatable.Models
{
    public class Role
    {
        public int R_Id { get; set; }

        [DisplayName("Role Name")]
        [Required(ErrorMessage = "Required")]
        public string R_Name { get; set; }
    }
}