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
                 .Include(g => g.Cities)
                 .FirstOrDefaultAsync(g => g.GovernorateId == id);
            if (governorate == null)
            {
                return null; // Or throw an exception if preferred
            }

            return governorate;
        }

        //public async Task<IList<GovernoratesInfo>> GetGovernoratesInfo()
        //{
        //    //var governorates = await context.Governorates
        //    //    .Select(g => new GovernoratesInfo
        //    //    {
        //    //        Id = g.Id,
        //    //        Name = g.Name,
        //    //        SchoolCount = g.Schools.Count
        //    //    }).ToListAsync();
        //    //return governorates;
        //    return null; // Placeholder for actual implementation

        //}

    }
}
