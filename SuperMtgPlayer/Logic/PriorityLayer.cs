using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class PriorityLayer : Singleton<PriorityLayer>
    {
        public Common.PlayerType CurrentActivePlayer = Common.PlayerType.Local;

        public void GivePriority()
        {
            if (this.CurrentActivePlayer == Common.PlayerType.Local)
            {
                Game1.Global.player.OnPriority();
            }
            else
            {
                Game1.Global.aiPlayer.OnPriority();
            }
        }

        public void PassPriorty()
        {
            // Move priority to the next player
            this.CurrentActivePlayer = (Common.PlayerType)((int)(this.CurrentActivePlayer + 1) % (int)Common.PlayerType.Count);

            if ((this.CurrentActivePlayer == Common.PlayerType.Local && CurrentTurn.Global.CurrentActivePlayer == Common.PlayerType.Local) ||
                (this.CurrentActivePlayer == Common.PlayerType.Remote && CurrentTurn.Global.CurrentActivePlayer == Common.PlayerType.Remote))
            {
                PhaseLogic.Global.NextStep();
            }
            else
            {
                // Let them play
                this.GivePriority();
            }
        }
    }
}
