using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class FileData
    {
        public string? Base64String { get; set; }
        public string? ContentType { get; set; }
    }
}
