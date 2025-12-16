using Microsoft.AspNetCore.Mvc;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.City;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectModels.Models.Citys;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasPhotoService.Controllers.Citys
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IRepository<City> repository;
        private readonly AppDBContext context;
        private readonly CitiesRepository cities;

        public CitiesController(IRepository<City> repository, AppDBContext context, CitiesRepository cities)
        {
            this.repository = repository;
            this.context = context;
            this.cities = cities;
        }

        [HttpGet]
        public ActionResult<List<City>> GetAll()
        {
            var cities = repository.GetAll();
            if (cities == null)
            {
                return NotFound("No data in the cities");
            }
            else
            {
                return Ok(cities);
            }
        }

        [HttpGet("{id}", Name = "GetCity")]
        public async Task<ActionResult<GovernorateDto>> Get(Guid id)
        {
            var city = await cities.GetGovernorateWithCities(id);

            if (city == null)
            {
                return NotFound("No data in the cities");
            }

            return Ok(city);
        }


        [HttpGet("city/{cityId}", Name = "GetCityDetails")]
        public async Task<ActionResult<CityDetailsDto>> GetCityDetails(Guid cityId)
        {
            var city = await cities.GetCityDetailsById(cityId);

            if (city == null)
            {
                return NotFound("No data in the cities");
            }

            return Ok(city);
        }

        [HttpGet("governorates/{governorateId}/cities", Name = "GetCitiesByGovernorate")]
        public async Task<ActionResult<List<CityDetailsDto>>> GetCitiesByGovernorate(Guid governorateId)

        {
            var Cities = await cities.GetCitiesByGovernorateId(governorateId);

            if (Cities == null)
            {
                return NotFound("No data in the cities");
            }

            return Ok(Cities);
        }

        [HttpPost]
        public ActionResult<City> Post(CreateCityDto city)
        {

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(city.CityBase64!.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                city.CityBase64.CopyToAsync(stream);
            }

            var photoUrl = $"/uploads/{uniqueFileName}";
            var governorate = context.Governorates.FirstOrDefault(g => g.GovernorateId == city.GovernorateId);
            if (governorate is null)
            {
                return NotFound("Governorate not found");
            }

            City newCity = new City
            {
                NameAr = city.NameAr,
                NameEn = city.NameEn,
                GovernorateId = city.GovernorateId,
                CityImage = photoUrl,
            };
            repository.Add(newCity);
            context.SaveChanges();
            return CreatedAtRoute("GetCity", new { id = newCity.CityId }, new CreateCityDto
            {
                NameAr = newCity.NameAr,
                NameEn = newCity.NameEn,
                GovernorateId = newCity.GovernorateId
            });
        }

        [HttpPut]
        public ActionResult Put(UpdateCityDto city)
        {
            var existingCity = repository.Get(city.CityId);
            var governorate = context.Governorates.FirstOrDefault(g => g.GovernorateId == city.GovernorateId);
            if (existingCity == null)
            {
                return NotFound("City not found");
            }
            else if (governorate is null)
            {
                return NotFound("governorate not found");
            }
            existingCity.NameAr = city.NameAr;
            existingCity.NameEn = city.NameEn;
            existingCity.GovernorateId = city.GovernorateId;
            repository.Update(existingCity);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var city = repository.Get(id);
            if (city == null)
            {
                return NotFound("City not found");
            }
            repository.Delete(city);
            context.SaveChanges();
            return NoContent();
        }
    }
}
