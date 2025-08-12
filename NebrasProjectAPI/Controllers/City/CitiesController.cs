using Microsoft.AspNetCore.Mvc;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.City;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectModels.Models.Citys;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectAPI.Controllers.Citys
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


        [HttpGet("city/{cityId}", Name = "GetCityWithSchools")]
        public async Task<ActionResult<CityWithSchoolsDto>> GetCityWithSchools(Guid cityId)
        {
            var city = await cities.GetCityWithSchools(cityId);

            if (city == null)
            {
                return NotFound("No data in the cities");
            }

            return Ok(city);
        }

        [HttpPost]
        public ActionResult<City> Post(CreateCityDto city)
        {
            var governorate = context.Governorates.FirstOrDefault(g => g.GovernorateId == city.GovernorateId);
            if (governorate is null)
            {
                return NotFound("Governorate not found");
            }

            City newCity = new City
            {
                NameAr = city.NameAr,
                NameEn = city.NameEn,
                GovernorateId = city.GovernorateId
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
