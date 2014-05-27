using Microsoft.Xna.Framework.Graphics;
using Super;
using SuperMtgPlayer.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Data
{
    public class PlayableCard
    {
        private OutputCard card;
        private int id;

        private static int currentId = 0;

        public SuperTexture texture;

        public PlayableCard(OutputCard _card)
        {
            this.card = _card;

            this.id = currentId;

            ++PlayableCard.currentId;

            this.texture = this.GetCardTexture();
        }

        private SuperTexture GetCardTexture()
        {
            return DisplayFactory.Global.CreateTexture(string.Format("CardImgs\\{0}.jpg", this.card.multiverseid));
        }

        public int ID
        {
            get { return this.id; }
        }
    }
}
