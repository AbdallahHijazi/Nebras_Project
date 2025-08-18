using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectDTOs.DTOs.Shared;
using NebrasProjectModels.Models.Roles;

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
        public Guid CityId { get; set; }
        public string GovernorateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
    }
}
