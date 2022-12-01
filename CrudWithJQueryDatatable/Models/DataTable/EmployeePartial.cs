namespace CrudWithJQueryDatatable.Models.DataTable
{
    public partial class EmployeePartial
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string SubjectName { get; set; }
    }

    public partial class EmployeePartial
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}