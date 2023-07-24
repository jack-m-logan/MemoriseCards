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
    }
}
