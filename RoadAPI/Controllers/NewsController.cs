using Microsoft.AspNetCore.Mvc;
using RoadAPI.Interface;
using RoadAPI.Models;

namespace RoadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private INewsRepository _newsRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public NewsController(INewsRepository _newsRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment)
        {
            this._newsRepository = _newsRepository;
            this._hostingEnvironment = _hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_newsRepository.GetAll());

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetId(Guid id)
        {
            try
            {
                return Ok(_newsRepository.GetById(id));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NewsModel model)
        {
            try
            {
                return Ok(_newsRepository.Add(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("Image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                // Get the server path for the wwwroot directory
                string wwwrootPath = _hostingEnvironment.WebRootPath;

                // Create the path for the destination file
                string filename = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(wwwrootPath, "Images", filename);

                // Save the uploaded file to the destination file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the path of the saved image
                return Ok(new { filePath });
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
                var result = _newsRepository.GetImage(imageName, fileStream);
                if ( result == null)
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
        public IActionResult Update([FromForm]  NewsModel model, Guid id)
        {
            try
            {
                return Ok(_newsRepository.Update(model, id));

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
                return Ok(_newsRepository.DeleteById(id));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}