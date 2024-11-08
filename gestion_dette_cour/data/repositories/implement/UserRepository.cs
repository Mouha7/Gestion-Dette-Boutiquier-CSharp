using Main.Core.Repository.Implement;
using Main.Data.Entities;

namespace Main.Data.Repositories.Implement
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public void changeStatus(User user, Boolean state)
        {
            User us = selectBy(user);
            if (us != null)
            {
                us.status = state;
            }
            else
            {
                Console.Error.WriteLine($"User non trouvé.");
            }
        }

        public List<User> selectAllActifs(int type)
        {
            return selectAll().Where(user => user.status && (int)user.role == type).ToList();
        }

        public User? selectByLogin(String login, String password)
        {
            return selectAll().FirstOrDefault(user => user.login == login && password == user.password);
        }
    }
}