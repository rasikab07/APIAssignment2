using APIAssignment2.Data.Abstract;
using APIAssignment2.Data.Base;
using APIAssignment2.Models.Entities;

namespace APIAssignment2.Data.Repositories
{
    public class DepartmentRepository : EntityBaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ManageEmployeesContext context) : base(context) { }
    }
}
