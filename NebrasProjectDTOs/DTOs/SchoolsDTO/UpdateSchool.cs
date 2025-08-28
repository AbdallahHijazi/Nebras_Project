namespace NebrasProjectDTOs.DTOs.SchoolsDTO
{
    public class UpdateSchool
    {
        public Guid SchoolId { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public decimal EstimatedRenovationCost { get; set; }

        public Guid GovernorateId { get; set; }   
        public Guid UserId { get; set; }          

        public List<string> Needs { get; set; } = new List<string>();

        public string HeadTeacherName { get; set; }
        public string HeadTeacherNumber { get; set; }
        public bool IsRequirementsMet { get; set; }

    }
}
