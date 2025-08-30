using System.ComponentModel.DataAnnotations;
using NebrasProjectModels.Models.Governorates;


namespace NebrasProjectModels.Models.Schools
{
    public class School
    {

        [Key]
        public Guid SchoolId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public decimal EstimatedRenovationCost { get; set; }
        public Guid AddedByUserId { get; set; }
        public Guid? ApprovedByUserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public Guid GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        public string HeadTeacherName { get; set; }
        public string HeadTeacherNumber { get; set; }
        public bool IsRequirementsMet { get; set; }
        public List<string> Needs { get; set; } = new List<string>();

        public School()
        {
            SchoolId = Guid.NewGuid();
        }
    }
}
