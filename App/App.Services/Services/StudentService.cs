using App.Repositories;
using App.Repositories.Helpers;
using App.Service;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{

    public class StudentService : IStudentService
    {
        private IStudentRepository _StudentRepository;

        public StudentService(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }
        public async Task<ActionResponse<SaveStudentDTO>> AddStudentAsync(SaveStudentDTO entity)
        {

            try
            {
                var original = await _StudentRepository.AddStudentAsync(entity);
                return new ActionResponse<SaveStudentDTO>(original);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActionResponse<SaveStudentDTO>($"An error occurred when Adding New Student : {ex.Message}");
            }
        }

        public async Task<ActionResponse<Student>> UpdateStudentAsync(Guid studentKey, SaveStudentDTO entity)
        {

            try
            {
                var existingUser = await _StudentRepository.GetAsync(studentKey);

                if (existingUser == null)
                    return new ActionResponse<Student>(true, "User not found.");

                await _StudentRepository.UpdateStudentAsync(studentKey, entity);

                return new ActionResponse<Student>(existingUser);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActionResponse<Student>($"An error occurred while Updateing Student : {ex.Message}");
            }
        }

        public async Task<PagedList<DisplayStudentDTO>> GetAllStudents(PagingParameters pagingParameters)
        {
            return await _StudentRepository.GetAllStudents(pagingParameters);
        }


        public async Task<ActionResponse<Student>> DeleteStudentAsync(Guid studentKey)
        {
            try
            {
                var Student = await _StudentRepository.GetAsync(studentKey);

                if (Student == null)
                    return new ActionResponse<Student>(true, "Student not found.");
                // Permanently delete
                //await _StudentRepository.DeleteAsync(Student);
                // just make entity inactive
                await _StudentRepository.DeleteStudent(studentKey);

                return new ActionResponse<Student>(Student);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActionResponse<Student>($"An error occurred when deleteing  Student: {ex.Message}");
            }
        }



    }
}
