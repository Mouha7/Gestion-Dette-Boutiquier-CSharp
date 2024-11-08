using Main.Data.Entities;

namespace Main.Views
{
    public interface IApplication
    {
        int menu();
        void run(User user);
        void msgSuccess();
        void msgSuccess(String msg);
        bool isEmpty(int size, String msg);
        void motif(char letter);
        void motif(String letter, int nbr);
        bool isDigit(String number);
        bool isDecimal(String number);
        bool isInteger(String number);
        void msgWelcome(User user);
    }
}