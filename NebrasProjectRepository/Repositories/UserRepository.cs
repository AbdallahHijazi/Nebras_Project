using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectDomain.Models;
using NebrasProjectModels.Models.Users;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly AppDBContext context;

        public UserRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task ChangePassword(string newPassword)
        {

        }

    }
}
