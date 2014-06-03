using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public int PermLine = 425;
        public int LandLine = 600;
        public int BorderLeft = 200;

        public List<CardInPlay> nonLandsInPlay = new List<CardInPlay>();
        private List<CardInPlay> landsInPlay = new List<CardInPlay>();

        private const int InPlayGrowth = 100;

        public void PutCardIntoPlayForPlayer(PlayableCard card, Player player)
        {
            CardInPlay newCard = CardInPlayFactory.Global.Create();
            newCard.Init(card, player);
            newCard.Display();
            newCard.display.scale.targetValue = 0.5f;
            newCard.display.scale.initValue = 0.5f;
            newCard.display.Growth = InPlayGrowth;

            if (Array.IndexOf(newCard.cardData.types, ("Land")) != -1)
            {
                this.landsInPlay.Add(newCard);
            }
            else
            {
                this.nonLandsInPlay.Add(newCard);
            }
        }

        public void FitCardsToScreen()
        {
            ScaleCardsInPlay(this.landsInPlay);
            ScaleCardsInPlay(this.nonLandsInPlay);
        }

        private void SetLines()
        {
            if(this.landsInPlay.Count > 0 && this.nonLandsInPlay.Count > 0)
            {
                this.LandLine = (int)(this.PermLine + CardDisplay.baseDimensions.Height * 0.7f) + 5;
            }
        }

        private void ScaleCardsInPlay(List<CardInPlay> cards)
        {
            float S1 = 0.7f;

            if (cards.Count > 0)
            {
                int cardSpace = (int)((CardDisplay.baseDimensions.Width * S1) * cards.Count);
                int bufferSpace = (10 * (cards.Count - 1));
                int farRight = this.BorderLeft + cardSpace + bufferSpace;

                int widthTaken = farRight - this.BorderLeft;
                int totalWidth = Game1.Global.graphics.PreferredBackBufferWidth - 50;
                int fitCardsToWidth = totalWidth - bufferSpace - this.BorderLeft;
                float scaleDown = (fitCardsToWidth) / (float)cardSpace;

                float curScale = S1;
                float newScale = Math.Min(S1, curScale * scaleDown);
                scaleDown = newScale / curScale;

                int newCardSpace = (int)((CardDisplay.baseDimensions.Width * newScale) * cards.Count);
                int newBufferSpace = (10 * (cards.Count - 1));
                int newFarRight = this.BorderLeft + newCardSpace + newBufferSpace;

                foreach (CardInPlay c in cards)
                {
                    c.display.scale.targetValue = newScale;
                    c.display.scale.initValue = newScale;

                    c.display.Growth = (int)(InPlayGrowth / scaleDown);
                }
            }
        }

        public void Update(GameTime gt)
        {
            this.SetLines();
            this.FitCardsToScreen();

            for (int i = 0; i < this.landsInPlay.Count; ++i)
            {
                CardInPlay card = this.landsInPlay[i];
                card.Update(gt);

                card.display.TargetLocation.X.targetValue = (i * card.display.BaseDimWidth()) + 10 * i + this.BorderLeft;
                card.display.TargetLocation.Y.targetValue = LandLine;
            }
            for (int i = 0; i < this.nonLandsInPlay.Count; ++i)
            {
                CardInPlay card = this.nonLandsInPlay[i];
                card.Update(gt);

                card.display.TargetLocation.X.targetValue = (i * card.display.BaseDimWidth()) + 10 * i + this.BorderLeft;
                card.display.TargetLocation.Y.targetValue = PermLine;
            }
        }

        public void UntapLocal()
        {

        }

        public void UntapRemote()
        {

        }

        public void ProcessTriggerType(Super.TriggeredAbility.TriggerType type)
        {

        }
    }
}
