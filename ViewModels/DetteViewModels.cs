using gestion_dette_web.Models;

namespace gestion_dette_web.ViewModels
{
    public record PaginatedViewModel<T>
    {
        public required List<T> Listes { get; init; }
        public int PageIndex { get; init; }
        public int TotalPages { get; init; }
        public bool HasPreviousPage { get; init; }
        public bool HasNextPage { get; init; }
    }

    public record DetteDetailViewModel
    {
        public int ArticleId { get; set; }
        public decimal Montant { get; set; }
    }

    public record DetteCreateViewModel
    {
        public int ClientId { get; set; }
        public List<DetteDetailViewModel> Details { get; set; } = new List<DetteDetailViewModel>();
        public required List<Article> AvailableArticles { get; set; }
    }
}