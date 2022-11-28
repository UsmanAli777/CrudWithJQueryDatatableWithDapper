using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.Models.DataTable;
using System.Collections.Generic;

namespace CrudWithJQueryDatatable.services
{
    public interface IEmployeeServices
    {
        DataTableResponse<EmployeePartial> GetAllEmployeeDT(DataTableRequest request, int sub);

        IEnumerable<EmployeeDetails> GetAllEmployeeRecord(EmployeeDetails model);

        IEnumerable<Subject> GetAllSubject();

        int DeleteEmployee(int id);

        int AddEmployee(Employee model);

        Employee GetEmployeeById(int id);

        int UpdateEmployee(Employee model);
    }
}