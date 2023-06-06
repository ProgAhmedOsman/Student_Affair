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
    [ApiExplorerSettings(GroupName = "ClassRoom")]
    public class ClassRoomController : ControllerBase
    {
        private readonly ILogger<ClassRoomController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClassRoomService _ClassRoomService;

        private readonly IMapper _mapper;
        public ClassRoomController(ILogger<ClassRoomController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IClassRoomService ClassRoomService, IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _ClassRoomService = ClassRoomService;
            _mapper = mapper;
        }

        /// <summary>
        ///       Get All ClassRooms
        /// </summary>
        /// <param name="PagingParametersDto"></param>
        /// <returns>array of ClassRooms data in  requested page and return paging data in response header</returns>     
        ///<response code="200">return all ClassRooms successfully</response>
        ///<response code="401">user not authorized</response>
        [HttpGet]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.DisplayClassRoomDTO>> GetAllAsync([FromQuery] Api.PagingParameters PagingParametersDto)
        {
            var data = await _ClassRoomService.GetAllClassRooms(_mapper.Map<Domain.PagingParameters>(PagingParametersDto));
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
        ///        Add new ClassRoom
        /// </summary>
        /// <param name="classRoomName"> class Room Name </param>
        /// <returns>ActionResult</returns>
        ///<response code="200">Succeeded</response>
        ///<response code="201">Succeeded and return added entity</response>
        ///<response code="400">BadRequest with error message</response>
        ///<response code="401">user not authorized</response>
        [HttpPost]
        [AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<ActionResult<Api.DisplayClassRoomDTO>> PostAsync(string classRoomName)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.GetErrorMessages().Aggregate((p, n) => p + ", " + n) });

            var result = await _ClassRoomService.AddClassRoom(classRoomName);
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            return Created("ClassRoom Added Successfully ", result.Entity);

        }


    }
}