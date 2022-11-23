using CrudWithJQueryDatatable.data;
using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.Models.DataTable;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CrudWithJQueryDatatable.services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly DapperRepo _dapperRepo;
        private readonly Generic _generic;

        public EmployeeServices(DapperRepo dapperRepo, Generic generic)
        {
            _dapperRepo = dapperRepo;
            _generic = generic;
        }
        public DataTableResponse<EmployeePartial> GetAllEmployeeDT(DataTableRequest request)
        {
            var req = new DTReq()
            {
                StartRowIndex = request.Start,
                PageSize = request.Length,
                SortExpression = request.Order[0].Dir,
                SearchText = request.Search != null ? request.Search.Value.Trim() : ""
            };

            return _generic.GetAllEmployeeMultiple(req);
        }
        public int AddEmployee(Employee model)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@Name", model.Name);
            param.Add("@Position", model.Position);
            param.Add("@Office", model.Office);
            param.Add("@Age", model.Age);
            param.Add("@Salary", model.Salary);
            return _dapperRepo.CreateEmployeeReturn("dbo.AddEmployee", param);
        }
        public Employee GetEmployeeById(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            var user = _dapperRepo.ReturnList<Employee>("dbo.GetEmployeeById", param).FirstOrDefault();
            return user;
        }
        public int UpdateEmployee(Employee model)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@EmployeeId", model.EmployeeId);
            parameter.Add("@Name", model.Name);
            parameter.Add("@Position", model.Position);
            parameter.Add("@Office", model.Office);
            parameter.Add("@Age", model.Age);
            parameter.Add("@Salary", model.Salary);
            return _dapperRepo.CreateEmployeeReturnInt("UpdateEmployee", parameter);
        }
        public int DeleteEmployee(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            var user = _dapperRepo.CreateUserReturnInt("dbo.DeleteEmployee", param);
            return user;
        }
    }
}