using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {    
        private readonly DevFreelaDbContext _context;
        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public int Create(CreateUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate, inputModel.Password, inputModel.Role);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public UserViewModel GetById(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            return new UserViewModel(user.FullName, user.Email);
        }

        public void Update(UpdateUserInputModel inputModel)
        {
            var user = _context.Users.SingleOrDefault(p => p.Id == inputModel.Id);
            user.Update(inputModel.FullName, inputModel.Email, inputModel.Password, inputModel.BirthDate);
            
            _context.SaveChanges();
        }
    }
}
