using APIAssignment2.Models.Enums;
using APIAssignment2.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace APIAssignment2.Models.Entities
{
    public class Department : IEntityBase
    {
        
        [Key,Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is invalid.")]
        [StringLength(50)]
        [JsonProperty("Dept_Name")]
        public string Dept_Name { get; set; }
        public RecordStatus RecordStatus { get; set; }
        
        [IgnoreDataMember]
        public ICollection<Employee> Employees { get; set; }
    }
}
