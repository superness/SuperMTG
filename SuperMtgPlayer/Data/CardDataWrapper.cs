using Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Data
{
    public class CardDataWrapper
    {
        OutputData cardData;

        public CardDataWrapper(OutputData _cardData)
        {
            this.cardData = _cardData;
        }

        public OutputCard GetCard(string name)
        {
            return Array.Find<OutputCard>(this.cardData.cards, c => c.name == name);
        }

        public List<OutputCard> GetCards(string[] names)
        {
            List<OutputCard> cards = new List<OutputCard>();

            foreach(string n in names)
            {
                cards.Add(this.GetCard(n));
            }

            return cards;
        }
    }
}
