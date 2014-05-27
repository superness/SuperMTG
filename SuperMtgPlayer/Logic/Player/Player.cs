using Microsoft.Xna.Framework;
using Super;
using SuperMtgPlayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic.Player
{
    public class Player : IPlayer
    {
        PlayerDeck deck = new PlayerDeck();
        PlayerHand hand = new PlayerHand();

        int handStartSize = 7;

        public void AddCardToDeck(OutputCard card, bool mainboard)
        {
            if (mainboard)
            {
                if (deck.mainboard.Count < 60)
                {
                    deck.mainboard.Add(card);
                }
            }
            else
            {
                if (deck.sideboard.Count < 15)
                {
                    deck.sideboard.Add(card);
                }
            }
        }
        public void AddCardsToDeck(List<OutputCard> cards, bool mainboard)
        {
            foreach(OutputCard c in cards)
            {
                this.AddCardToDeck(c, mainboard);
            }
        }
        public void RemoveCardFromDeck(OutputCard card, bool mainboard)
        {
            if(mainboard)
            {
                deck.mainboard.Remove(card);
            }
            else
            {
                deck.sideboard.Remove(card);
            }
        }
        public PlayerDeck GetDeck()
        {
            return this.deck;
        }
        public void GameStart()
        {
            this.deck.Start();
            this.DrawHand();
        }
        public void DrawHand()
        {
            this.hand.Reset();

            for(int i = 0; i < handStartSize; ++i)
            {
                this.hand.AddCard(this.deck.Draw());
            }
        }
        public void Mulligan()
        {
            this.handStartSize--;

            this.deck.Shuffle();
            this.DrawHand();
        }
        public List<PlayableCard> GetCardsInHand()
        {
            return this.hand.Hand;
        }
        public void Update(GameTime gt)
        {
            this.hand.UpdateDisplay();
        }
    }
}
