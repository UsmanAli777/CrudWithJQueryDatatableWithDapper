using CrudWithJQueryDatatable.data;
using CrudWithJQueryDatatable.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CrudWithJQueryDatatable.services
{
    //public class RoleServices : IRoleServices
    //{
    //    private readonly IDapperRepo _dapperRepo;

    //    public RoleServices(IDapperRepo dapperRepo)
    //    {
    //        _dapperRepo = dapperRepo;
    //    }

    //    public int AddRole(Role model)
    //    {
    //        Dapper.DynamicParameters param = new DynamicParameters();
    //        param.Add("@RId", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
    //        param.Add("@RName", model.R_Name);

    //        var result = _dapperRepo.CreateUserReturnInt("dbo.AddRole", param);
    //        if (result > 0)
    //        { }
    //        return result;
    //    }

    //    public int UpdateRole(Role model)
    //    {
    //        Dapper.DynamicParameters param = new DynamicParameters();
    //        param.Add("@RId", model.R_Id);
    //        param.Add("@RName", model.R_Name);

    //        var result = _dapperRepo.CreateUserReturnInt("dbo.UpdateRole", param);
    //        if (result > 0)
    //        { }
    //        return result;
    //    }

    //    public Role GetRoleById(int Id)
    //    {
    //        Dapper.DynamicParameters param = new DynamicParameters();
    //        param.Add("@RId", Id);
    //        var user = _dapperRepo.ReturnList<Role>("dbo.GetUserByRoleId", param).FirstOrDefault();

    //        return user;
    //    }

    //    public int DeleteRole(int Id)
    //    {
    //        Dapper.DynamicParameters param = new DynamicParameters();
    //        param.Add("@RId", Id);
    //        var user = _dapperRepo.CreateEmployeeReturnInt("dbo.DeleteRole", param);

    //        return user;
    //    }

    //    public IEnumerable<Role> GetAllRole()
    //    {
    //        List<Role> role = new List<Role>();
    //        role = _dapperRepo.ReturnList<Role>("GetAllRole").ToList();
    //        return (role);
    //    }

    //    public IEnumerable<login> UserList(login model)
    //    {
    //        Dapper.DynamicParameters param = new DynamicParameters();

    //        param.Add("@Name", model.username);
    //        param.Add("@Email", model.email);
    //        return _dapperRepo.ReturnList<login>("dbo.GetUserByRole", param);
    //    }
    //}
}