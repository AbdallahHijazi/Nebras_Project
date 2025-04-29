using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using NebrasProjectRepository.SheardRepository;
using NebrasProjectModels.Models.Users;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.UsersDTO;
using NebrasProjectModels.Profiles;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace NebrasProject.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<Users> repository;
        private readonly AppDBContext context;
        private readonly IConfiguration configuration;


        public UserController
            (
            AppDBContext context,
            IConfiguration configuration,
            IRepository<Users> repository
            )
        {
            this.repository = repository;
            this.context = context;
            this.configuration = configuration;
        }
        
        [HttpGet]
        public ActionResult<List<Users>> GetAll()
        {
            Users[] users = repository.GetAll().ToArray();
            if (users == null)
            {
                return NotFound("No data in the users");
            }
            else
            {
                return Ok(users);
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<Users> Get(string id)
        {
            var user = repository.Get(id);
            if (user == null)
            {
                return NotFound("No data in the users");
            }
            else
            {
                return Ok(user);
            }
        }
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationUser authenticationUser)
        {
            var user = ValidateUserInformation(authenticationUser.Email, authenticationUser.password);
            if(user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secretKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(configuration["Authentication:SecretKey"]));
            var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audiance"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(9),
                signingCred
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return Ok(new { Token = token });
                
        }

        private Users ValidateUserInformation(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        [HttpPost]
        public ActionResult<Users> Post(CreateUser user)
        {
            if (user == null)
            {
                return BadRequest("No data in the users");
            }

            var users = new Users
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Governorate = user.Governorate,
                City = user.City,
                Address = user.Address
            };
            var newUser = repository.Add(users);
            repository.SaveChenges();
            return CreatedAtRoute("GetUser", new { id = newUser.Id }, newUser);
        }

        [HttpPut]
        public ActionResult<Users> UpdateUser(UpdateUser user)
        {
            if (user == null)
            {
                return BadRequest("No data in the users");
            }
            var oldUser = repository.Get(user.Id);
            if (oldUser == null)
            {
                return NotFound("this user not found");
            }
            else
            {
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.PhoneNumber = user.PhoneNumber;
                oldUser.Governorate = user.Governorate;
                oldUser.City = user.City;
                oldUser.Address = user.Address;

                repository.Update(oldUser);
                repository.SaveChenges();
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<Users> Delete(string id)
        {
            var user = repository.Get(id);
            if (user == null)
            {
                return NotFound("this user not found");
            }
            else
            {
                repository.Delete(user);
                repository.SaveChenges();
                return NoContent();
            }
        }
    }
}
