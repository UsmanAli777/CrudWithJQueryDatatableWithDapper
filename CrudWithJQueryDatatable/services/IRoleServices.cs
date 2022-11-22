using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.Models.DataTable;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudWithJQueryDatatable.services
{
    public interface IRoleServices
    {
        IEnumerable<UserDetail> GetUserByRole(UserDetail model);

        int AddRole(Role model);

        login GetUserById(int id);

        List<UserRoleEdit> GetAllRole(int uId);

        int AddUserRole(int userId, int roleId);

        void RemoveUserRole(int userId, int roleId);

        int DeleteUser(int id);
        //Task<DataTableResponse<UserPartial>> GetAllUserAsync(DataTableRequest request);
        DataTableResponse<UserPartial> GetAllUserDT(DataTableRequest request);
    }
}