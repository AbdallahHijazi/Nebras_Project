using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.City;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectDTOs.DTOs.Shared;
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
        public async Task<CityDetailsDto> GetCityDetailsById(Guid cityId)
        {
            var city = await context.Cities
                .Where(c => c.CityId == cityId)
                .Select(c => new CityDetailsDto
                {
                    GovernorateId = c.GovernorateId,
                    GovernorateNameAr = c.Governorate.NameAr,
                    GovernorateNameEn = c.Governorate.NameEn,
                    CityId = c.CityId,
                    NameAr = c.NameAr,
                    NameEn = c.NameEn,
                    SchoolCount = c.Schools.Count()
                })
                .FirstOrDefaultAsync();

            return city;
        }

        public async Task<List<CityDetailsDto>> GetCitiesByGovernorateId(Guid governorateId)
        {
            var citiesEntities = await context.Cities
                                             .Where(c => c.GovernorateId == governorateId)
                                             .Include(c => c.Governorate)
                                             .Include(c => c.Schools)
                                             .ToListAsync();

            var cities = new List<CityDetailsDto>();

            foreach (var c in citiesEntities)
            {
                FileData? cityImage = null;

                if (!string.IsNullOrEmpty(c.CityImage))
                {
                    var safeFileName = Path.GetFileName(c.CityImage);
                    var filePath = Path.Combine("wwwroot/uploads", safeFileName);

                    if (File.Exists(filePath))
                    {
                        var fileBytes = File.ReadAllBytes(filePath);
                        var extension = Path.GetExtension(filePath).ToLower();

                        string contentType = extension switch
                        {
                            ".jpg" or ".jpeg" => "image/jpeg",
                            ".png" => "image/png",
                            ".gif" => "image/gif",
                            _ => "application/octet-stream"
                        };

                        cityImage = new FileData
                        {
                            Base64String = Convert.ToBase64String(fileBytes),
                            ContentType = contentType
                        };
                    }
                }

                cities.Add(new CityDetailsDto
                {
                    CityId = c.CityId,
                    NameEn = c.NameEn,
                    NameAr = c.NameAr,
                    GovernorateId = c.GovernorateId,
                    GovernorateNameAr = c.Governorate.NameAr,
                    GovernorateNameEn = c.Governorate.NameEn,
                    SchoolCount = c.Schools.Count,
                    CityImageUrl = cityImage
                });
            }

            return cities;
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
                   
                }).FirstOrDefaultAsync();
        }
    }
}
