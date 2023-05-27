using App.Repositories;
using App.Repositories.Helpers;
using App.Service;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{

    public class ClassRoomService : IClassRoomService
    {
        private IClassRoomRepository _ClassRoomRepository;

        public ClassRoomService(IClassRoomRepository ClassRoomRepository)
        {
            _ClassRoomRepository = ClassRoomRepository;
        }
        public async Task<ActionResponse<DisplayClassRoomDTO>> AddClassRoom(string classRoomName)
        {


            try
            {
            var original = await _ClassRoomRepository.AddClassRoom(classRoomName);
                 return new ActionResponse<DisplayClassRoomDTO>(original);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActionResponse<DisplayClassRoomDTO>($"An error occurred when Adding New Class Room : {ex.Message}");
            }
        }

        public async Task<PagedList<DisplayClassRoomDTO>> GetAllClassRooms(PagingParameters pagingParameters)
        {
            return await _ClassRoomRepository.GetAllClassRooms(pagingParameters);
        }






    }
}
