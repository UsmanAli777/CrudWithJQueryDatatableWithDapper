using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWithJQueryDatatable.Models
{
    public class UserDetail
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string roles_list { get; set; }
    }
}