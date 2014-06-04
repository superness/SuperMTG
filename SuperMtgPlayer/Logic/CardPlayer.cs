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
        public bool LocalPlayerActive = false;

        public void TryPlayCard(Player p, PlayableCard c)
        {
            bool canPlay = false;

            if(c.card.types.Contains("Land"))
            {
                if(CurrentTurn.Global.CurrentLandsPlayed < CurrentTurn.Global.LandsPerTurn)
                {
                    canPlay = true;
                    CurrentTurn.Global.CurrentLandsPlayed++;
                }
            }
            else if(ManaPool.Global.HaveManaForSpell(c.card.manaCost))
            {
                canPlay = true;
                ManaPool.Global.RemoveManaFromPool(c.card.manaCost);
            }

            if (canPlay)
            {
                Battlefield.Global.PutCardIntoPlayForPlayer(c, p);
                p.hand.PlayCard(c);
            }
        }
    }
}
