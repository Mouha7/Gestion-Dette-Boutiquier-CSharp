using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class UserService : IUserService {
    private IUserRepository userRepository;

    public UserService(IUserRepository userRepository) {
        this.userRepository = userRepository;
    }
    public void add(User value) {
        userRepository.insert(value);
    }
    public List<User> findAll() {
        return userRepository.selectAll();
    }
    public User? findBy(User user) {
        foreach (User us in userRepository.selectAll()) {
            if (us.idUser == user.idUser) {
                return us;
            }
        }
        return null;
    }
    public User? findBy(List<User> users, User user) {
        foreach (User us in users) {
            if (us.idUser == user.idUser) {
                return us;
            }
            if (user.login != null && us.login == user.login) {
                return us;
            }
            if (user.email != null && us.email == user.email) {
                return us;
            }
        }
        return null;
    }
    public void setStatus(User user, bool state) {
        userRepository.changeStatus(user, state);
    }
    public List<User> getAllActifs(int type, User userConnect) {
        return userRepository.selectAllActifs(type)
                .Where(us => us.idUser != userConnect.idUser)
                .ToList();
    }
    public User? getByLogin(String login, String password) {
        return userRepository.selectByLogin(login, password);
    }
    public int length() {
        return userRepository.size();
    }
    public void update(List<User> users, User updateUser) {
        for (int i = 0; i < users.Count; i++) {
            if (users[i].idUser == updateUser.idUser) {
                users[i] = updateUser;
                break; // Sortir de la boucle une fois que la mise à jour est effectuée
            }
        }
    }
}
}