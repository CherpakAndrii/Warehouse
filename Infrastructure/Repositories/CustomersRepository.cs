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

        public void CreateCustomer(Customer customer)
        {
            if (_context.Customers.FirstOrDefault(m => m.CustomerId == customer.CustomerId) is not null)
            {
                throw new ArgumentException("CustomerId already exists");
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            if (customer.CustomerId == null)
            {
                throw new Exception($"CustomerId {nameof(customer.CustomerId)} is null");
            }

            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerByLogin(string login)
        {
            return _context.Customers.Where(c => c.Login == login).FirstOrDefault();
        }
    }
}
