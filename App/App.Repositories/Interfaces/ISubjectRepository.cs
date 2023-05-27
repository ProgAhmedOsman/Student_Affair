using APP.Domain.Entities;
using App.Repositories;
using APP.Domain.DTOs;
using App.Repositories.Helpers;
 
namespace App.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<DisplaySubjectDTO> AddSubject(string subjectName);
        Task<PagedList<DisplaySubjectDTO>> GetAllSubjects(PagingParameters pagingParameters);
         
    }
}
