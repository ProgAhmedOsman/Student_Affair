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
using Microsoft.Extensions.FileProviders;
using App.Common.Extensions;
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
        private readonly IVirtualFileProvider _fileProvider;

        private readonly IMapper _mapper;
        public StudentController(ILogger<StudentController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IStudentService StudentService, IMapper mapper, IVirtualFileProvider fileProvider)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _StudentService = StudentService;
            _mapper = mapper;
            _fileProvider = fileProvider;
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
        public async Task<ActionResult<Api.DisplayStudentDTO>> GetAllAsync([FromQuery] Api.PagingParameters PagingParametersDto)
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
        [HttpPut("{studentkey}")]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.SaveStudentDTO>> PutAsync(Guid studentkey, Api.SaveStudentDTO resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.GetErrorMessages().Aggregate((p, n) => p + ", " + n) });

            var result = await _StudentService.UpdateStudentAsync(studentkey, _mapper.Map<Domain.SaveStudentDTO>(resource));
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            var x = _mapper.Map<Api.SaveStudentDTO>(result.Entity);
            return Created("Student Updated Successfully ", x);
        }
        /// <summary>
        ///  Add new  Student 
        /// </summary>
        /// <param name="studentkey"> student key</param>
        /// <param name="resource"> Save Student DTO</param>
        /// <returns></returns>
        [HttpPut("{studentkey}")]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.SaveStudentDTO>> UpdateStudents_UsingLock(Guid studentkey, Api.SaveStudentDTO resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.GetErrorMessages().Aggregate((p, n) => p + ", " + n) });

            var result = await _StudentService.UpdateStudentAsync_Lock(studentkey, _mapper.Map<Domain.SaveStudentDTO>(resource));
            //apply Distributed lock 
            //  Task task1 = Task.Run(async () => _StudentService.UpdateStudentAsync_Lock(studentkey, _mapper.Map<Domain.SaveStudentDTO>(resource)));
            //  Task task2 = Task.Run(async () =>  _StudentService.UpdateStudentAsync_Lock(studentkey, _mapper.Map<Domain.SaveStudentDTO>(resource)));
            //  await Task.WhenAll(task1, task2);

            if (!result.Success)
                return BadRequest(new { message = result.Message });
            var x = _mapper.Map<Api.SaveStudentDTO>(result.Entity);
            return Created("Student Updated Successfully ", x);
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

        /// <summary>
        ///        Upload student profile Img  
        /// </summary>
        /// <param name="files">files to upload</param>
        /// <returns>uploaded files names sperated by #</returns>
        ///<response code="200">return uploaded files names sperated by #</response>
        ///<response code="400">files not uploaded with error message</response>
        ///<response code="401">user not authorized</response>
        [HttpPost("UploadFiles")]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<IActionResult> UploadFilesAsync(List<IFormFile> files)
        {

            //if (files.Any(f => f.Length > 10485760)) return BadRequest(new { message = Resource.MaxFileSize });
            if (!files.Any(f => f.Length > 0)) return BadRequest(new { message = "Invalid Input" });
            var filesNames = new StringBuilder();
            var fileName = "";

            foreach (var formFile in files.Where(f => f.Length > 0))
            {
                fileName = formFile.FileName.UniqueCleanFileName();
                var filePath = _fileProvider?.MapPath(Path.Combine(FilesUploadPaths.Students, fileName));
                if (System.IO.File.Exists(filePath))
                {
                    fileName = fileName.UniqueCleanFileName();
                    filePath = _fileProvider?.MapPath(Path.Combine(FilesUploadPaths.Students, fileName));
                }
                //if path not exist

                //string Path_ = _fileProvider?.MapPath(FilesUploadPaths.Students);
                //if (!Directory.Exists(Path_))
                //{
                //    Directory.CreateDirectory(Path_);
                //}

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
                filesNames.Append("#" + fileName);

            }

            return Ok(new
            {
                FileName = filesNames.ToString(),
                FilePath = string.IsNullOrWhiteSpace(filesNames.ToString()) ? null : FilesUploadPaths.Students + "/" + filesNames.Replace("#", ""),

            });
        }

    }
}