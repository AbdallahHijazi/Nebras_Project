using NebrasProjectDomain.Models;
using NebrasProjectModels.Models.RenovationTypes;
using NebrasProjectRepository.SheardRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectRepository.Repositories
{
    public class RenovationTypeRepsitory : GenericRepository<RenovationType>
    {
        private readonly AppDBContext context;

        public RenovationTypeRepsitory(AppDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
