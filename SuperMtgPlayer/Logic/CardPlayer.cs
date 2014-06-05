using Super;
using SuperMtgPlayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class CardPlayer : Singleton<CardPlayer>
    {
        public enum PlaySpeed
        {
            Sorcery,
            Instant
        }

        public PlaySpeed CurrentPlaySpeed = PlaySpeed.Sorcery;

        public void TryPlayCard(Player p, PlayableCard c)
        {
            if (this.CanPlayCard(p, c))
            {
                Battlefield.Global.PutCardIntoPlayForPlayer(c, p);
                p.hand.PlayCard(c);

                if(c.card.types.Contains("Land"))
                {
                    CurrentTurn.Global.CurrentLandsPlayed++;
                }
                else
                {
                    p.manaPool.RemoveManaFromPool(c.card.manaCost);
                }
            }
        }

        public bool CanPlayCard(Player p, PlayableCard c)
        {
            bool canPlay = false;

            // If this is the local player and he has priorty or the remote player and he has priorty
            if (PriorityLayer.Global.CurrentActivePlayer == p.Type)
            {
                canPlay = true;
            }

            if (canPlay == false)
                return false;

            if (this.GetPlaySpeedOfCard(c.card) == PlaySpeed.Sorcery && this.CurrentPlaySpeed == PlaySpeed.Instant)
                return false;

            if(this.GetPlaySpeedOfCard(c.card) == PlaySpeed.Sorcery && CurrentTurn.Global.CurrentActivePlayer != p.Type)
                return false;
            
            if (c.card.types.Contains("Land"))
            {
                if (CurrentTurn.Global.CurrentLandsPlayed >= CurrentTurn.Global.LandsPerTurn)
                {
                    canPlay = false;
                }
            }
            else if (p.manaPool.HaveManaForSpell(c.card.manaCost) == false)
            {
                canPlay = false;
            }

            return canPlay;
        }

        private PlaySpeed GetPlaySpeedOfCard(OutputCard card)
        {
            if (card.types.Contains("Instant") || (card.staticAbilities != null && card.staticAbilities.FirstOrDefault((x) => x.effect == StaticAbility.AbilityEffect.Flash) != null))
            {
                return PlaySpeed.Instant;
            }
            return PlaySpeed.Sorcery;
        }
    }
}
