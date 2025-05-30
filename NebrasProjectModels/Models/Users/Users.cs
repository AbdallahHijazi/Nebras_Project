using System.ComponentModel.DataAnnotations;
using NebrasProjectModels.Models.Roles;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectModels.Models.Users
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public ICollection<School> AddedSchools { get; set; }
        public ICollection<School> ApprovedSchools { get; set; }
        public User()
        {
            UserId = Guid.NewGuid();
        }
    }
}
