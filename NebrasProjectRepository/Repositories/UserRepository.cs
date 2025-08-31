using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.Shared;
using NebrasProjectDTOs.DTOs.UsersDTO;
using NebrasProjectModels.Models.Users;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly AppDBContext context;

        public UserRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<UserDTO?> GetByIdAsync(Guid userId)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return null;

            FileData? profileImage = null;

            if (!string.IsNullOrEmpty(user.ProfileImageUrl))
            {
                profileImage = GetUserImage(user.ProfileImageUrl);
            }

            var schools = await context.Schools
                .Where(s => s.AddedByUserId == user.UserId)
                .Select(s => new SchoolDto
                {
                    SchoolId = s.SchoolId,
                    CityName = s.City,
                    SchoolName = s.NameAr,
                    GovernorateName = s.Governorate.NameAr,
                    GovernorateId = s.Governorate.GovernorateId,
                })
                .ToListAsync();

            var role = user.RoleId == Guid.Parse("00000000-0000-0000-0000-000000000011") ? "Administrator" : "User";
            var roleId = user.RoleId == Guid.Parse("00000000-0000-0000-0000-000000000011") ? "11" : "01";
            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Role = role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive,
                Schools = schools,
                RoleId = roleId,
                ProfileImageUrl = profileImage
            };
        }

        public FileData? GetUserImage(string relativePath)
        {
            return GetFileAsBase64(relativePath, "users");
        }

    }
}
