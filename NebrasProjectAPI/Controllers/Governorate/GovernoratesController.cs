using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            var governorates = repository.GetAll().ToArray();
            if (governorates == null)
            {
                return NotFound("No data in the governorates");
            }
            else
            {
                return Ok(governorates);
            }
        }

        [HttpGet("{id}/schools-summary", Name = "GetGovernorateWithSchools")]
        public async Task<ActionResult<GovernorateDetailsDTO>> GetGovernorateWithSchools(Guid id)
        {
            var governorate = await governorateRepository.GetGovernorateWithSchools(id);
            if (governorate == null)
            {
                return NotFound("No data in the governorates");
            }
            else
            {
                return Ok(governorate);
            }
        }

        [HttpGet("{id}", Name = "GetGovernorate")]
        public async Task<ActionResult<GovernorateDetailsDTO>> Get(Guid id)
        {
            var governorate = await governorateRepository.GetGovernorateWithSchools(id);
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


            var governorates = new Governorate
            {
                NameAr = governorate.NameAr,
                NameEn = governorate.NameEn
            };
            if (governorates is null)
            {
                return BadRequest("No data in the governorate");
            }
            else if (governorate.NameEn.IsNullOrEmpty() || governorate.NameAr.IsNullOrEmpty())
            {
                return BadRequest("Name most be not enpty.");
            }
            else
            {
                repository.Add(governorates);
                repository.SaveChenges();
                return CreatedAtRoute("GetGovernorate", new { id = governorates.GovernorateId }, governorates);
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
                governorates.NameAr = governorate.NameAr;
                governorates.NameEn = governorate.NameEn;
                if (governorate.NameEn.IsNullOrEmpty() || governorate.NameAr.IsNullOrEmpty())
                {
                    return BadRequest("Name most be not enpty.");
                }

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

    }
}
