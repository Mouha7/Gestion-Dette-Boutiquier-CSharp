using Main.Core.Repository.Implement;
using Main.Data.Entities;

namespace Main.Data.Repositories.Implement
{
    public class ArticleRepository : Repository<Article> , IArticleRepository {
    public ArticleRepository() : base() {}
    public void updateQte(Article article, int newQte) {
        Article art = selectBy(article);
        if (art != null) {
            art.qteStock = newQte;
        } else {
            Console.Error.WriteLine("Erreur lors de la mise à jour de l'article.");
        }
    }

    public List<Article> selectAllAvailable() {
        return selectAll().Where(article => article.qteStock != 0)
                .ToList();
    }
}
}