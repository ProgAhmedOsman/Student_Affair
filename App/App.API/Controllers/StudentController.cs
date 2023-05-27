using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using Api = App.API.DTOs;
using App.Common.Enums;
using App.Helper;
using App.Service;
using Domain = APP.Domain.DTOs;
using APP.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using App.API.DTOs;
using APP.Domain.DTOs;
using App.API.Helper;
using NuGet.Common;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentService _StudentService;

        private readonly IMapper _mapper;
        public StudentController(ILogger<StudentController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IStudentService StudentService, IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _StudentService = StudentService;
            _mapper = mapper;
        }

        /// <summary>
        ///  Get All Students
        /// </summary>
        /// <param name="PagingParametersDto"></param>
        /// <returns>array of Students data in  requested page and return paging data in response header</returns>     
        ///<response code="200">return all Students successfully</response>
        ///<response code="401">user not authorized</response>
        [HttpGet]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.DisplayStudentDTO>> GetAllAsync(Api.PagingParameters PagingParametersDto)
        {
            var data = await _StudentService.GetAllStudents(_mapper.Map<Domain.PagingParameters>(PagingParametersDto));
            var metadata = new
            {
                data.TotalCount,
                data.PageSize,
                data.CurrentPage,
                data.TotalPages,
                data.HasNext,
                data.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            _logger.LogInformation($"Returned {data.TotalCount} Student note from database.");
            return Ok(data);
        }





        /// <summary>
        ///  Add new  Student 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.SaveStudentDTO>> PostAsync(Api.SaveStudentDTO resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.GetErrorMessages().Aggregate((p, n) => p + ", " + n) });
            var result = await _StudentService.AddStudentAsync(_mapper.Map<Domain.SaveStudentDTO>(resource));
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            return Created("Student Added Successfully ", result.Entity);

        }
        /// <summary>
        ///  Add new  Student 
        /// </summary>
        /// <param name="studentkey"> student key</param>
        /// <param name="resource"> Save Student DTO</param>
        /// <returns></returns>
        [HttpPut("{Studentkey}")]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.SaveStudentDTO>> PutAsync(Guid studentkey, Api.SaveStudentDTO resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.GetErrorMessages().Aggregate((p, n) => p + ", " + n) });

            var result = await _StudentService.UpdateStudentAsync(studentkey, _mapper.Map<Domain.SaveStudentDTO>(resource));
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            return Created("Student Updated Successfully ", result.Entity);
        }



        /// <summary>
        ///  Delete Student by key
        /// </summary>
        /// <param name="Studentkey">Student key </param>
        /// <returns>OK</returns>
        ///<response code="200"> Student deleted successfully</response>
        ///<response code="404">no Student found with this key</response>
        /// <response code="400">no Student deleted</response>
        /// <response code="401">user not authorized to delete Student</response>
        [HttpDelete("{Studentkey}")]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<IActionResult> DeleteAsync(Guid Studentkey)
        {
            var result = await _StudentService.DeleteStudentAsync(Studentkey);
            if (!result.Success && result.NotFoundError)
            {
                _logger.LogError(result.Message);
                return NotFound(new { message = result.Message });
            }
            if (!result.Success)
            {
                _logger.LogError(result.Message);
                return BadRequest(new { message = result.Message });
            }
            return Ok(new { message = "Student deleted successfully" });

        }


    }
}