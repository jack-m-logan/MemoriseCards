using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MemoriseCards.Data;
using MemoriseCards.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemoriseCards.Controllers
{
    public class DeckController : Controller
    {
        private readonly MemoriseCardsDbContext _context;
        private readonly DeckBuilder _deckBuilder;

        public DeckController(DeckBuilder deckBuilder)
        {
            _deckBuilder = deckBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewDeck(Deck model)
        {
            if (ModelState.IsValid)
            {
                var deck = _deckBuilder.CreateNewDeck(model.Name);
                return RedirectToAction("Index", "CardMatrix", new { deckId = deck.Id });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetDecksForDropdown()
        {
            // Fetch the decks based on whether the user is logged in or not
            var userId = GetCurrentUserId(); // Implement this method to get the user ID
            var decks = GetDecksByUserId(userId);

            // Convert the decks to a list of objects with id and name properties
            var deckList = decks.Select(deck => new { id = deck.Id, name = deck.Name }).ToList();

            return Json(deckList); // Return the list of decks as JSON response
        }

        // Method to get the decks based on the user ID (or null if the user is not logged in)
        private List<Deck> GetDecksByUserId(int? userId)
        {
            if (userId.HasValue)
            {
                return _context.Deck.Where(d => d.UserId == userId.Value).ToList();
            }
            else
            {
                return _context.Deck.Where(d => d.UserId == null).ToList();
            }
        }

        // Method to get the current user ID (you can use your preferred way to get the user ID)
        private int? GetCurrentUserId()
        {
            // Implement this method to get the user ID from the authentication context
            // For example, using HttpContext.User, User.Identity, or any other method
            // that suits your authentication mechanism.
            // If the user is not logged in, return null.
            // Example pseudocode:
            // var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // return userId != null ? int.Parse(userId) : (int?)null;
            return null; // Replace this with actual implementation
        }
    }
}
