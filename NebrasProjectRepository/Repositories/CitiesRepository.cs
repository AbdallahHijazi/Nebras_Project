using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.City;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectModels.Models.Citys;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class CitiesRepository : GenericRepository<City>
    {
        private readonly AppDBContext context;

        public CitiesRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<CityWithSchoolsDto> GetCityWithSchools(Guid cityId)
        {
            var city = await context.Cities
                .Where(c => c.CityId == cityId)
                .Select(c => new CityWithSchoolsDto
                {
                    CityId = c.CityId,
                    NameAr = c.NameAr,
                    NameEn = c.NameEn,
                    Schools = c.Schools.Select(s => new SchoolBasicDto
                    {
                        SchoolId = s.SchoolId,
                        NameAr = s.NameAr,
                        NameEn = s.NameEn
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return city;
        }

        public async Task<GovernorateDto> GetGovernorateWithCities(Guid id)
        {
            return await context.Governorates
                .Where(g => g.GovernorateId == id)
                .Select(g => new GovernorateDto
                {
                    GovernorateId = g.GovernorateId,
                    NameAr = g.NameAr,
                    NameEn = g.NameEn,
                    Cities = g.Cities.Select(c => new CityBasicDto
                    {
                        CityId = c.CityId,
                        NameAr = c.NameAr,
                        NameEn = c.NameEn
                    }).ToList()
                }).FirstOrDefaultAsync();
        }


    }
}
