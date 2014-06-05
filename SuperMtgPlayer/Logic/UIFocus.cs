using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class UIFocus : Singleton<UIFocus>
    {
        public List<object> FocusObjStack = new List<object>();

        public object FocusObj
        {
            get
            {
                return this.FocusObjStack[this.FocusObjStack.Count - 1];
            }
        }

        public void TakeFocus(object obj)
        {
            this.FocusObjStack.Add(obj);
        }

        public void PopFocus(object obj)
        {
            if(this.FocusObj == obj)
            {
                this.FocusObjStack.Remove(obj);
            }
            else
            {
                throw new Exception("Bad focus pop");
            }
        }
    }
}
