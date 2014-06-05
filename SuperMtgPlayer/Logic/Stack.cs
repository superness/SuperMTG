using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class Stack : Singleton<Stack>
    {
        public List<Super.Action> TheStack = new List<Super.Action>();

        public void PushToStack(Super.Action action)
        {
            // For display purposes
            this.TheStack.Add(action);
        }
    }
}
