using NebrasProjectDTOs.DTOs.Shared;

namespace NebrasProjectDTOs.DTOs.UsersDTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public FileData? ProfileImageUrl { get; set; }
        public List<SchoolDto> Schools { get; set; } = new();
    }

    public class SchoolDto
    {
        public Guid SchoolId { get; set; }
        public Guid GovernorateId { get; set; }
        public string GovernorateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
    }
}
