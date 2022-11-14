using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWithJQueryDatatable.Models
{
    public class RoleEdit
    {
        public string roles_list { get; set; }
        public IEnumerable<login> Members { get; set; }
        public IEnumerable<login> NonMembers { get; set; }
        public string RoleName { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}