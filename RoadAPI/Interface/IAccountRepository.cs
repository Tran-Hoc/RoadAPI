using RoadAPI.Models;

namespace RoadAPI.Interface
{
    public interface IAccountRepository
    {
        public List<AccountViewModel> GetAll();
        AccountViewModel GetById(Guid id);
        bool Add(AccountModel models);
        bool DeleteById(Guid id);
        bool Update(AccountModel model, Guid id);
        public FileStream GetImage(string fileName);
    }
}
