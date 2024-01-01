using App.Repositories;
using App.Repositories.Helpers;
using App.Service;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using APP.SharedKernel.DistributedLock.DistributedLock.Sql.Contracts;

namespace App.Service
{

    public class StudentService : IStudentService
    {
        private IStudentRepository _StudentRepository;
        private readonly IDistributedLockClient _distributedLockClient;

        public StudentService(IStudentRepository StudentRepository, IDistributedLockClient distributedLockClient)
        {
            _StudentRepository = StudentRepository;
            _distributedLockClient = distributedLockClient;
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

        public async Task<ActionResponse<SaveStudentDTO>> UpdateStudentAsync(Guid studentKey, SaveStudentDTO entity)
        {

            try
            {


                var existingUser = await _StudentRepository.GetAsync(studentKey);
                if (existingUser == null)
                    return new ActionResponse<SaveStudentDTO>(true, "User not found.");
                await _StudentRepository.UpdateStudentAsync(studentKey, entity);

                return new ActionResponse<SaveStudentDTO>(entity);

            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActionResponse<SaveStudentDTO>($"An error occurred while Updateing Student : {ex.Message}");
            }
        }
        public async Task<ActionResponse<SaveStudentDTO>> UpdateStudentAsync_Lock(Guid studentKey, SaveStudentDTO entity)
        {

            try
            {

                using (_distributedLockClient.AcquireLock(studentKey.ToString()))
                {
                    var existingUser = await _StudentRepository.GetAsync(studentKey);

                    if (existingUser == null)
                        return new ActionResponse<SaveStudentDTO>(true, "User not found.");



                    await _StudentRepository.UpdateStudentAsync(studentKey, entity);
                    Thread.Sleep(10000);
                    return new ActionResponse<SaveStudentDTO>(entity);
                }
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActionResponse<SaveStudentDTO>($"An error occurred while Updateing Student : {ex.Message}");
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
