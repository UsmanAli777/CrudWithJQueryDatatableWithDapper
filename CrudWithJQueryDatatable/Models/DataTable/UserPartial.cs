namespace CrudWithJQueryDatatable.Models.DataTable
{
    public partial class UserPartial
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string roles_list { get; set; }
    }

    public partial class UserPartial
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}