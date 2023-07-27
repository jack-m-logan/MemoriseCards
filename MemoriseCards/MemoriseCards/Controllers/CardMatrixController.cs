using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoriseCards.Data;
using MemoriseCards.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemoriseCards.Controllers
{
    public class CardMatrixController : Controller
    {
        private readonly MemoriseCardsDbContext _context;
        private readonly DeckBuilder _deckBuilder;

        public CardMatrixController(MemoriseCardsDbContext context, DeckBuilder deckBuilder)
        {
            _context = context;
            _deckBuilder = deckBuilder;
        }

        public IActionResult Index(int deckId)
        {
            var deck = _deckBuilder.GetDeckById(deckId);

            ViewBag.Deck = deck;

            return View();
        }
    }

}

