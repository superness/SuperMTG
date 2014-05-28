using SuperMtgPlayer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer
{
    public class Globals : Singleton<Globals>
    {
        public Player localPlayer;
        public Player remotePlayer;
    }
}
