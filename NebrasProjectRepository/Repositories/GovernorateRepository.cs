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

        public async Task<GovernorateDetailsDTO> GetGovernorateWithSchools(Guid id)
        {
            var governorate = await context.Governorates
                .Where(g => g.GovernorateId == id)
                .Select(g => new GovernorateDetailsDTO
                {
                    GovernorateId = g.GovernorateId,
                    NameAr = g.NameAr,
                    NameEn = g.NameEn,
                    CityCount = g.Cities.Count,
                    Cities = g.Cities.Select(c => new CityDetails
                    {
                        CityId = c.CityId,
                        NameAr = c.NameAr,
                        NameEn = c.NameEn,

                        SchoolCount = c.Schools.Count,
                        Schools = c.Schools.Select(s => new SchoolDetails
                        {
                            SchoolId = s.SchoolId,
                            NameAr = s.NameAr,
                            NameEn = s.NameEn,
                            AddressDetails = s.AddressDetails,
                            Latitude = s.Latitude,
                            Longitude = s.Longitude,
                            StudentCapacity = s.StudentCapacity,
                            NumberOfClassrooms = s.NumberOfClassrooms,
                            YearEstablished = s.YearEstablished
                        }).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();
            if (governorate == null)
            {
                return null; // Or throw an exception if preferred
            }

            return governorate;
        }
    }
}
