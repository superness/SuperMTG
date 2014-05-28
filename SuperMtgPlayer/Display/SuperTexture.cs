using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Display
{
    public class SuperTexture
    {
        public Texture2D texture;
        public Rectangle drawRect;
        public float zOrder = 0.0f;
        public bool visible = true;

        public void Load(string path)
        {
            this.texture = SuperContentManager.Global.LoadTexture(path);
            this.drawRect = new Rectangle();
        }

        public void Unload()
        {
            DisplayFactory.Global.Unload(this);
        }
    }
}
