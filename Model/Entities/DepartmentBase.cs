using APIAssignment2.Models.Enums;
using APIAssignment2.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAssignment2.Models.Entities
{
    public class DepartmentBase
    {

        [StringLength(50, ErrorMessage = "Department Name should be maximum 50 length")]
        [Required(ErrorMessage = "Department Name is required") ]
        [JsonProperty("Dept_Name")]
        public string Dept_Name { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
