using APP.Domain.Entities;
using App.Repositories;
using APP.Domain.DTOs;
using App.Repositories.Helpers;

namespace App.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<SaveStudentDTO> AddStudentAsync(SaveStudentDTO entity);
        Task<SaveStudentDTO> UpdateStudentAsync(Guid studentKey, SaveStudentDTO entity);
        Task<Student> DeleteStudent(Guid studentKey);
        Task<PagedList<DisplayStudentDTO>> GetAllStudents(PagingParameters pagingParameters);

    }
}
