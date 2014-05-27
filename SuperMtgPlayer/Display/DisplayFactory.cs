using Microsoft.Xna.Framework;
using Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Display
{
    public class DisplayFactory
    {
        private static DisplayFactory g = new DisplayFactory();

        public List<SuperTexture> textures = new List<SuperTexture>();

        public SuperTexture CreateTexture(string name)
        {
            SuperTexture newTexture = new SuperTexture();
            newTexture.texture = SuperContentManager.Global.LoadTexture(name);
            newTexture.drawRect = new Rectangle();
            this.textures.Add(newTexture);
            return newTexture;
        }

        public static DisplayFactory Global
        {
            get { return g; }
        }
    }
}
