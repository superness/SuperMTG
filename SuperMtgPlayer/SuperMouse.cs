using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer
{
    public class SuperMouse : Singleton<SuperMouse>
    {
        MouseState previousState;
        MouseState currentState;

        public void Update()
        {
            previousState = currentState;
            currentState = Mouse.GetState();
        }

        public bool LeftMouseClick()
        {
            return currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released;
        }

        public bool RightMouseClick()
        {
            return currentState.RightButton == ButtonState.Pressed && previousState.RightButton == ButtonState.Released;
        }
    }
}
