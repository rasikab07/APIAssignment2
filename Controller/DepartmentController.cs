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

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _imapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, IMapper imapper)
        {
            this._departmentRepository = departmentRepository;
            this._employeeRepository = employeeRepository;
            this._imapper = imapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {


                var departments = JsonConvert.SerializeObject(_departmentRepository.AllIncluding(emp => emp.Employees).Where(rs => rs.RecordStatus == RecordStatus.Active), Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
                if (departments.Contains("null"))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(departments);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {


                var department = JsonConvert.SerializeObject(_departmentRepository.GetSingle(p => p.Id == id, e => e.Employees), Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });

                if (department.Contains("null"))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(department);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody] DepartmentBase department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var _department = department;
                if (_department == null) throw new ArgumentNullException(nameof(_department));
                Department departmentmap = _imapper.Map<DepartmentBase, Department>(_department);
                _departmentRepository.Add(departmentmap);
                _departmentRepository.Commit();
                //_department.Employees = null;

                return Created($"/api/department/{departmentmap.Id}", departmentmap);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DepartmentBase department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Department Name should not be more than 50 characters");
                }
                var _department = _departmentRepository.GetSingle(id);
                if (_department == null)
                {
                    throw new ArgumentNullException(nameof(_department));
                }
                else
                {
                    _department.Dept_Name = department.Dept_Name;
                    _department.RecordStatus = department.RecordStatus;
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
                var department = _departmentRepository.GetSingle(id);
                if (department == null) throw new Exception($"Record not found in the database");
                _departmentRepository.Delete(department);
                _departmentRepository.Commit();

                return Ok(department);
             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
