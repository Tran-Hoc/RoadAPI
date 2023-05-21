using AutoMapper;
using RoadAPI.Data;
using RoadAPI.DataContext;
using RoadAPI.Interface;
using RoadAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace RoadAPI.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RoadAndOtherApiContext _context;
        public readonly IMapper _mapper;
        string path = "wwwroot\\images\\account";
        public AccountRepository(IMapper mapper, RoadAndOtherApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public bool Add(AccountModel model)
        {
            try
            {
                Account _model = new Account();
                _model = _mapper.Map<AccountModel, Account>(model);
                _model.PathToImage = saveImage(model.Image, (Guid)_model.Id);

                _context.Accounts.Add(_model);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
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
        public List<AccountViewModel> GetAll()
        {
            var data = _mapper.Map<List<Account>, List<AccountViewModel>>(_context.Accounts.ToList());
            return data;
        }
        public bool DeleteById(Guid id)
        {
           var item = _context.Accounts.FirstOrDefault(x => x.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.Accounts.Remove(item);
            _context.SaveChanges();
            return true;
        }



        public AccountViewModel GetById(Guid id)
        {
            var item = _context.Accounts.FirstOrDefault(y => y.Id == id);

            if(item == null)
            {
                return null;
            }
            return _mapper.Map<Account, AccountViewModel>(item);

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

        public bool Update(AccountModel model, Guid id)
        {
            // Get the acccount item with the specified ID
            var item = _context.Accounts.FirstOrDefault(n => n.Id == id);

            // Check if the account item exists
            if (item == null)
            {
                return false;
            }

            // Update the account item with the data from the model
     
            _mapper.Map(model, item);
            item.PathToImage = saveImage(model.Image, (Guid)item.Id);
       
            // Save the changes to the database
            _context.Update(item);
            _context.SaveChanges();

            return true;
        }
    }
}
