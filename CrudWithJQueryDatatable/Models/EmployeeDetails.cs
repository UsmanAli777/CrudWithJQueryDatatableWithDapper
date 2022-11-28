using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWithJQueryDatatable.Models
{
    public class EmployeeDetails
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string SubjectName { get; set; }
    }
}