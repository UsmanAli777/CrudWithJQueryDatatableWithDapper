using CrudWithJQueryDatatable.data;
using CrudWithJQueryDatatable.Models.DataTable;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Web;

namespace CrudWithJQueryDatatable.services
{
    public class Generic : IGeneric
    {
        private readonly DapperRepo _dapperRepo;
        
        public Generic(DapperRepo dapperRepo)
        {
            _dapperRepo = dapperRepo;
            
        }
        //Datatable
        public async Task<List<UserPartial>> GetUserAsync(ListingRequest request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("SearchValue", request.SearchValue, DbType.String);
                parameters.Add("PageNo", request.PageNo, DbType.Int32);
                parameters.Add("PageSize", request.PageSize, DbType.Int32);
                parameters.Add("SortColumn", request.SortColumn, DbType.Int32);
                parameters.Add("SortDirection", request.SortDirection, DbType.String);
                return _dapperRepo.ReturnList<UserPartial>("GetAllUserSP", parameters).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}