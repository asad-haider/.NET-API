using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using Microsoft.Extensions.Logging;
using DomainModel.RequestModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        protected ILogger _logger { get; }
        private IDepartment _departmentService;

        public DepartmentController(ILoggerFactory loggerFactory, IDepartment departmentService)
        {
            _logger = loggerFactory.CreateLogger(GetType().Namespace);
            _departmentService = departmentService;
        }

        [HttpGet("")]
        [HttpGet("id/{id?}")]
        public async Task<IActionResult> Get(short? id)
        {
            try
            {
                return Ok(await _departmentService.GetDepartment(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddUpdateDepartmentRequestDTO request)
        {
            try
            {
                return Ok(await _departmentService.AddDepartment(request));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]AddUpdateDepartmentRequestDTO request)
        {
            try
            {
                return Ok(await _departmentService.UpdateDepartment(request));
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
                return Ok(await Task.FromResult(_departmentService.DeleteDepartment(id)));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return StatusCode(500, e.Message);
            }
        }
    }
}
