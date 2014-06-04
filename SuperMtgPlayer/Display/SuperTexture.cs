using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Super;
using SuperMtgPlayer.Data;
using SuperMtgPlayer.Factories;
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
        public BlendableFloat rotation = BlendableFloatFactory.Global.Create();

        public void Load(string path)
        {
            this.texture = SuperContentManager.Global.LoadTexture(path);
            this.drawRect = new Rectangle();
            this.rotation.currentValue = 0.0f;
            this.rotation.targetValue = 0.0f;
            this.rotation.BlendSpeed = 15.0f;
        }

        public void Unload()
        {
            DisplayFactory.Global.Unload(this);
        }
    }
}
