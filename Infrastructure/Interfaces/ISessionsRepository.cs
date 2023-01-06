using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface ISessionsRepository
    {
        int CreateSessionAndGetSessionId(User user);
        void CloseSessionById(int sessionId);
        void CloseSessionForUser(User user);
        User GetUserBySessionId(int sessionId);
    }
}
