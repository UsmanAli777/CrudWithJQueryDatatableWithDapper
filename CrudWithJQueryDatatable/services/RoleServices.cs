using CrudWithJQueryDatatable.data;
using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.Models.DataTable;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CrudWithJQueryDatatable.services
{
    public class RoleServices : IRoleServices
    {
        private readonly DapperRepo _dapperRepo;
        private readonly Generic _generic;

        public RoleServices(DapperRepo dapperRepo, Generic generic)
        {
            _dapperRepo = dapperRepo;
            _generic = generic;
        }

        public IEnumerable<UserDetail> GetUserByRole(UserDetail model)
        {
            List<UserDetail> user = new List<UserDetail>();
            user = _dapperRepo.ReturnList<UserDetail>("dbo.GetUserByRole").ToList();
            return (user);
        }

        public int AddRole(Role model)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@R_Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@R_Name", model.R_Name);
            return _dapperRepo.CreateUserReturn("dbo.AddRole", param);
        }

        public login GetUserById(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            var user = _dapperRepo.ReturnList<login>("dbo.GetUserById", param).FirstOrDefault();
            return user;
        }

        public List<UserRoleEdit> GetAllRole(int uId)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@userid", uId);
            return _dapperRepo.ReturnList<UserRoleEdit>("dbo.GetAllRole", param).ToList();
        }

        public int AddUserRole(int userId, int roleId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@UserId", userId);
            param.Add("@RoleId", roleId);
            return _dapperRepo.CreateUserReturnFKInt("AddUserRole", param);
        }

        public void RemoveUserRole(int userId, int roleId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", userId);
            param.Add("@RoleId", roleId);
            _dapperRepo.CreateUserReturnFKInt("RemoveUserRole", param);
        }

        public int DeleteUser(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            var user = _dapperRepo.CreateUserReturnInt("dbo.DeleteUser", param);
            return user;
        }

        //public async Task<DataTableResponse<UserPartial>> GetAllUserAsync(DataTableRequest request)
        //{
        //    var req = new ListingRequest()
        //    {
        //        StartRowIndex = request.Start,
        //        PageSize = request.Length,
        //        //SortExpression = request.Order[0].Column,
        //        SortExpression = request.Order[0].Dir,
        //        SearchText = request.Search != null ? request.Search.Value.Trim() : ""
        //    };
        //    var users = await _generic.GetUserAsync(req);
        //    return new DataTableResponse<UserPartial>()
        //    {
        //        Draw = request.Draw,
        //        RecordsTotal = users[0].TotalCount,
        //        RecordsFiltered = users[0].TotalCount,
        //        Data = users.ToArray(),
        //        Error = ""
        //    };
        //}
        public DataTableResponse<UserPartial> GetAllUserDT(DataTableRequest request)
        {
            var req = new DTReq()
            {
                StartRowIndex = request.Start,
                PageSize = request.Length,
                SortExpression = request.Order[0].Dir,
                SearchText = request.Search != null ? request.Search.Value.Trim() : ""
            };

            return _generic.GetAllUserMultiple(req);
        }
    }
}