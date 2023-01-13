namespace Infrastructure.Interfaces
{
    public interface ISessionsRepository
    {
        int CreateSessionAndGetSessionId(int userId);
        void CloseSessionById(int sessionId);
        void CloseSessionForUser(int userId);
        int GetUserBySessionId(int sessionId);
    }
}
