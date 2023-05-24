using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RoadAPI.Data;
using RoadAPI.DataContext;
using RoadAPI.Interface;
using RoadAPI.Models;
using System.IO;

namespace RoadAPI.Services
{

    public class ReportRepository : IReportRepository
    {
        private readonly RoadapiDbContext _context;
        public readonly IMapper _mapper;

        string path = "wwwroot\\images\\report";

        public ReportRepository(RoadapiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Add(ReportModel model)
        {
            try
            {
                Report item = new Report();

                item.Id = Guid.NewGuid();
                item.TimeSend = DateTime.Now;
                item.Location = model.Location;
                item.Status = model.Status;
                item.Content = model.Content;
                item.Image = saveImage(model.Image, item.Id);
                item.IdAccount = model.IdAccount;
                _context.Reports.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteById(Guid id)
        {

            var item = await _context.Reports.FirstOrDefaultAsync(n => n.Id == id);

            // Check if the news item exists
            if (item == null)
            {
                return false;
            }

            deleteImage(item.Image);
            // Delete the news item from the database
            _context.Reports.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<ReportViewModel> GetAll()
        {
            var data = _mapper.Map<List<Report>, List<ReportViewModel>>(_context.Reports.ToList());

            return data;
        }

        public ReportViewModel GetById(Guid id)
        {

            var data = _context.Reports.FirstOrDefault(n => n.Id == id);

            if (data == null)
            {
                return null;
            }

            return _mapper.Map<Report, ReportViewModel>(data);
        }

        public bool Update(ReportUpdateModel model, Guid id)
        {
            try
            {
                var item = _context.Reports.FirstOrDefault(p => p.Id == id);
                if (item == null)
                {
                    return false;
                }
                if (model.Status != null)
                {
                    item.Status = model.Status;
                }
                if (model.Content != null)
                {
                    item.Content = model.Content;
                }

                _mapper.Map(model, item);
                if (model.Image != null)
                {
                    deleteImage(item.Image);
                    item.Image = saveImage(model.Image, item.Id);
                }
                _context.Reports.Update(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //throw new NotImplementedException();
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
