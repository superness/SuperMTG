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
        public void TryPlayCard(Player p, PlayableCard c)
        {
            Battlefield.Global.PutCardIntoPlayForPlayer(c, p);
            p.hand.PlayCard(c);
        }
    }
}
