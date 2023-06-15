using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APIAssignment2.Models;
using APIAssignment2.Models.Enums;
using Newtonsoft.Json;

namespace APIAssignment2.Models.Entities
{
    public class EmployeeBase 
    {

        [StringLength(30, ErrorMessage = "Employee Name should be maximum 30 length")]
        [JsonProperty("Emp_Name")]
        public string Emp_Name { get; set; } = string.Empty;
        
        [Range(21,100, ErrorMessage = "Employee age is in between 21-100")]
        public int Emp_Age { get; set; }
        public RecordStatus RecordStatus { get; set; }

        [ForeignKey("Department")]
        [JsonProperty("departmentId")]
        public int DepartmentId { get; set; }
        public decimal Emp_Salary { get; set; }
    }
}
