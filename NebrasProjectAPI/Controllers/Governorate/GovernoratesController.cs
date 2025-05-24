using Microsoft.AspNetCore.Mvc;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectAPI.Controllers.Governorates
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernoratesController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IRepository<Governorate> repository;
        private readonly GovernorateRepository governorateRepository;

        public GovernoratesController(AppDBContext context,
                                      IRepository<Governorate> repository,
                                      GovernorateRepository governorateRepository
)
        {
            this.context = context;
            this.repository = repository;
            this.governorateRepository = governorateRepository;
        }


        [HttpGet]
        public ActionResult<List<Governorate>> GetAll()
        {
            Governorate[] governorates = repository.GetAll().ToArray();
            if (governorates == null)
            {
                return NotFound("No data in the governorates");
            }
            else
            {
                return Ok(governorates);
            }
        }
        [HttpGet("{id}/schools-summary", Name = "GetGovernorate")]
        public ActionResult<Governorate> Get(Guid id)
        {
            var governorate = governorateRepository.GetGovernorateWithSchools(id);
            if (governorate == null)
            {
                return NotFound("No data in the governorates");
            }
            else
            {
                return Ok(governorate);
            }
        }

        [HttpPost]
        public ActionResult<Governorate> Post(CreateGovernorate governorate)
        {
            var schools = context.Schools
                .Where(s => governorate.Schools.Contains(s.Id))
                .ToList();

            var governorates = new Governorate
            {
                Name = governorate.Name,
                Description = governorate.Description,
                DamageLevel = governorate.DamageLevel,
                Address = governorate.Address,
                Schools = schools

            };
            if (governorates is null)
            {
                return BadRequest("No data in the governorate");
            }
            else
            {
                repository.Add(governorates);
                repository.SaveChenges();
                return CreatedAtRoute("GetGovernorate", new { id = governorates.Id }, governorates);
            }
        }
        [HttpPut]
        public ActionResult<Governorate> Put(UpdateGovernorate governorate)
        {
            var governorates = repository.Get(governorate.Id);
            if (governorates is null)
            {
                return NotFound("No data in the governorate");
            }
            else
            {
                var schools = context.Schools
                    .Where(s => governorate.Schools.Contains(s.Id))
                    .ToList();
                governorates.Name = governorate.Name;
                governorates.Description = governorate.Description;
                governorates.DamageLevel = governorate.DamageLevel;
                governorates.Address = governorate.Address;
                governorates.Schools = schools;
                repository.Update(governorates);
                repository.SaveChenges();
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<Governorate> Delete(Guid id)
        {
            var governorate = repository.Get(id);
            if (governorate is null)
            {
                return NotFound("No data in the governorate");
            }
            else
            {
                repository.Delete(governorate);
                repository.SaveChenges();
                return NoContent();
            }
        }
        [HttpGet("info")]
        public ActionResult<GovernoratesInfo> GetGovernoratesInfo()
        {
            var governorates=governorateRepository.GetGovernoratesInfo();
            if (governorates is null)
            {
                return NotFound();
            }
            return Ok(governorates);
        }

    }
}
