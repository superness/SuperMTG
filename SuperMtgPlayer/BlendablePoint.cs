using SuperMtgPlayer.Data;
using SuperMtgPlayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer
{
    public class BlendablePoint
    {
        public BlendableFloat X = BlendableFloatFactory.Global.Create();
        public BlendableFloat Y = BlendableFloatFactory.Global.Create();
    }
}
