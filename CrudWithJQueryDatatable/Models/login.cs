using System.ComponentModel.DataAnnotations;

namespace CrudWithJQueryDatatable.Models
{
    public class login
    {
        [Key]
        public int id { get; set; }

        public string username { get; set; }

        public string gender { get; set; }

        public string email { get; set; }

        public string password { get; set; }
        public bool IsVerify { get; set; }

        public string image { get; set; }
        public string ResetPasswordCode { get; set; }
    }
}