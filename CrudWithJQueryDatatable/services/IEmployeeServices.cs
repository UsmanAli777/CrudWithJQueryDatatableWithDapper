using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.Models.DataTable;

namespace CrudWithJQueryDatatable.services
{
    public interface IEmployeeServices
    {
        DataTableResponse<EmployeePartial> GetAllEmployeeDT(DataTableRequest request);
        int DeleteEmployee(int id);
        int AddEmployee(Employee model);
        Employee GetEmployeeById(int id);
        int UpdateEmployee( Employee model);
    }
}