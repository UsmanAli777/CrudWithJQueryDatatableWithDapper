using CrudWithJQueryDatatable.data;
using CrudWithJQueryDatatable.Models;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;

namespace CrudWithJQueryDatatable.services
{
    public class UserServices : IUserServices
    {
        private readonly DapperRepo _dapperRepo;

        public UserServices()
        {
            _dapperRepo = new DapperRepo();
        }

        public int AddUser(login model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@username", model.username);
            param.Add("@gender", model.gender);
            param.Add("@email", model.email);
            param.Add("@password", model.password);
            param.Add("@image", model.image);
            param.Add("@ResetPasswordCode", model.ResetPasswordCode);
            var result = _dapperRepo.CreateUserReturnInt("dbo.AddUser", param);
            if (result > 0)
            {
            }
            return result;
        }

        public login userLogin(string username)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@username", username);
            return _dapperRepo.ReturnList<login>("GetUser", parameter).FirstOrDefault();
        }

        public login updatePassword(int id, string password)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", id);
            parameter.Add("@password", password);
            return _dapperRepo.ReturnList<login>("PasswordChange", parameter).FirstOrDefault();
        }

        public string CreatePasswordHash(string password)
        {
            var hmac = new HMACSHA512();

            byte[] passwordSalt = passwordSalt = hmac.Key;
            byte[] passwordHash = passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            string Passalt = Convert.ToBase64String(passwordSalt);
            string Pashash = Convert.ToBase64String(passwordHash);

            var createHash = Pashash + ":" + Passalt;
            return createHash;
        }

        public bool VerifyPasswordHash(string dbpassword, string password)
        {
            string[] passwordarry = dbpassword.Split(':');
            byte[] orignalhash = Convert.FromBase64String(passwordarry[1]);
            using (var hmac = new HMACSHA512(orignalhash))
            {
                var verifyHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var orignalsalt = Convert.FromBase64String(passwordarry[0]);
                return verifyHash.SequenceEqual(orignalsalt);
            }
        }

        public int updateImage(int id, string image)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", id);
            parameter.Add("@image", image);
            return _dapperRepo.CreateUserReturnInt("UserUpdateImage", parameter);
        }

        public login GetUserByEmail(string email)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@email", email);
            return _dapperRepo.ReturnList<login>("GetUserByEmail", parameter).FirstOrDefault();
        }

        public login saveResetPasswordCode(string email, string ResetPasswordCode)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@email", email);
            parameter.Add("@resetpasswordcode", ResetPasswordCode);
            return _dapperRepo.ReturnList<login>("saveResetPasswordCode", parameter).FirstOrDefault();
        }

        public login GetResetPasswordCode(string ResetPasswordCode)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", ResetPasswordCode);
            return _dapperRepo.ReturnList<login>("GetResetPasswordCode", parameter).FirstOrDefault();
        }

        public int ResetPassword(int id, string password, string resetpasswordcode)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", id);
            parameter.Add("@password", password);
            parameter.Add("@resetpasswordcode", resetpasswordcode);
            return _dapperRepo.CreateUserReturnInt("ResetPassword", parameter);
        }
        public int EmptyResetPassword(int id, bool IsVerify, string resetpasswordcode )
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", id);
            parameter.Add("@isVerify", IsVerify);
            parameter.Add("@resetpasswordcode", resetpasswordcode);
            return _dapperRepo.CreateUserReturnInt("EmptyResetPassword", parameter);
        }
    }
}