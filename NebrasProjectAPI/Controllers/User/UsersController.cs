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

namespace NebrasProject.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> repository;
        private readonly AppDBContext context;
        private readonly IConfiguration configuration;


        public UserController
            (
            AppDBContext context,
            IConfiguration configuration,
            IRepository<User> repository
            )
        {
            this.repository = repository;
            this.context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            User[] users = repository.GetAll().ToArray();
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
        public ActionResult<User> Get(Guid id)
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
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
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

        private User ValidateUserInformation(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        [HttpPost]
        public ActionResult<User> Post(CreateUser user)
        {
            if (user == null)
            {
                return BadRequest("No data in the users");
            }

            var users = new User
            {
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoleId = Guid.Parse("00000000-0000-0000-0000-000000000001")

            };
            var newUser = repository.Add(users);
            repository.SaveChenges();
            return CreatedAtRoute("GetUser", new { id = newUser.UserId }, newUser);
        }

        [HttpPut]
        public ActionResult<User> UpdateUser(UpdateUser user)
        {
            if (user == null)
            {
                return BadRequest("No data in the users");
            }
            var oldUser = repository.Get(user.UserId);
            if (oldUser == null)
            {
                return NotFound("this user not found");
            }
            else
            {
                oldUser.Username = user.Username;
                oldUser.Email = user.Email;
                oldUser.Password = oldUser.Password;
                oldUser.FullName = user.FullName;
                oldUser.IsActive = oldUser.IsActive;
                oldUser.RoleId = oldUser.RoleId;
                oldUser.CreatedAt = oldUser.CreatedAt;

                repository.Update(oldUser);
                repository.SaveChenges();
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(Guid id)
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
