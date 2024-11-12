using gestion_dette_web.Models;
using gestion_dette_web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gestion_dette_web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = (ClientRepository)clientRepository;
        }

        // Index - Affiche tous les clients
        public IActionResult Index()
        {
            var clients = _clientRepository.GetAll();
            return View(clients);
        }

        // Details - Affiche les détails d'un client spécifique
        public IActionResult Details(int id)
        {
            var client = _clientRepository.GetById(id);
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
                _clientRepository.Insert(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // Edit - Formulaire d'édition d'un client
        public IActionResult Edit(int id)
        {
            var client = _clientRepository.GetById(id);
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
                _clientRepository.Update(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // Delete - Confirmation de suppression d'un client
        public IActionResult Delete(int id)
        {
            var client = _clientRepository.GetById(id);
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
            _clientRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
