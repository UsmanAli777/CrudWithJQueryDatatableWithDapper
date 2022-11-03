using Dapper;
using System.Collections.Generic;

namespace CrudWithJQueryDatatable.data
{
    public interface IDapperRepo
    {
        T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters param = null);

        IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters param = null);

        int CreateEmployeeReturnInt(string StoredProcedure, DynamicParameters param = null);

        int CreateUserReturnInt(string StoredProcedure, DynamicParameters param = null);
    }
}