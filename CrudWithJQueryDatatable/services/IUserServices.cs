using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.viewModel;
using System.Collections.Generic;

namespace CrudWithJQueryDatatable.services
{
    public interface IUserServices
    {
        int AddUser(login model);

        /// <summary>
        /// this outputs login as objext
        /// </summary>
        /// <param name="username"></param>
        /// <returns>login class</returns>
        login GetUserByRoleIdForLogin(string username);

        login userLogin(string username);

        login updatePassword(int id, string password);

        string CreatePasswordHash(string password);

        bool VerifyPasswordHash(string dbpassword, string password);

        int updateImage(int id, string image);

        login GetUserByEmail(string email);

        login saveResetPasswordCode(string email, string ResetPasswordCode);

        login GetResetPasswordCode(string ResetPasswordCode);

        int ResetPassword(int id, string password, string resetpasswordcode);

        int EmptyResetPassword(int id, bool IsVerify, string resetpasswordcode);

        IEnumerable<UserRolePartial> UserRoleList(int id);

        IEnumerable<UserRolePartial> UserRoleById(int id);

        //IEnumerable<UserDetail> GetUserByRole(UserDetail model);
        //Task<DataTableResponse<UserPartial>> GetAllUserAsync(DataTableRequest request);

        //int AddRole(Role model);
        login GetUserById(int id);

        //List<UserRoleEdit> GetAllRole(int uId);
        //int AddUserRole(int userId, int roleId);
        //void RemoveUserRole(int userId, int roleId);
        //int DeleteUser(int id);
    }
}