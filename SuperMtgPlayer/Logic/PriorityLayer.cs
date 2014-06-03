using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class PriorityLayer : Singleton<PriorityLayer>
    {
        public enum ActivePlayer
        {
            Local,
            Remote,

            Count
        }

        public ActivePlayer CurrentActivePlayer = ActivePlayer.Local;

        public void GivePriority()
        {
            CardPlayer.Global.LocalPlayerActive = this.CurrentActivePlayer == ActivePlayer.Local;

            if(this.CurrentActivePlayer == ActivePlayer.Local)
            {
                Game1.Global.player.OnPriority();
            }
        }

        public void PassPriorty()
        {
            // Move priority to the next player
            this.CurrentActivePlayer = (ActivePlayer)((int)(this.CurrentActivePlayer + 1) % (int)ActivePlayer.Count);
            CardPlayer.Global.LocalPlayerActive = this.CurrentActivePlayer == ActivePlayer.Local;

            // Let them play
            this.GivePriority();
        }
    }
}
