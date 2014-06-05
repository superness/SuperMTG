using Microsoft.Xna.Framework;
using Super;
using SuperMtgPlayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class Player
    {
        public PlayerDeck deck = new PlayerDeck();
        public PlayerHand hand = new PlayerHand();
        public ManaPool manaPool = new ManaPool();

        public Common.PlayerType Type = Common.PlayerType.Local;

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
        public void DrawCard()
        {
            if (this.deck.library.Count > 0)
            {
                this.hand.AddCard(this.deck.Draw(), this);
            }
        }
        public void DrawHand()
        {
            this.hand.Reset();

            for(int i = 0; i < handStartSize; ++i)
            {
                this.hand.AddCard(this.deck.Draw(), this);
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
        public void Update(GameTime gt, GraphicsDeviceManager graphics)
        {
            this.hand.Update();
            this.hand.UpdateDisplay(graphics);

            if (PriorityLayer.Global.CurrentActivePlayer == Common.PlayerType.Local)
            {
                // Pass priorty on spacebar
                if(SuperKeyboard.Global.KeyPress(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    PriorityLayer.Global.PassPriorty();
                }
            }
        }

        public virtual void OnPriority()
        {

        }
    }
}
