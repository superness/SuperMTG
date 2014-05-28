using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer
{
    public class SuperKeyboard : Singleton<SuperKeyboard>
    {
        KeyboardState previousState;
        KeyboardState currentState;

        public void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public bool KeyPress(Keys k)
        {
            return Contains(currentState.GetPressedKeys(), k) && !Contains(previousState.GetPressedKeys(), k);
        }

        public bool Contains<T>(T[] array, T val)
        {
            return Array.IndexOf(array, val) != -1;
        }
    }
}
