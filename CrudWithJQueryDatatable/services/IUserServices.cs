using CrudWithJQueryDatatable.Models;

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
    }
}