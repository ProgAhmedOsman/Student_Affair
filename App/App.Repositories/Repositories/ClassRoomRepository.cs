using App.Common.Enums;
using App.Repositories;
using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class ClassRoomRepository : GenericRepositoryBase<ClassRoom>, IClassRoomRepository
    {

        public ClassRoomRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<DisplayClassRoomDTO> AddClassRoom(string className)
        {
            ClassRoom ObjectToAdd = new ClassRoom();


            ObjectToAdd.Name = className;
            ObjectToAdd.CreateDate = DateTime.Now;
            ObjectToAdd.ModifiedDate = DateTime.Now;
            ObjectToAdd.Status = EntityStatus.Active;
            var result = entities.Add(ObjectToAdd);
            await _context.SaveChangesAsync();
            if (result == null) return null;
            else
            {
                return new DisplayClassRoomDTO { Key = ObjectToAdd.Key, Name = ObjectToAdd.Name };
            }

        }

        public async Task<PagedList<DisplayClassRoomDTO>> GetAllClassRooms(PagingParameters pagingParameters)
        {

            var data = entities.Where(c =>
            c.Status == EntityStatus.Active
            ).Select(c => new DisplayClassRoomDTO
            {
                Key = c.Key,
                Name = c.Name,
            });
            return PagedList<DisplayClassRoomDTO>.ToPagedList(data.OrderByDescending(c => c.Name), pagingParameters.PageNumber, pagingParameters.PageSize);
        }

    }



}
