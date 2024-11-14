using gestion_dette_web.Models;
using gestion_dette_web.services;
using Microsoft.AspNetCore.Mvc;

namespace gestion_dette_web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        public record PaginatedClientViewModel
        {
            public required List<Client> Clients { get; init; }
            public int PageIndex { get; init; }
            public int TotalPages { get; init; }
            public bool HasPreviousPage { get; init; }
            public bool HasNextPage { get; init; }
        }

        // Index - Affiche tous les clients
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 10;
            var clients = _clientService.GetClients(page, pageSize);
            var totalClients = _clientService.GetTotalClients();
            var totalPages = (int)Math.Ceiling((decimal)totalClients / pageSize);

            var model = new PaginatedClientViewModel
            {
                Clients = clients,
                PageIndex = page,
                TotalPages = totalPages,
                HasPreviousPage = page > 1,
                HasNextPage = page < totalPages
            };

            return View(model);
        }

        // Details - Affiche les détails d'un client spécifique
        public IActionResult Details(int id)
        {
            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // Create - Formulaire de création d'un client
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientService.Insert(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // Edit - Formulaire d'édition d'un client
        public IActionResult Edit(int id)
        {
            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientService.Update(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // Delete - Confirmation de suppression d'un client
        public IActionResult Delete(int id)
        {
            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _clientService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
