using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APIAssignment2.Models;
using APIAssignment2.Models.Enums;
using Newtonsoft.Json;

namespace APIAssignment2.Models.Entities
{
    public class Employee : IEntityBase
    {
       

        [Key,Required]
        public int Id { get; set; }


        [StringLength(30)]
        [JsonProperty("Emp_Name")]
        public string Emp_Name { get; set; } = string.Empty;

        [Range(21, 100, ErrorMessage = "Employee age is in between 21-100")]
        public int Emp_Age { get; set; }
        public RecordStatus RecordStatus { get; set; }

        [ForeignKey("Department")]
        [JsonProperty("departmentId")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public decimal Emp_Salary { get; set; }
    }
}
