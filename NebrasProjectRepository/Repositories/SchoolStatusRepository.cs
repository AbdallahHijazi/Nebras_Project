using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectDomain.Models;
using NebrasProjectModels.Models.SchoolStatues;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class SchoolStatusRepository : GenericRepository<SchoolStatus>
    {
        private readonly AppDBContext context;

        public SchoolStatusRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
