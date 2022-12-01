using CrudWithJQueryDatatable.Models.DataTable;

namespace CrudWithJQueryDatatable.services
{
    public interface IGeneric
    {
        //Task<List<UserPartial>> GetUserAsync(ListingRequest request);
        DataTableResponse<UserPartial> GetAllUserMultiple(DTReq request);

        DataTableResponse<EmployeePartial> GetAllEmployeeMultiple(DTReq request);
    }
}