using APIAssignment2.Models.Enums;

namespace APIAssignment2.Models
{
    public interface IEntityBase
    {
        int Id { get; set; }
        RecordStatus RecordStatus { get; set; }
    }
}