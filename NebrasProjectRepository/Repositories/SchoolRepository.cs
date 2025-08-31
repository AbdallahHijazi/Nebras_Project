using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.SchoolsDTO;
using NebrasProjectDTOs.DTOs.Shared;
using NebrasProjectModels.Models.Schools;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class SchoolRepository : GenericRepository<School>
    {
        private readonly AppDBContext context;

        public SchoolRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IList<SchoolDetailsDto>> GetAllSchools()
        {
            var schools = await context.Schools
                .Where(s => s.IsApproved == true && s.IsRequirementsMet == false)
                .Include(s => s.Governorate)
                .ToListAsync();

            var SchoolsList = new List<SchoolDetailsDto>();

            foreach (var school in schools)
            {
                FileData? schoolImage = null;
                if (!string.IsNullOrEmpty(school.SchoolImageUrl))
                {
                    schoolImage = GetFileAsBase64(school.SchoolImageUrl, "schools");
                }

                var schoole = new SchoolDetailsDto
                {
                    SchoolId = school.SchoolId,
                    NameAr = school.NameAr,
                    NameEn = school.NameEn,
                    City = school.City,
                    Description = school.Description,
                    EstimatedRenovationCost = school.EstimatedRenovationCost,
                    GovernorteId = school.GovernorateId,
                    GovernortesName = school.Governorate?.NameAr!,
                    HeadTeacherName = school.HeadTeacherName,
                    HeadTeacherNumber = school.HeadTeacherNumber,
                    AddedByUserId = school.AddedByUserId,
                    IsApproved = school.IsApproved,
                    IsRequirementsMet = school.IsRequirementsMet,
                    Needs = school.Needs,
                    ProfileImageUrl = schoolImage
                };

                SchoolsList.Add(schoole);
            }

            return SchoolsList;
        }


        public async Task<SchoolDetailsDto?> GetSchoolById(Guid id)
        {
            var school = await context.Schools
                .Include(s => s.Governorate)
                .FirstOrDefaultAsync(s => s.SchoolId == id);

            if (school == null)
                return null;

            FileData? schoolImage = null;
            if (!string.IsNullOrEmpty(school.SchoolImageUrl))
            {
                schoolImage = GetFileAsBase64(school.SchoolImageUrl, "schools");
            }

            var schoolDto = new SchoolDetailsDto
            {
                SchoolId = school.SchoolId,
                NameAr = school.NameAr,
                NameEn = school.NameEn,
                City = school.City,
                Description = school.Description,
                EstimatedRenovationCost = school.EstimatedRenovationCost,
                GovernorteId = school.GovernorateId,
                GovernortesName = school.Governorate?.NameAr!,
                HeadTeacherName = school.HeadTeacherName,
                HeadTeacherNumber = school.HeadTeacherNumber,
                AddedByUserId = school.AddedByUserId,
                IsApproved = school.IsApproved,
                IsRequirementsMet = school.IsRequirementsMet,
                Needs = school.Needs,
                ProfileImageUrl = schoolImage
            };

            return schoolDto;
        }

    }
}
