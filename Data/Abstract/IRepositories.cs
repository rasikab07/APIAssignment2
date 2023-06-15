using APIAssignment2.Models.Entities;

namespace APIAssignment2.Data.Abstract
{
    public interface IEmployeeRepository : IEntityBaseRepository<Employee> { }

    public interface IDepartmentRepository : IEntityBaseRepository<Department> { }
}
