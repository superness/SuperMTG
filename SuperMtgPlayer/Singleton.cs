using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer
{
    public class Singleton<T> where T : new()
    {
        private static T g = new T();

        public static T Global
        {
            get { return g; }
        }
    }
}
