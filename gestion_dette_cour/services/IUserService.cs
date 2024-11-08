using Main.Data.Entities;

namespace Main.Services
{
    public interface IUserService
    {
        void add(User value);
        List<User> findAll();
        User? findBy(User user);
        User? findBy(List<User> users, User user);
        void setStatus(User user, Boolean state);
        List<User> getAllActifs(int type, User userConnect);
        User? getByLogin(String login, String password);
        int length();
        void update(List<User> users, User updateUser);
    }
}