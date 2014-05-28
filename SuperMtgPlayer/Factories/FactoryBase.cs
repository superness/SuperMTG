using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Factories
{
    public class FactoryBase<T> where T : new()
    {
        private static FactoryBase<T> g = new FactoryBase<T>();

        public List<T> data = new List<T>();

        public T Create()
        {
            T newObj = new T();
            this.data.Add(newObj);
            return newObj;
        }

        public void Update(GameTime gt)
        {
            foreach(T val in data)
            {
                typeof(T).GetMethod("Update").Invoke(val, new object[] {gt});
            }
        }

        public static FactoryBase<T> Global
        {
            get { return g; }
        }

        public void Unload(T val)
        {
            this.data.Remove(val);
        }
    }
}
