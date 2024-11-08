using Main.Data.Entities;

namespace Main.Views
{
    public interface IDetteView : IView<Dette>
    {
        void display(List<Dette> dettes);
        void displayDette(Dette dette);
        void displayDetail(Dette dette);
        void displayPay(Dette dette);
    }
}