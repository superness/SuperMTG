using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Data
{
    public class BlendableFloat
    {
        public float currentValue;
        public float targetValue;

        public float BlendSpeed = 1.0f;

        public void Update(GameTime gt)
        {
            this.currentValue += (this.targetValue - this.currentValue) * BlendSpeed * gt.ElapsedGameTime.Milliseconds / 1000.0f;
        }
    }
}
