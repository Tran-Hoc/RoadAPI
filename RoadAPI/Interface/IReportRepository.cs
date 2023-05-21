using RoadAPI.Models;

namespace RoadAPI.Interface
{
    public interface IReportRepository
    {
        List<ReportViewModel> GetAll();
        ReportViewModel GetById(Guid id);
        bool Add(ReportModel news);
       Task<bool> DeleteById(Guid id);
        bool Update(ReportUpdateModel news, Guid id);
        public FileStream GetImage(string fileName);
    }
}
