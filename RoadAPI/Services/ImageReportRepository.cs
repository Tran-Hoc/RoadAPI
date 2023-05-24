using AutoMapper;
using RoadAPI.Data;
using RoadAPI.DataContext;
using RoadAPI.Interface;
using RoadAPI.Models;
using System.Drawing;

namespace RoadAPI.Services
{
    public class ImageReportRepository : IImageReportRepository
    {

        private readonly RoadAndOtherApiContext _context;
        public readonly IMapper _mapper;
        string path = "wwwroot\\images\\report";
        string pathImageProcess = "wwwroot\\images\\reportProcess";

        public ImageReportRepository(RoadAndOtherApiContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;
        }
        public async Task<bool> Add(ImageReportModel model)
        {
            try
            {
                //ImageReport _model = new ImageReport();
                //_model = _mapper.Map<ImageReportModel, ImageReport >(model);
                //_model.Image = saveImage(news.Image, _news.Id);
                //_model.PublishedAt = DateTime.Now;

                //_context.News.Add(_news);
                //_context.SaveChanges();
               byte[] byteImage = await ImageToByteArray(model.Image);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ImageReportModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ImageReportModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public bool Update(ImageReportModel news, Guid id)
        {
            throw new NotImplementedException();
        }


        public async Task<byte[]> ImageToByteArray(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    return fileBytes;
                }
            }
            return null;
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

        private string saveImage(IFormFile? image, Guid Id)
        {
            if (image != null)
            {
                // Get the file name and ensure it is unique
                var fileName = Path.GetFileName(image.FileName);
                fileName = Id.ToString() + Path.GetExtension(fileName);


                path = Path.Combine(Directory.GetCurrentDirectory(), path);

                // if not exists create that
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Save the file to a specific location
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                // Update the model with the path to the saved image
                string result = fileName;
                return result;

            }
            else
            {
                return null;
            }

        }
        public FileStream GetImage(string fileName)
        {

            path = Path.Combine(Directory.GetCurrentDirectory(), path);

            // Get the path to the image file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }


            var fileStream = new FileStream(filePath, FileMode.Open);
            return fileStream;

        }
        private void deleteImage(string fileName)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), path);

            // Get the path to the image file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


    }
}
