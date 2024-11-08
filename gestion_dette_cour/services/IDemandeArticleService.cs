using Main.Data.Entities;

namespace Main.Services
{
    public interface IDemandeArticleService
    {
        void add(DemandeArticle value);
        List<DemandeArticle> findAll();
        DemandeArticle? findBy(DemandeArticle demandeArticle);
        int length();
    }
}