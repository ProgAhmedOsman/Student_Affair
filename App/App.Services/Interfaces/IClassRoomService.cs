using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{
    public interface IClassRoomService
    {

        Task<ActionResponse<DisplayClassRoomDTO>> AddClassRoom(string classRoomName);
        Task<PagedList<DisplayClassRoomDTO>> GetAllClassRooms(PagingParameters pagingParameters);

    }
}
