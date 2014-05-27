using Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Data
{
    public class PlayerDeck
    {
        public List<OutputCard> mainboard = new List<OutputCard>();
        public List<OutputCard> sideboard = new List<OutputCard>();

        public List<PlayableCard> library = new List<PlayableCard>();

        public void Start()
        {
            this.library.Clear();

            foreach(OutputCard c in mainboard)
            {
                this.library.Add(new PlayableCard(c));
            }

            this.Shuffle();
        }

        public void Shuffle()
        {
            Random r = new Random((int)DateTime.UtcNow.Ticks);

            List<PlayableCard> cardPool = new List<PlayableCard>();

            foreach (PlayableCard c in this.library)
            {
                cardPool.Add(c);
            }

            library.Clear();

            while(cardPool.Count > 0)
            {
                int randIdx = r.Next(cardPool.Count);
                library.Add(cardPool[randIdx]);
                cardPool.RemoveAt(randIdx);
            }
        }

        public PlayableCard Draw()
        {
            if (this.library.Count > 0)
            {
                int lastIdx = this.library.Count - 1;
                PlayableCard c = this.library[lastIdx];

                this.library.RemoveAt(lastIdx);

                return c;
            }
            return null;
        }
    }
}
