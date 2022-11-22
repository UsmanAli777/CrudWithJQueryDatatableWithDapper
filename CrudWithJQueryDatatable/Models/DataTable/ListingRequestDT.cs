using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWithJQueryDatatable.Models.DataTable
{
    public class ListingRequestDT
    {
        public string SearchText { get; set; }
        public string SortExpression { get; set; } = "asc";
        public int StartRowIndex { get; set; }
        public int PageSize { get; set; }
    }
}