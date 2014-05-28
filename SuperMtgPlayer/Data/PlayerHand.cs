using Microsoft.Xna.Framework;
using Super;
using SuperMtgPlayer.Display;
using SuperMtgPlayer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Data
{
    public class PlayerHand
    {
        private List<PlayableCard> cardsInHand = new List<PlayableCard>();
        private static Rectangle cardSize = new Rectangle(0, 0, 184, 256);

        public Point Location = new Point(0, 0);

        public List<PlayableCard> Hand
        {
            get { return cardsInHand; }
        }

        public void Reset()
        {
            this.cardsInHand.Clear();
        }

        public void AddCard(PlayableCard card)
        {
            this.cardsInHand.Add(card);
            card.DisplayCard();
        }

        public void PlayCard(PlayableCard card)
        {
            int cardIdx = this.cardsInHand.IndexOf(card);

            if(cardIdx != -1)
            {
                this.cardsInHand[cardIdx].display.Unload();
                this.cardsInHand.RemoveAt(cardIdx);
            }
        }

        public void Update()
        {
            if (SuperMouse.Global.LeftMouseClick())
            {
                PlayableCard highlighted = this.GetHighlightedCard();
                if (highlighted != null)
                {
                    CardPlayer.Global.TryPlayCard(Globals.Global.localPlayer, highlighted);
                }
            }
        }

        public void UpdateDisplay(GraphicsDeviceManager graphics)
        {
            for(int i = 0; i < this.cardsInHand.Count; ++i)
            {
                PlayableCard card = this.cardsInHand[i];

                card.display.TargetLocation.X.targetValue = (int)(i * PlayerHand.cardSize.Width / 1.5) + this.Location.X;
                card.display.TargetLocation.Y.targetValue = 0 + this.Location.Y;

                if(card.display.isHighlighted == false)
                    card.display.texture.zOrder = 1.0f - ((float)i / this.cardsInHand.Count);
            }

            this.Location = new Point((graphics.PreferredBackBufferWidth - this.HandDisplayWidth()) / 2, graphics.PreferredBackBufferHeight - CardDisplay.baseDimensions.Height);
        }

        public int HandDisplayWidth()
        {
            return (int)(CardDisplay.baseDimensions.Width * this.Hand.Count / 1.5);
        }

        public PlayableCard GetHighlightedCard()
        {
            for(int i = this.cardsInHand.Count - 1; i >= 0; --i)
            {
                if (this.cardsInHand[i].display.isHighlighted)
                {
                    return this.cardsInHand[i];
                }
            }

            return null;
        }
    }
}
