using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface ICustomersRepository
    {
        void CreateCustomer(User user);
        User GetCustomer(int customerId);
        void UpdateCustomer(User user);
        void DeleteCustomer(User user);

        User GetCustomerByLogin(string login);
        List<User> GetAllCustomers();
    }
}
