using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface ICustomersRepository
    {
        void CreateCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);

        Customer GetCustomerByLogin(string login);
        List<Customer> GetAllCustomers();
    }
}
