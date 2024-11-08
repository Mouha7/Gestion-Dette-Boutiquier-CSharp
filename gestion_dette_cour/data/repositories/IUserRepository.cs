using Main.Data.Entities;

namespace Main.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void changeStatus(User user, Boolean state);
        List<User> selectAllActifs(int type);
        User? selectByLogin(String login, String password);
    }
}