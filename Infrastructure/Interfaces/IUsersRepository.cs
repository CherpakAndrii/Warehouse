using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IUsersRepository
    {
        void CreateUser(User user);
        User GetUser(int customerId);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool CheckPassword(string login, string inputedPassword);

        User GetUserByLogin(string login);
        List<User> GetAllUsers();
    }
}
