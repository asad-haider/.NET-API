using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Business.Interfaces;
using DomainModel.RequestModels;
using WebAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        protected ILogger _logger { get; }
        private IStudent _studentInfoService;

        public StudentController(ILoggerFactory loggerFactory, IStudent studentInfoService)
        {
            _logger = loggerFactory.CreateLogger(GetType().Namespace);
            _studentInfoService = studentInfoService;
        }

        [HttpGet("")]
        [HttpGet("id/{id?}")]
        public async Task<IActionResult> Get(short? id, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                if (id.HasValue)
                {
                    return Ok(await _studentInfoService.GetStudent(id.Value));
                }
                else
                {
                    return Ok(await _studentInfoService.GetStudentsInfo(pageNumber, pageSize));
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddUpdateStudentRequestDTO request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _studentInfoService.AddStudentInfo(request));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return StatusCode(500, e.Message);
            }
        }

        //[HttpPost("upload")]
        //public async Task<IActionResult> Post(IFormFile file)
        //{
        //    var stream = file.OpenReadStream();
        //    var name = file.FileName;

        //    return null; //null just to make error free
        //}

        [HttpPost("upload")]
        public async void UploadFiles(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                size += file.Length;

                if (!System.IO.Directory.Exists("Uploads"))
                {
                    System.IO.DirectoryInfo directory = System.IO.Directory.CreateDirectory("Uploads");
                }

                using (FileStream fs = System.IO.File.Create("Uploads/" + file.FileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]AddUpdateStudentRequestDTO request)
        {
            try
            {
                return Ok(await _studentInfoService.UpdateStudentInfo(request));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/Hosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Task.FromResult(_studentInfoService.DeleteStudentInfo(id)));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return StatusCode(500, e.Message);
            }
        }
    }
}
