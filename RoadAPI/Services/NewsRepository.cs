using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadAPI.Data;
using RoadAPI.DataContext;
using RoadAPI.Interface;
using RoadAPI.Models;

namespace RoadAPI.Services
{
    public class NewsRepository : INewsRepository
    {
        private readonly RoadAndOtherApiContext _context;
        public readonly IMapper _mapper;
        string path = "wwwroot\\images\\news";
        //[Obsolete]
        //private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public NewsRepository(RoadAndOtherApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            //_hostingEnvironment = hostingEnvironment;
        }
        public bool Add(NewsModel news)
        {
            try
            {
                News _news = new News();
                _news = _mapper.Map<NewsModel, News>(news);
                _news.PathToImage = saveImage(news.Image, _news.Id);
                _news.PublishedAt = DateTime.Now;

                _context.News.Add(_news);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteById(Guid id)
        {
            // Get the news item with the specified ID
            var newsItem = _context.News.FirstOrDefault(n => n.Id == id);

            // Check if the news item exists
            if (newsItem == null)
            {
                return false;
            }

            // Delete the news item from the database
            _context.News.Remove(newsItem);
            _context.SaveChanges();
            return true;
        }

        public List<NewsViewModel> GetAll()
        {
            var data = _mapper.Map<List<News>, List<NewsViewModel>>(_context.News.ToList());
            return data;
        }

        public NewsViewModel GetById(Guid id)
        {
            var newsItem = _context.News.FirstOrDefault(n => n.Id == id);

            // Check if the news item exists
            if (newsItem == null)
            {
                return null;
            }
            return _mapper.Map<News, NewsViewModel>(newsItem);
        }

        public bool Update(NewsModel model, Guid id)
        {
            // Get the news item with the specified ID
            var newsItem = _context.News.FirstOrDefault(n => n.Id == id);

            // Check if the news item exists
            if (newsItem == null)
            {
                return false;
            }

            // Update the news item with the data from the model
            //newsItem.Id = id;
            //newsItem.Title = model.Title;
            //newsItem.Author = model.Author;
            //newsItem.Content = model.Content;

            //newsItem.PathToImage = saveImage(model.Image, newsItem.Id);
            //newsItem.PublishedAt = DateTime.Now;

            // automapper
            _mapper.Map(model, newsItem);


            if (model.Image != null)
            {   // delete image
                deleteImage(newsItem.PathToImage);
                //update new image
                newsItem.PathToImage = saveImage(model.Image, newsItem.Id);

            }


            newsItem.PublishedAt = DateTime.Now;

            // Save the changes to the database
            _context.Update(newsItem);
            _context.SaveChanges();

            return true;
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
