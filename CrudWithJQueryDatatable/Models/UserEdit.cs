using System.ComponentModel.DataAnnotations;

namespace CrudWithJQueryDatatable.Models
{
    public class UserEdit
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserEdit()
        { }

        public UserEdit(login users)
        {
            Email = users.email;
            Password = users.password;
        }
    }
}