using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
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
            var userId = GetCurrentUserId();

            var decks = userId != null ? _deckBuilder.GetDecksByUserId(userId) : _deckBuilder.GetAllDecksForGuestUser();

            var deckList = decks.Select(deck => new { id = deck.Id, name = deck.Name }).ToList();

            return Json(deckList);
        }

        private int? GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }

            return null;
        }
    }
}
