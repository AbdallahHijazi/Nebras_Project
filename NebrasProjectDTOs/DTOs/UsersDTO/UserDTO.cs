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
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public FileData? ProfileImageUrl { get; set; }
    }
}
