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
    [ApiExplorerSettings(GroupName = "Files")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVirtualFileProvider _fileProvider;
        public FilesController(ILogger<FilesController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IVirtualFileProvider fileProvider)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }

        /// <summary>
        ///        Upload Files  
        /// </summary>
        /// <param name="files">files to upload</param>
        /// <returns>uploaded files names sperated by #</returns>
        ///<response code="200">return uploaded files names sperated by #</response>
        ///<response code="400">files not uploaded with error message</response>
        ///<response code="401">user not authorized</response>
        [HttpPost("UploadFiles")]
        //[AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<IActionResult> UploadFilesAsync(List<IFormFile> files)
        {

            //if (files.Any(f => f.Length > 10485760)) return BadRequest(new { message = Resource.MaxFileSize });
            if (!files.Any(f => f.Length > 0)) return BadRequest(new { message = "Invalid Input" });
            var filesNames = new StringBuilder();
            var fileName = "";

            foreach (var formFile in files.Where(f => f.Length > 0))
            {
                fileName = formFile.FileName.UniqueCleanFileName();
                var filePath = _fileProvider?.MapPath(Path.Combine(FilesUploadPaths.InitialLocation, fileName));
                if (System.IO.File.Exists(filePath))
                {
                    fileName = fileName.UniqueCleanFileName();
                    filePath = _fileProvider?.MapPath(Path.Combine(FilesUploadPaths.InitialLocation, fileName));
                }
                //if path not exist

                //string Path_ = _fileProvider?.MapPath(FilesUploadPaths.Filess);
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
                FilePath = string.IsNullOrWhiteSpace(filesNames.ToString()) ? null : FilesUploadPaths.InitialLocation + "/" + filesNames.Replace("#", ""),

            });
        }
        /// <summary>
        ///        Upload one  File   
        /// </summary>
        /// <param name="files">files to upload</param>
        /// <returns>uploaded files names sperated by #</returns>
        ///<response code="200">return uploaded files names sperated by #</response>
        ///<response code="400">files not uploaded with error message</response>
        ///<response code="401">user not authorized</response>
        [HttpPost("UploadFile")]
        //[AuthorizeApiUser(Roles = new[] { Roles.SuperAdmin })]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {

            //if (files.Any(f => f.Length > 10485760)) return BadRequest(new { message = Resource.MaxFileSize });
            if (!(file.Length > 0)) return BadRequest(new { message = "Invalid Input" });
            var filesNames = new StringBuilder();
            var fileName = "";


            fileName = file.FileName.UniqueCleanFileName();
            var filePath = _fileProvider?.MapPath(Path.Combine(FilesUploadPaths.InitialLocation, fileName));
            if (System.IO.File.Exists(filePath))
            {
                fileName = fileName.UniqueCleanFileName();
                filePath = _fileProvider?.MapPath(Path.Combine(FilesUploadPaths.InitialLocation, fileName));
            }

            //  if path not exist
            string Path_ = _fileProvider?.MapPath(FilesUploadPaths.InitialLocation);
            if (!Directory.Exists(Path_)) Directory.CreateDirectory(Path_);


            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            filesNames.Append("#" + fileName);



            return Ok(new
            {
                FileName = filesNames.ToString(),
                FilePath = string.IsNullOrWhiteSpace(filesNames.ToString()) ? null : FilesUploadPaths.InitialLocation + "/" + filesNames.Replace("#", ""),
            });
        }

    }
}