using CrudWithJQueryDatatable.data;
using CrudWithJQueryDatatable.Models.DataTable;
using Dapper;
using System;
using System.Data;

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
        //public async Task<List<UserPartial>> GetUserAsync(ListingRequest request)
        //{
        //    try
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("SearchText", request.SearchText, DbType.String);
        //        parameters.Add("SortExpression", request.SortExpression, DbType.String);
        //        parameters.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
        //        parameters.Add("PageSize", request.PageSize, DbType.Int32);
        //        return _dapperRepo.ReturnList<UserPartial>("GetAllUserDT", parameters).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}
        public DataTableResponse<UserPartial> GetAllUserMultiple(DTReq request)
        {
            try
            {
                Dapper.DynamicParameters param = new DynamicParameters();
                param.Add("SearchText", request.SearchText, DbType.String);
                param.Add("SortExpression", request.SortExpression, DbType.String);
                param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
                param.Add("PageSize", request.PageSize, DbType.Int32);

                return _dapperRepo.ReturnListMultiple<UserPartial>("dbo.GetAllUserDT", param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}