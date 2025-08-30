namespace NebrasProjectDTOs.DTOs.SchoolsDTO
{
    public class SchoolDetailsDto
    {
        public Guid SchoolId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public decimal EstimatedRenovationCost { get; set; }
        public Guid GovernortesId { get; set; }
        public string GovernortesName { get; set; }
        public string HeadTeacherName { get; set; }
        public string HeadTeacherNumber { get; set; }
        public Guid AddedByUserId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRequirementsMet { get; set; }
        public List<string> Needs { get; set; } = new List<string>();


    }

}
