using App.Common.Enums;
using App.Repositories;
using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class StudentRepository : GenericRepositoryBase<Student>, IStudentRepository
    {

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<SaveStudentDTO> AddStudentAsync(SaveStudentDTO entity)
        {
            Student ObjectToAdd = new Student();
            ObjectToAdd.Key = Guid.NewGuid();
            ObjectToAdd.Name = entity.Name;
            ObjectToAdd.Email = entity.Email;
            ObjectToAdd.BirthDate = entity.BirthDate;
            ObjectToAdd.Address = entity.Address;
            ObjectToAdd.ClassRoom_key = entity.ClassRoom_key;
            ObjectToAdd.CreateDate = DateTime.Now;
            ObjectToAdd.ModifiedDate = DateTime.Now;
            ObjectToAdd.Status = EntityStatus.Active;

            if (entity.Subjects.Any())
            {
                ObjectToAdd.Subjects = new List<StudentSubject>();

                ObjectToAdd.Subjects.AddRange(entity.Subjects.Select(c => new StudentSubject
                {

                    Key = Guid.NewGuid(),
                    Student_Key = ObjectToAdd.Key,
                    Subject_Key = c,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = EntityStatus.Active

                }).ToList()); ;

            }
            var result = entities.Add(ObjectToAdd);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<SaveStudentDTO> UpdateStudentAsync(Guid studentKey, SaveStudentDTO entity)
        {
            var ObjectToEdit = await entities.Include(c => c.Subjects).FirstOrDefaultAsync(c => c.Key == studentKey);
            if (ObjectToEdit != null)
            {
                ObjectToEdit.Name = entity.Name;

                ObjectToEdit.Email = entity.Email;
                ObjectToEdit.BirthDate = entity.BirthDate;
                ObjectToEdit.Address = entity.Address;
                ObjectToEdit.ClassRoom_key = entity.ClassRoom_key;
                ObjectToEdit.ModifiedDate = DateTime.Now;
                if (ObjectToEdit.Subjects.Any())
                {
                    _context.Set<StudentSubject>().RemoveRange(ObjectToEdit.Subjects);
                    ObjectToEdit.Subjects = new List<StudentSubject>();
                }

                if (entity.Subjects.Any())
                {
                    _context.Set<StudentSubject>().AddRange(entity.Subjects.Select(c => new StudentSubject
                    {

                        Key = Guid.NewGuid(),
                        Student_Key = ObjectToEdit.Key,
                        Subject_Key = c,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Status = EntityStatus.Active

                    }).ToList()); 

                }
                //var result = entities.Add(ObjectToEdit);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }


        public async Task<PagedList<DisplayStudentDTO>> GetAllStudents(PagingParameters pagingParameters)
        {

            var data = entities.Where(c =>
            c.Status == EntityStatus.Active
            ).Select(c => new DisplayStudentDTO
            {
                Key = c.Key,
                Name = c.Name,
                BirthDate = c.BirthDate,
                Email = c.Email,
                Address = c.Address,
                ClassRoomName = c.ClassRoom.Name,
            });
            return PagedList<DisplayStudentDTO>.ToPagedList(data.OrderBy(c => c.Name), pagingParameters.PageNumber, pagingParameters.PageSize);
        }




        public async Task<Student> DeleteStudent(Guid studentKey)
        {

            var objectToDelete = entities.FirstOrDefault(s => s.Key == studentKey);



            if (objectToDelete != null)
            {
                objectToDelete.Status = EntityStatus.InActive;
                await _context.SaveChangesAsync();
                return objectToDelete;
            }
            return null;
        }



    }
}
