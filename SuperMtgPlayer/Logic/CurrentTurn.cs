using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMtgPlayer;

namespace SuperMtgPlayer.Logic
{
    public class CurrentTurn : Singleton<CurrentTurn>
    {
        public Common.PlayerType CurrentActivePlayer = Common.PlayerType.Local;

        public int LandsPerTurn = 1;
        public int CurrentLandsPlayed = 0;

        public void NextTurn()
        {
            this.CurrentActivePlayer = (Common.PlayerType)((int)(this.CurrentActivePlayer + 1) % (int)(Common.PlayerType.Count));
            PriorityLayer.Global.CurrentActivePlayer = this.CurrentActivePlayer;

            this.LandsPerTurn = 1;
            this.CurrentLandsPlayed = 0;
        }
    }
}
