using Infrastructure.Interfaces;
using Models.DBModels;

namespace Infrastructure.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        private readonly AppDbContext _context;

        public SessionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public int CreateSessionAndGetSessionId(int userId)
        {
            CloseSessionForUser(userId);
            Session createdSession = new Session() { UserId = userId };
            _context.Sessions.Add(createdSession);
            _context.SaveChanges();
            
            createdSession = _context.Sessions.FirstOrDefault(s => s.UserId == userId);
            return createdSession.SessionId.Value;
        }

        public void CloseSessionById(int sessionId)
        {
            Session session = _context.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session is not null) _context.Sessions.Remove(session);
            _context.SaveChanges();
        }

        public void CloseSessionForUser(int userId)
        {
            Session session = _context.Sessions.FirstOrDefault(s => s.UserId == userId);
            if (session is not null) _context.Sessions.Remove(session);
            _context.SaveChanges();
        }

        public int GetUserBySessionId(int sessionId)
        {
            Session session = _context.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session is null) return -1;
            return session.UserId;
        }
    }
}
