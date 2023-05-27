using APP.Domain.Entities;
using App.Repositories;
using APP.Domain.DTOs;
using App.Repositories.Helpers;
 
namespace App.Repositories
{
    public interface IClassRoomRepository : IGenericRepository<ClassRoom>
    {
        Task<DisplayClassRoomDTO> AddClassRoom(string className);
        Task<PagedList<DisplayClassRoomDTO>> GetAllClassRooms(PagingParameters pagingParameters);
         
    }
}
