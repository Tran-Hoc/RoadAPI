using Microsoft.AspNetCore.Mvc;
using RoadAPI.Interface;
using RoadAPI.Models;
using RoadAPI.Services;

namespace RoadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportRepository _repository;

        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_repository.GetAll());

            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetId(Guid id)
        {
            try
            {
                if(id == Guid.Empty)
                {
                    return BadRequest();
                }
                return Ok(_repository.GetById(id));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ReportModel model)
        {
            try
            {
                return Ok(_repository.Add(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("Image")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            try
            {
                var result = _repository.GetImage(imageName);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        public IActionResult Update([FromForm] ReportUpdateModel model, Guid id)
        {
            try
            {
                return Ok(_repository.Update(model, id));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                return Ok(_repository.DeleteById(id));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
