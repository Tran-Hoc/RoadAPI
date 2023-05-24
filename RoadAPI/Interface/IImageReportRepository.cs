using RoadAPI.Models;

namespace RoadAPI.Interface
{
    public interface IImageReportRepository
    {
        List<ImageReportModel> GetAll();
        ImageReportModel GetById(Guid id);
        Task<bool> Add(ImageReportModel model);
        bool DeleteById(Guid id);
        bool Update(ImageReportModel news, Guid id);
        public FileStream GetImage(string fileName);
    }
}
