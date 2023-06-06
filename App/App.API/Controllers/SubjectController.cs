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
    [ApiExplorerSettings(GroupName = "Subject")]
    public class SubjectController : ControllerBase
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubjectService _SubjectService;

        private readonly IMapper _mapper;
        public SubjectController(ILogger<SubjectController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISubjectService SubjectService, IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _SubjectService = SubjectService;
            _mapper = mapper;
        }

        /// <summary>
        ///       Get All Subjects
        /// </summary>
        /// <param name="PagingParametersDto"></param>
        /// <returns>array of Subjects data in  requested page and return paging data in response header</returns>     
        ///<response code="200">return all Subjects successfully</response>
        ///<response code="401">user not authorized</response>
        [HttpGet]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.DisplaySubjectDTO>> GetAllAsync([FromQuery] Api.PagingParameters PagingParametersDto)
        {
            var data = await _SubjectService.GetAllSubjects(_mapper.Map<Domain.PagingParameters>(PagingParametersDto));
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
            _logger.LogInformation($"Returned {data.TotalCount} Quiz note from database.");
            return Ok(data);
        }





        /// <summary>
        ///  Add new Subject
        /// </summary>
        /// <param name="subjectNAme"> subjectNAme </param>
        /// <returns>ActionResult</returns>
        ///<response code="200">Succeeded</response>
        ///<response code="201">Succeeded and return added entity</response>
        ///<response code="400">BadRequest with error message</response>
        ///<response code="401">user not authorized</response>
        [HttpPost]
        [AuthorizeApiUser(Roles = new[] { Roles.RegularUser })]
        public async Task<ActionResult<Api.DisplaySubjectDTO>> PostAsync(string subjectName)
        {
           
            var result = await _SubjectService.AddSubject(subjectName);
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            return Created("Subject Added Successfully ", result.Entity);

        }


    }
}