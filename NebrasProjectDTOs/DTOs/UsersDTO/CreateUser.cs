using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectDTOs.DTOs.UsersDTO
{
    public class CreateUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? ProfileImageBase64 { get; set; }

    }
}
