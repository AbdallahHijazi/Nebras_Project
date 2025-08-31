using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectDTOs.DTOs.Shared;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class GovernorateRepository : GenericRepository<Governorate>
    {
        private readonly AppDBContext context;

        public GovernorateRepository(AppDBContext context
            ) : base(context)
        {
            this.context = context;
        }

        public async Task<GovernorateDetailsDTO> GetGovernorateDetails(Guid id)
        {
            FileData? profileImage = null;

            var gove = await context.Governorates
                 .FirstOrDefaultAsync(g => g.GovernorateId == id);
            string? base64String = null;
            string? contentType = null;

            if (!string.IsNullOrEmpty(gove!.GovernorateImage!))
            {
                profileImage = GetGovernorateImage(gove.GovernorateImage);

            }
            var governorate = await context.Governorates
                .Where(g => g.GovernorateId == id)
                .Select(g => new GovernorateDetailsDTO
                {
                    GovernorateId = g.GovernorateId,
                    NameAr = g.NameAr,
                    NameEn = g.NameEn,
                    CityCount = g.CityCount,
                    Description = g.Description,
                    SchoolCount = g.SchoolCount,
                    GovernorateImageUrl = profileImage!
                }).FirstOrDefaultAsync();
            if (governorate == null)
            {
                return null; // Or throw an exception if preferred
            }

            return governorate;
        }

        public FileData? GetGovernorateImage(string relativePath)
        {
            return GetFileAsBase64(relativePath, "governorates");
        }

        public async Task<GovernorateSummaryDTO> GetGovernorateSummary(Guid id)
        {
            var governorateEntity = await context.Governorates
           .Include(g => g.Cities)
               .ThenInclude(c => c.Schools)
           .FirstOrDefaultAsync(g => g.GovernorateId == id);

            if (governorateEntity == null) return null;

            int totalCities = governorateEntity.Cities.Count();

            int totalSchools = governorateEntity.Cities.SelectMany(c => c.Schools).Count();


            var governorateDTO = new GovernorateSummaryDTO
            {
                GovernorateId = governorateEntity.GovernorateId,
                NameAr = governorateEntity.NameAr,
                NameEn = governorateEntity.NameEn,
                CoveredCities = totalCities,
                TotalCities = governorateEntity.CityCount,
                CoveredSchools = totalSchools,
                TotalSchools = governorateEntity.SchoolCount,
                DamagePercentage = 20,

                SchoolsCoverage = new List<ChartDataDTO>
                {
                    new ChartDataDTO { Label = "Covered Schools", Value = totalSchools },
                    new ChartDataDTO { Label = "Remaining Schools", Value = governorateEntity.SchoolCount }
                },

                CitiesCoverage = new List<ChartDataDTO>
                {
                    new ChartDataDTO { Label = "Covered Cities", Value = totalCities },
                    new ChartDataDTO { Label = "Remaining Cities", Value = governorateEntity.CityCount }
                },

                DamageChart = new List<ChartDataDTO>
                {
                    new ChartDataDTO { Label = "Damaged Schools", Value = 80 },
                    new ChartDataDTO { Label = "Undamaged Schools", Value = 20 }
                }
            };
            return governorateDTO;

        }
    }
}
