using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.SchoolsDTO;
using NebrasProjectModels.Models.Schools;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class SchoolRepository : GenericRepository<School>
    {
        private readonly AppDBContext context;

        public SchoolRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IList<School>> GetSchoolsByGovernorateId(Guid governorateId)
        {
            return context.Schools
                .Where(s => s.GovernorateId == governorateId)
                .ToList();
        }

        public async Task<IList<SchoolsNameDTO>> GetSchoolNameAndId(Guid id)
        {
            var schools = await context.Schools.Where(s => s.GovernorateId == id)
                                       .Select(s => new SchoolsNameDTO
                                       {
                                           Name = s.Name,
                                           Id = s.Id.ToString()
                                       }).
                                       ToListAsync();
            return schools;
        }

        public async Task<School> GetSchoolsByDamageLevel(int min, int max)
        {
            var school = await context.Schools
                .Where(s => s.DamageLevel <= min && s.DamageLevel >= max)
                .FirstOrDefaultAsync();

            return school;
        }
    }
}
