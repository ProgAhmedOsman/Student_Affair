using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{
    public interface IStudentService
    {

        Task<ActionResponse<SaveStudentDTO>> AddStudentAsync(SaveStudentDTO entity);
        Task<ActionResponse<Student>> UpdateStudentAsync(Guid studentKey, SaveStudentDTO entity);
        Task<ActionResponse<Student>> DeleteStudentAsync(Guid studentKey);
        Task<PagedList<DisplayStudentDTO>> GetAllStudents(PagingParameters pagingParameters);

    }
}
