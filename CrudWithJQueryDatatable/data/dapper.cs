using CrudWithJQueryDatatable.Models.DataTable;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CrudWithJQueryDatatable.data
{
    public class DapperRepo : IDapperRepo
    {
        private string connectionString;

        public DapperRepo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DB"].ToString();
        }

        public T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.Execute(procrdureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }

        //DapperORM.RetrunList<EmployeeModel> <= IEnumberable<EmployeeModel>

        public IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procrdureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public int CreateEmployeeReturnInt(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("id");
            }
        }

        public int CreateUserReturnInt(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("id");
            }
        }

        public int CreateUserReturnFKInt(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("UserId");
            }
        }

        public int CreateUserReturn(string StoredProcedure, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("R_Id");
            }
        }

        public DataTableResponse<T> ReturnListMultiple<T>(string procrdureName, DynamicParameters param = null)
        {
            var list = new List<T>();
            int total = 0;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var query = sqlCon.QueryMultiple(procrdureName, param, commandType: CommandType.StoredProcedure))
                {
                    list = query.Read<T>().AsList<T>();
                    if (!query.IsConsumed)
                        total = query.Read<int>().FirstOrDefault();
                }
            }
            return new DataTableResponse<T>()
            {
                data = list,
                recordsFiltered = total,
                recordsTotal = total
            };
        }
    }
}//