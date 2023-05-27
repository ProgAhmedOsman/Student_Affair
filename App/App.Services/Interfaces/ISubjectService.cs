using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{
    public interface ISubjectService
    {

        Task<ActionResponse<DisplaySubjectDTO>> AddSubject(string SubjectName);
        Task<PagedList<DisplaySubjectDTO>> GetAllSubjects(PagingParameters pagingParameters);

    }
}
