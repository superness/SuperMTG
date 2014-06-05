using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Super;
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
        public int LocalPermLine = 450;
        public int LocalLandLine = 600;
        public int RemotePermLine = 250;
        public int RemoteLandLine = 25;
        public int BorderLeft = 200;

        public List<CardInPlay> localNonLandsInPlay = new List<CardInPlay>();
        public List<CardInPlay> localLandsInPlay = new List<CardInPlay>();

        public List<CardInPlay> remoteNonLandsInPlay = new List<CardInPlay>();
        public List<CardInPlay> remoteLandsInPlay = new List<CardInPlay>();

        private const int InPlayGrowth = 100;

        public void PutCardIntoPlayForPlayer(PlayableCard card, Player player)
        {
            if(player.Type == Common.PlayerType.Local)
            {
                this.PutCardIntoPlayForPlayerInternal(card, player, this.localLandsInPlay, this.localNonLandsInPlay);
            }
            else
            {
                this.PutCardIntoPlayForPlayerInternal(card, player, this.remoteLandsInPlay, this.remoteNonLandsInPlay);
            }
        }

        private void PutCardIntoPlayForPlayerInternal(PlayableCard card, Player player, List<CardInPlay> storageLands, List<CardInPlay> storageNonLands)
        {
            CardInPlay newCard = CardInPlayFactory.Global.Create();
            newCard.Init(card, player);
            newCard.Display();
            newCard.display.scale.targetValue = 0.5f;
            newCard.display.initScale = 0.5f;
            newCard.display.Growth = InPlayGrowth;

            if (Array.IndexOf(newCard.cardData.types, ("Land")) != -1)
            {
                storageLands.Add(newCard);
            }
            else
            {
                storageNonLands.Add(newCard);
            }
        }

        public void FitCardsToScreen()
        {
            ScaleCardsInPlay(this.localLandsInPlay);
            ScaleCardsInPlay(this.localNonLandsInPlay);
            ScaleCardsInPlay(this.remoteLandsInPlay);
            ScaleCardsInPlay(this.remoteNonLandsInPlay);
        }

        private void SetLines()
        {
            if (this.localLandsInPlay.Count > 0 && this.localNonLandsInPlay.Count > 0)
            {
                this.LocalLandLine = (int)(this.LocalPermLine + CardDisplay.baseDimensions.Height * 0.7f) + 5;
            }
            if (this.remoteNonLandsInPlay.Count > 0 && this.remoteLandsInPlay.Count > 0)
            {
                this.RemotePermLine = (int)(this.RemoteLandLine + CardDisplay.baseDimensions.Height * 0.7f) + 20;
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
                    c.display.initScale = newScale;

                    c.display.Growth = (int)(InPlayGrowth / scaleDown);
                }
            }
        }

        public void Update(GameTime gt)
        {
            this.SetLines();
            this.FitCardsToScreen();

            foreach (CardInPlay card in this.localLandsInPlay)
            {
                card.Update(gt);
            }
            foreach (CardInPlay card in this.localNonLandsInPlay)
            {
                card.Update(gt);
            }
            foreach (CardInPlay card in this.remoteLandsInPlay)
            {
                card.Update(gt);
            }
            foreach (CardInPlay card in this.remoteNonLandsInPlay)
            {
                card.Update(gt);
            }

            this.OrderCardList(this.localLandsInPlay, this.LocalLandLine);
            this.OrderCardList(this.localNonLandsInPlay, this.LocalPermLine);
            this.OrderCardList(this.remoteLandsInPlay, this.RemoteLandLine);
            this.OrderCardList(this.remoteNonLandsInPlay, this.RemotePermLine);
        }

        private void OrderCardList(List<CardInPlay> cards, int atY)
        {
            for (int i = 0; i < cards.Count; ++i)
            {
                CardInPlay card = cards[i];

                card.display.TargetLocation.X.targetValue = (i * card.display.BaseDimWidth()) + 10 * i + this.BorderLeft;
                card.display.TargetLocation.Y.targetValue = atY;
                card.display.texture.rotation.targetValue = 0.0f;

                if (card.Tapped)
                {
                    this.TapCard(card.display);
                }
            }
        }

        private void TapCard(CardDisplay display)
        {
            display.texture.rotation.targetValue = 3.14f / 2.0f;
            display.TargetLocation.X.targetValue += display.BaseDimHeight();
            display.TargetLocation.Y.targetValue += display.BaseDimWidth() / 2;
        }
        
        public void UntapLocal()
        {
            foreach (CardInPlay card in this.localLandsInPlay)
            {
                card.Tapped = false;
                card.display.canFocus = true;
            }
        }

        public void UntapRemote()
        {
            foreach(CardInPlay card in this.remoteLandsInPlay)
            {
                card.Tapped = false;
                card.display.canFocus = true;
            }
        }

        public void ProcessTriggerType(TriggeredAbility.TriggerType type, CardInPlay card = null)
        {
            switch(type)
            {
                case TriggeredAbility.TriggerType.Tapped:
                    if(card != null && card.triggeredAbilities != null)
                    {
                        foreach(TriggeredAbility ability in card.cardData.triggeredAbilties)
                        {
                            if(ability.type == TriggeredAbility.TriggerType.Tapped)
                            {

                            }
                        }
                    }
                    break;
            }
        }

        public void ActivateAbility(ActivatedAbility ability, CardInPlay card)
        {
            if (ability != null)
            {
                // Activate abilities
                foreach (Super.Action action in ability.actions)
                {
                    this.ProcessAction(action, card.owner);
                }

                // Pay costs
                switch (ability.type)
                {
                    case ActivatedAbility.AbilityType.PayTap:
                        Game1.Global.player.manaPool.RemoveManaFromPool(ability.cost);
                        card.Tapped = true;
                        card.display.canFocus = false;
                        break;
                    case ActivatedAbility.AbilityType.Tap:
                        card.Tapped = true;
                        card.display.canFocus = false;
                        break;
                }
            }
        }

        private void ProcessAction(Super.Action action, Player owner)
        {
            switch(action.type)
            {
                case Super.Action.ActionType.AddMana:
                    owner.manaPool.AddToPool(action.cost);
                    break;
            }
        }
    }
}
