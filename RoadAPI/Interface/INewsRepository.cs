using RoadAPI.Models;

namespace RoadAPI.Interface
{
    public interface INewsRepository
    {
        List<NewsViewModel> GetAll();
        NewsViewModel GetById(Guid id);
        bool Add(NewsModel news);
        bool DeleteById(Guid id);
        bool Update(NewsModel news, Guid id);
        public FileStream GetImage(string fileName);
    }
}
