namespace CrudWithJQueryDatatable.Models.DataTable
{
    public class DTReq
    {
        public string SearchText { get; set; }
        public string SortExpression { get; set; }
        public int StartRowIndex { get; set; }
        public int PageSize { get; set; }
        public int SubjectId { get; set; }
    }
}