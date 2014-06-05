using Microsoft.Xna.Framework.Graphics;
using Super;
using SuperMtgPlayer.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMtgPlayer.Data;

namespace SuperMtgPlayer
{
    public class PlayableCard
    {
        private int id;

        private static int currentId = 0;

        public OutputCard card;
        public CardDisplay display;

        public PlayableCard(OutputCard _card)
        {
            this.card = _card;

            this.id = currentId;

            ++PlayableCard.currentId;
        }

        public void DisplayCard()
        {
            this.display = CardDisplayFactory.Global.Create();
            this.display.Init(this.GetCardTexture());

            this.display.TargetLocation.X.BlendSpeed = 20.0f;
            this.display.TargetLocation.Y.BlendSpeed = 20.0f;
        }

        private SuperTexture GetCardTexture()
        {
            SuperTexture texture = DisplayFactory.Global.Create();
            texture.Load(string.Format("CardImgs\\{0}.jpg", this.card.multiverseid));

            return texture;
        }

        public int ID
        {
            get { return this.id; }
        }
    }
}
