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
        private readonly RoadAndOtherApiContext _context;
        public readonly IMapper _mapper;
        public ReportRepository(RoadAndOtherApiContext context, IMapper mapper)
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
                item.TimeSend = model.TimeSend;
                item.Location = model.Location;
                item.Status = model.Status;
                item.Content = model.Content;

                //item = _mapper.Map<ReportModel, Report>(model);

                foreach (var child in model.Image)
                {
                    ImageReport imageReport = new ImageReport()
                    {
                        Id = Guid.NewGuid(),
                        ReportId = item.Id
                    };
                    imageReport.PathToImage = saveImage(child, imageReport.Id);
                    _context.ImageReports.Add(imageReport);
                }
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
            foreach (var child in item.ImageReports)
            {
                var childitem = await _context.ImageReports
                                    .FirstOrDefaultAsync(n => n.Id == id);
                if (childitem != null)
                {
                    deleteImage(childitem.PathToImage);
                    _context.ImageReports.Remove(childitem);
                }
            }
            // Delete the news item from the database
            _context.Reports.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<ReportViewModel> GetAll()
        {
           if(_context != null)
            {
                // _context is not empty
                var data = _context.Reports
                        .Select(p => new ReportViewModel
                        {
                            Id = p.Id,
                            TimeSend = p.TimeSend,
                            Location = p.Location,
                            Status = p.Status,
                            Content = p.Content,
                            ImageReports = p.ImageReports.Select(c => new ImageReport
                            {
                                Id = c.Id,
                                PathToImage = c.PathToImage,
                                ResultProcessing = c.ResultProcessing
                            }).ToList()
                        })
                        .ToList();
                return data;
            }

            // _context is empty
            return null;
        }

        public ReportViewModel GetById(Guid id)
        {
            
            var data = _context.Reports
                .Where(p => p.Id == id)
                .Select(p => new ReportViewModel
                {
                    Id = p.Id,
                    TimeSend = p.TimeSend,
                    Location = p.Location,
                    Status = p.Status,
                    Content = p.Content,
                    ImageReports = p.ImageReports.Select(c => new ImageReport
                    {
                        Id = c.Id,
                        PathToImage = c.PathToImage,
                        ResultProcessing = c.ResultProcessing
                    }).ToList()
                })
                .FirstOrDefault();

            if (data == null)
            {
                return null;
            }

            return data;
        }

        public bool Update(ReportUpdateModel model, Guid id)
        {
            try
            {
                Report item = _context.Reports.FirstOrDefault(p => p.Id == id);
                if (item == null)
                {
                    return false;
                }

                item.Status = model.Status;
                item.Content = model.Content;

                //_mapper.Map(item, model);

                //item = _mapper.Map<ReportModel, Report>(model);

                //foreach (var child in model.Image)
                //{
                //    Id =
                //    ImageReport imageReport = new ImageReport()
                //    {

                //        ReportId = item.Id
                //    };
                //    imageReport.PathToImage = saveImage(child, imageReport.Id);
                //    _context.Add(imageReport);
                //}

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


        string path = "wwwroot\\images\\report";
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
