using System;
using System.Linq;
using APIAssignment2.Data.Abstract;
using APIAssignment2.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using APIAssignment2.Models.Enums;
using Newtonsoft.Json;
using AutoMapper;

namespace APIAssignment2.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _imapper;
        public EmployeeController(IEmployeeRepository employeeRepository,                                   
                                  IDepartmentRepository departmentRepository,
                                  IMapper imapper)
        {
            this._employeeRepository = employeeRepository;
            this._departmentRepository = departmentRepository;
            this._imapper = imapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            
            var employees = JsonConvert.SerializeObject(_employeeRepository
                .AllIncluding(p => p.Department)
                .Where(rs => rs.RecordStatus == RecordStatus.Active), Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            if (employees!=null)
                return Ok(employees);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = JsonConvert.SerializeObject(_employeeRepository.GetSingle(p => p.Id == id, dep => dep.Department), Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }); 
            if (employee != null) return Ok(employee);

            return NotFound();
        }

        
        [HttpPost]
        public IActionResult Post([FromBody]  EmployeeBase employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var _employee = employee;
                if (_employee == null) throw new ArgumentNullException(nameof(_employee));
                Employee employeemap = _imapper.Map<EmployeeBase, Employee>(_employee);
                _employeeRepository.Add(employeemap);
                _employeeRepository.Commit();
                
                if (_employee == null) throw new ArgumentNullException(nameof(_employee));

                return Created($"/api/employee/{employeemap.Id}", employeemap);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EmployeeBase employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Employee Name should not be more than 30 characters");
                }
                var _employee = _employeeRepository.GetSingle(id);
                if (_employee == null)
                {
                    throw new Exception($"Record not found");
                }
                else
                {
                    _employee.Emp_Name = employee.Emp_Name;
                    _employee.Emp_Age = employee.Emp_Age;
                    _employee.Emp_Salary = employee.Emp_Salary;
                    _employee.RecordStatus = employee.RecordStatus;
                    _employee.DepartmentId = employee.DepartmentId;
                    _departmentRepository.Commit();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _employeeRepository.GetSingle(id);

                if (employee == null) return new NotFoundResult();

                _employeeRepository.Delete(employee);
                _employeeRepository.Commit();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
