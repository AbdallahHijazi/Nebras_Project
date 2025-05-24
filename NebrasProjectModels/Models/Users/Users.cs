using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectModels.Models.Users
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; } 
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Governorate { get; set; } = "";
        public string City { get; set; } = "";
        public string Address { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;
        public Users()
        {
            Id = Guid.NewGuid();
        }
    }
}
