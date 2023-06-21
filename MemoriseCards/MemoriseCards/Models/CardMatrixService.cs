using System;
using System.IO;
using MemoriseCards.Data;
using MemoriseCards.Services;
using Microsoft.EntityFrameworkCore;

namespace MemoriseCards.Models
{
	public class CardMatrixService
	{
        private readonly MemoriseCardsDbContext _context;

        public CardMatrixService(MemoriseCardsDbContext context)
        {
            _context = context;
        }

        internal void AddPOAToCard(Card card, POA poa)
        {
            if (card.POA != null)
            {
                card.POA.Person = poa.Person;
                card.POA.Object = poa.Object;
                card.POA.Action = poa.Action;
            }
            else
            {
                poa.Card = card;
                card.POA = poa;
                _context.Entry(poa).State = EntityState.Added;
            }

            _context.SaveChanges();
        }

        internal POA CreatePOA(int cardId, string person, string obj, string action)
        {
            var card = _context.Card.Find(cardId);

            if (card != null)
            {
                var poa = new POA(cardId, person, obj, action);

                _context.POA.Add(poa);
                _context.SaveChanges();

                return poa;
            }

            return null;
        }

        internal void DeletePOA(Card card)
        {
            if (card.POA != null)
            {
                _context.POA.Remove(card.POA);
                card.POA = null;
                _context.SaveChanges();
            }
        }

        internal void UpdatePOAPerson(Card card, string updatedPerson)
        {
            if (card.POA != null)
            {
                card.POA.Person = updatedPerson;
                _context.SaveChanges();
            }
        }

        internal void UpdatePOAObject(Card card, string updatedObject)
        {
            if (card.POA != null)
            {
                card.POA.Object = updatedObject;
                _context.SaveChanges();
            }
        }

        internal void UpdatePOAAction(Card card, string updatedAction)
        {
            if (card.POA != null)
            {
                card.POA.Action = updatedAction;
                _context.SaveChanges();
            }
        }

        //internal object UpdatePOAObject(Card card, string updatedObject)
        //{
        //    throw new NotImplementedException();
        //}

        //internal object UpdatePOAAction(Card card, string updatedAction)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

