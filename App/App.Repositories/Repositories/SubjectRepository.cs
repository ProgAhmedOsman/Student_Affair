using App.Common.Enums;
using App.Repositories;
using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class SubjectRepository : GenericRepositoryBase<Subject>, ISubjectRepository
    {

        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<DisplaySubjectDTO> AddSubject(string subjectName)
        {
            Subject ObjectToAdd = new Subject();


            ObjectToAdd.Name = subjectName;
            ObjectToAdd.CreateDate = DateTime.Now;
            ObjectToAdd.ModifiedDate = DateTime.Now;
            ObjectToAdd.Status = EntityStatus.Active;
            var result = entities.Add(ObjectToAdd);
            await _context.SaveChangesAsync();
            if (result == null) return null;
            else
            {
                return new DisplaySubjectDTO { Key = ObjectToAdd.Key, Name = ObjectToAdd.Name };
            }

        }

        public async Task<PagedList<DisplaySubjectDTO>> GetAllSubjects(PagingParameters pagingParameters)
        {

            var data = entities.Where(c =>
            c.Status == EntityStatus.Active
            ).Select(c => new DisplaySubjectDTO
            {
                Key = c.Key,
                Name = c.Name,
            });
            return PagedList<DisplaySubjectDTO>.ToPagedList(data.OrderByDescending(c => c.Name), pagingParameters.PageNumber, pagingParameters.PageSize);
        }

    }



}
