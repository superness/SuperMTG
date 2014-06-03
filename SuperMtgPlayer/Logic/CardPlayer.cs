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
                }
            }


            if (canPlay)
            {
                Battlefield.Global.PutCardIntoPlayForPlayer(c, p);
                p.hand.PlayCard(c);
            }
        }
    }
}
