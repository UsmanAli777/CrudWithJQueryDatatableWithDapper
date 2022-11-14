using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWithJQueryDatatable.viewModel
{
    public partial class UserRolePartial
    {
        public int id { get; set; }
        public string username { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string image { get; set; }
        public string ResetPasswordCode { get; set; }
        public string IsVerify { get; set; }
    }
    public partial class UserRolePartial
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public partial class UserRolePartial
    {
        public int R_Id { get; set; }
        public string R_Name { get; set; }
    }
}