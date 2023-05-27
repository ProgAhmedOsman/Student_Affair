using App.Repositories;
using App.Repositories.Helpers;
using App.Service;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{

    public class SubjectService : ISubjectService
    {
        private ISubjectRepository _SubjectRepository;

        public SubjectService(ISubjectRepository SubjectRepository)
        {
            _SubjectRepository = SubjectRepository;
        }
        public async Task<ActionResponse<DisplaySubjectDTO>> AddSubject(string subjectName)
        {

            try
            {
                var original = await _SubjectRepository.AddSubject(subjectName);
                return new ActionResponse<DisplaySubjectDTO>(original);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ActionResponse<DisplaySubjectDTO>($"An error occurred when Adding New Subject : {ex.Message}");
            }
        }

        public async Task<PagedList<DisplaySubjectDTO>> GetAllSubjects(PagingParameters pagingParameters)
        {
            return await _SubjectRepository.GetAllSubjects(pagingParameters);
        }






    }
}
