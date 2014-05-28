using Microsoft.Xna.Framework;
using SuperMtgPlayer.Data;
using SuperMtgPlayer.Display;
using SuperMtgPlayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class Battlefield : Singleton<Battlefield>
    {

        public const int PermLine = 300;
        public const int LandLine = 500;
        public const int BorderLeft = 200;

        public List<CardInPlay> nonLandsInPlay = new List<CardInPlay>();
        private List<CardInPlay> landsInPlay = new List<CardInPlay>();

        public void PutCardIntoPlayForPlayer(PlayableCard card, Player player)
        {
            CardInPlay newCard = CardInPlayFactory.Global.Create();
            newCard.Init(card, player);
            newCard.Display();
            newCard.display.scale.targetValue = 0.5f;
            newCard.display.scale.initValue = 0.5f;

            if (Array.IndexOf(newCard.cardData.types, ("Land")) != -1)
            {
                this.landsInPlay.Add(newCard);
            }
            else
            {
                this.nonLandsInPlay.Add(newCard);
            }
        }

        public void Update(GameTime gt)
        {
            for (int i = 0; i < this.landsInPlay.Count; ++i)
            {
                CardInPlay card = this.landsInPlay[i];
                card.Update(gt);

                card.display.TargetLocation.X.targetValue = (i * card.display.BaseDimWidth()) + 10 * i + Battlefield.BorderLeft;
                card.display.TargetLocation.Y.targetValue = LandLine;
            }
            for (int i = 0; i < this.nonLandsInPlay.Count; ++i)
            {
                CardInPlay card = this.nonLandsInPlay[i];
                card.Update(gt);

                card.display.TargetLocation.X.targetValue = (i * card.display.BaseDimWidth()) + 10 * i + Battlefield.BorderLeft;
                card.display.TargetLocation.Y.targetValue = PermLine;
            }
        }
    }
}
