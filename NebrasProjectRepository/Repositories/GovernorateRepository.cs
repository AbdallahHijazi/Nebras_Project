using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class GovernorateRepository : GenericRepository<Governorate>
    {
        private readonly AppDBContext context;

        public GovernorateRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Governorate> GetGovernorateWithSchools(Guid id)
        {
            var governorate = await context.Governorates
                .Include(g=>g.Schools)
                .FirstOrDefaultAsync(g => g.Id == id);

            return governorate;
        }

        public async Task<IList<GovernoratesInfo>> GetGovernoratesInfo()
        {
            var governorates = await context.Governorates
                .Select(g => new GovernoratesInfo
                {
                    Id = g.Id,
                    Name = g.Name,
                    SchoolCount = g.Schools.Count
                }).ToListAsync();
            return governorates;
        }

    }
}
