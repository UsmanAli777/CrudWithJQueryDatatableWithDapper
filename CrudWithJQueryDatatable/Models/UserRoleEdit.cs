using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CrudWithJQueryDatatable.Models
{
    public class UserRoleEdit
    {
        public int R_Id { get; set; }
        public string R_Name { get; set; }
        public bool Checked { get; set; }
    }
}