using Microsoft.Xna.Framework;
using Super;
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
        }

        public void PlayCard(PlayableCard card)
        {
            int cardIdx = this.cardsInHand.IndexOf(card);

            if(cardIdx != -1)
            {
                this.cardsInHand.RemoveAt(cardIdx);
            }
        }

        public void UpdateDisplay()
        {
            for(int i = 0; i < this.cardsInHand.Count; ++i)
            {
                PlayableCard card = this.cardsInHand[i];

                card.texture.drawRect = new Rectangle(i * card.texture.texture.Width, 0, card.texture.texture.Width, card.texture.texture.Height);
            }
        }
    }
}
