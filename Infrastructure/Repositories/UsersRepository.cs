using System.Reflection;
using Infrastructure.Interfaces;
using Models.DBModels;

namespace Infrastructure.Repositories
{
    [Obfuscation]
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            if (_context.Users.FirstOrDefault(m => m.UserId == user.UserId) is not null)
            {
                throw new ArgumentException("UserId already exists");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetUser(int customerId)
        {
            return _context.Users.Where(c => c.UserId == customerId).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (user.UserId == null)
            {
                throw new Exception($"UserId {nameof(user.UserId)} is null");
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserByLogin(string login)
        {
            return _context.Users.Where(c => c.Login == login).FirstOrDefault();
        }
    }
}
