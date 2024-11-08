using Main.Data.Entities;

namespace Main.Views
{
    public interface IArticleView : IView<Article> {
        int check(string msg, string msgError);
    }
}