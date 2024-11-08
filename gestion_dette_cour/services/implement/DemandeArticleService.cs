using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class DemandeArticleService : IDemandeArticleService {
    private IDemandeArticleRepository demandeArticleRepository;

    public DemandeArticleService(IDemandeArticleRepository demandeArticleRepository) {
        this.demandeArticleRepository = demandeArticleRepository;
    }

    public void add(DemandeArticle value) {
        demandeArticleRepository.insert(value);
    }

    public List<DemandeArticle> findAll() {
        return demandeArticleRepository.selectAll();
    }

    public DemandeArticle? findBy(DemandeArticle demandeArticle) {
        foreach (DemandeArticle article in findAll()) {
            if (article.idDemandeArticle == demandeArticle.idDemandeArticle) {
                return article;
            }
        }
        return null;
    }

    public int length() {
        return demandeArticleRepository.size();
    }
    
}
}