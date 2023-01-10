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

        public int CreateSessionAndGetSessionId(User user)
        {
            CloseSessionForUser(user);
            Session createdSession = new Session() { User = user };
            _context.Sessions.Add(createdSession);
            _context.SaveChanges();
            
            createdSession = _context.Sessions.FirstOrDefault(s => s.User == user);
            return createdSession.SessionId.Value;
        }

        public void CloseSessionById(int sessionId)
        {
            Session session = _context.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session is not null) _context.Sessions.Remove(session);
            _context.SaveChanges();
        }

        public void CloseSessionForUser(User user)
        {
            Session session = _context.Sessions.FirstOrDefault(s => s.User == user);
            if (session is not null) _context.Sessions.Remove(session);
            _context.SaveChanges();
        }

        public User GetUserBySessionId(int sessionId)
        {
            Session session = _context.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session is null) return null;
            return session.User;
        }
    }
}
