using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Users;

namespace NebrasProjectModels.Models.Roles
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
        public Role()
        {
            RoleId = Guid.NewGuid();
        }
    }
}
