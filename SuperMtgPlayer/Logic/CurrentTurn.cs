using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class CurrentTurn : Singleton<CurrentTurn>
    {
        public enum ActivePlayer
        {
            Local,
            Remote,

            Count
        }

        public ActivePlayer CurrentActivePlayer = ActivePlayer.Local;

        public int LandsPerTurn = 1;
        public int CurrentLandsPlayed = 0;

        public void NextTurn()
        {
            this.CurrentActivePlayer = (ActivePlayer)((int)(this.CurrentActivePlayer + 1) % (int)(ActivePlayer.Count));

            this.LandsPerTurn = 1;
            this.CurrentLandsPlayed = 0;
        }
    }
}
