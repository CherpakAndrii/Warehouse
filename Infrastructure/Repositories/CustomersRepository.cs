using Infrastructure.Interfaces;
using Models.DBModels;

namespace Infrastructure.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly AppDbContext _context;

        public CustomersRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCustomer(User user)
        {
            if (_context.Customers.FirstOrDefault(m => m.CustomerId == user.CustomerId) is not null)
            {
                throw new ArgumentException("CustomerId already exists");
            }

            _context.Customers.Add(user);
            _context.SaveChanges();
        }

        public User GetCustomer(int customerId)
        {
            return _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
        }

        public void UpdateCustomer(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (user.CustomerId == null)
            {
                throw new Exception($"CustomerId {nameof(user.CustomerId)} is null");
            }

            _context.Customers.Update(user);
            _context.SaveChanges();
        }

        public void DeleteCustomer(User user)
        {
            _context.Customers.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public User GetCustomerByLogin(string login)
        {
            return _context.Customers.Where(c => c.Login == login).FirstOrDefault();
        }
    }
}
