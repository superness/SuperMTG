using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperMtgPlayer.Data;
using SuperMtgPlayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Display
{
    public class CardDisplay
    {
        private Vector4 dimensions = new Vector4(0, 0, 184, 256);
        private Vector4 dimensionsTarget = new Vector4(0, 0, 184, 256);
        private Rectangle displayRect = new Rectangle();

        public int Growth = 25;
        public const float BlendSpeed = 15.0f;

        public static Rectangle baseDimensions = new Rectangle(0, 0, 184, 256);
        public bool isHighlighted = false;
        public BlendableFloat scale = BlendableFloatFactory.Global.Create();
        public Point Location = new Point();
        public BlendablePoint TargetLocation = new BlendablePoint();
        public SuperTexture texture;

        public float initScale = 0.0f;

        public Rectangle DisplayRect
        {
            get { return displayRect; }
        }

        public CardDisplay()
        {
            this.scale.currentValue = 1.0f;
            this.scale.targetValue = 1.0f;
            this.scale.BlendSpeed = 15.0f;
            this.initScale = 1.0f;
        }

        public float BaseDimWidth()
        {
            return CardDisplay.baseDimensions.Width * this.initScale;
        }
        public float BaseDimHeight()
        {
            return CardDisplay.baseDimensions.Height * this.initScale;
        }

        public void Init(SuperTexture _texture)
        {
            this.texture = _texture;
        }

        public void MoveNow()
        {

        }

        public void Update(GameTime gt)
        {
            // Update logic
            MouseState mouse = Mouse.GetState();
            if ((mouse.Position.X <= this.texture.drawRect.X + this.texture.drawRect.Width) &&
               (mouse.Position.X >= this.texture.drawRect.X) &&
               (mouse.Position.Y <= this.texture.drawRect.Y + this.texture.drawRect.Height) &&
               (mouse.Position.Y >= this.texture.drawRect.Y))
            {
                this.isHighlighted = true;
            }
            else
            {
                this.isHighlighted = false;
            }

            if(this.isHighlighted)
            {
                int growthWidth = (int)(baseDimensions.Width * this.Growth / 100.0f);
                int growthHeight = (int)(baseDimensions.Height * this.Growth / 100.0f);

                this.dimensionsTarget.X = -growthWidth / 2 * this.scale.currentValue;
                this.dimensionsTarget.Y = -growthHeight * this.scale.currentValue;
                this.dimensionsTarget.W = baseDimensions.Width + growthWidth;
                this.dimensionsTarget.Z = baseDimensions.Height + growthHeight;

                this.texture.zOrder = 0.001f;
                //this.scale.targetValue = 1.0f;
            }
            else
            {
                this.dimensionsTarget.X = 0;
                this.dimensionsTarget.Y = 0;
                this.dimensionsTarget.W = baseDimensions.Width;
                this.dimensionsTarget.Z = baseDimensions.Height;

                this.texture.zOrder = 1.0f;
                //this.scale.targetValue = this.scale.initValue;
            }

            // Blend to target size
            this.dimensions.X += ((this.dimensionsTarget.X - this.dimensions.X) * CardDisplay.BlendSpeed) * gt.ElapsedGameTime.Milliseconds / 1000.0f;
            this.dimensions.Y += ((this.dimensionsTarget.Y - this.dimensions.Y) * CardDisplay.BlendSpeed) * gt.ElapsedGameTime.Milliseconds / 1000.0f;
            this.dimensions.W += ((this.dimensionsTarget.W - this.dimensions.W) * CardDisplay.BlendSpeed) * gt.ElapsedGameTime.Milliseconds / 1000.0f;
            this.dimensions.Z += ((this.dimensionsTarget.Z - this.dimensions.Z) * CardDisplay.BlendSpeed) * gt.ElapsedGameTime.Milliseconds / 1000.0f; 

            // Create display rect
            displayRect.Width = (int)(dimensions.W * this.scale.currentValue);
            displayRect.Height = (int)(dimensions.Z * this.scale.currentValue);

            this.Location.X = (int)this.TargetLocation.X.currentValue;
            this.Location.Y = (int)this.TargetLocation.Y.currentValue;

            displayRect.Location = new Point(this.Location.X + (int)(this.dimensions.X), this.Location.Y + (int)(this.dimensions.Y));

            this.texture.drawRect = this.DisplayRect;
        }

        public void Unload()
        {
            this.texture.Unload();
            CardDisplayFactory.Global.Unload(this);
        }

        //private Rectangle RotatedDrawRect()
        //{

        //}
    }
}
