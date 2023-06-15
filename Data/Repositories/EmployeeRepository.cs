using APIAssignment2.Data.Abstract;
using APIAssignment2.Data.Base;
using APIAssignment2.Models.Entities;

namespace APIAssignment2.Data.Repositories
{
    public class EmployeeRepository : EntityBaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ManageEmployeesContext context) : base(context) { }
    }
}
