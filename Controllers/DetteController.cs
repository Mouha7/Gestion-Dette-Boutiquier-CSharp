using gestion_dette_web.Data;
using gestion_dette_web.Models;
using gestion_dette_web.services;
using gestion_dette_web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace gestion_dette_web.Controllers
{
    public class DetteController : Controller
    {
        private readonly IDetteService _detteService;
        private readonly ILogger<DetteController> _logger;
        private readonly ApplicationDbContext _context;

        public DetteController(ILogger<DetteController> logger, IDetteService detteService, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _detteService = detteService;
        }

        // Index - Affiche tous les dettes
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 2;
            var dettes = _detteService.GetDettes(page, pageSize);
            var totalDettes = _detteService.GetTotalDettes();
            var totalPages = (int)Math.Ceiling((decimal)totalDettes / pageSize);
            if (dettes == null) {
                dettes = new List<Dette>();
            }

            var model = new PaginatedViewModel<Dette>
            {
                Listes = dettes,
                PageIndex = page,
                TotalPages = totalPages,
                HasPreviousPage = page > 1,
                HasNextPage = page < totalPages
            };

            return View(model);
        }

        // Details - Affiche les détails d'un dette spécifique
        public IActionResult Details(int id)
        {
            var dette = _detteService.GetById(id);
            if (dette == null)
            {
                return NotFound();
            }
            return View(dette);
        }

        // Create - Formulaire de création d'un dette
        public IActionResult Create(int clientId)
        {
            var viewModel = new DetteCreateViewModel
            {
                ClientId = clientId,
                AvailableArticles = _context.Articles.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DetteCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Données invalides" });
            }

            try
            {
                var dette = new Dette
                {
                    ClientId = model.ClientId,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Details = model.Details.Select(d => new Detail
                    {
                        ArticleId = d.ArticleId,
                        Montant = (float)d.Montant
                    }).ToList(),
                    Montant = (float)model.Details.Sum(d => d.Montant)
                };

                _context.Dettes.Add(dette);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dette dette)
        {
            if (ModelState.IsValid)
            {
                _detteService.Insert(dette);
                return RedirectToAction(nameof(Index));
            }
            return View(dette);
        }

        // Edit - Formulaire d'édition d'un dette
        public IActionResult Edit(int id)
        {
            var dette = _detteService.GetById(id);
            if (dette == null)
            {
                return NotFound();
            }
            return View(dette);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dette dette)
        {
            if (ModelState.IsValid)
            {
                _detteService.Update(dette);
                return RedirectToAction(nameof(Index));
            }
            return View(dette);
        }

        // Delete - Confirmation de suppression d'un dette
        public IActionResult Delete(int id)
        {
            var dette = _detteService.GetById(id);
            if (dette == null)
            {
                return NotFound();
            }
            return View(dette);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _detteService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
