using CrudWithJQueryDatatable.Models.DataTable;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudWithJQueryDatatable.data
{
    public interface IDapperRepo
    {
        T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters param = null);

        IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters param = null);

        //int CreateEmployeeReturnInt(string StoredProcedure, DynamicParameters param = null);
        int CreateEmployeeReturnInt(string StoredProcedure, DynamicParameters param = null);
        int CreateEmployeeReturn(string StoredProcedure, DynamicParameters param = null);
        int CreateUserReturnInt(string StoredProcedure, DynamicParameters param = null);

        int CreateUserReturnFKInt(string StoredProcedure, DynamicParameters param = null);

        int CreateUserReturn(string StoredProcedure, DynamicParameters param = null);
        DataTableResponse<T> ReturnListMultiple<T>(string procrdureName, DynamicParameters param = null);
    }
}