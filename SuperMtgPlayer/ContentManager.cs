using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super
{
    public class SuperContentManager
    {
        private Dictionary<string, Texture2D> textures = new Dictionary<string,Texture2D>();
        private GraphicsDevice graphics;
        private ContentManager content;

        private static SuperContentManager g = new SuperContentManager();

        public static SuperContentManager Global
        {
            get { return SuperContentManager.g; }
        }

        public void Initialize(GraphicsDevice _graphics, ContentManager _content)
        {
            this.graphics = _graphics;
            this.content = _content;
        }

        public Texture2D LoadTexture(string name)
        {
            if (!this.textures.ContainsKey(name))
            {
                this.textures[name] = this.content.Load<Texture2D>(name);
            }

            return this.textures[name];
        }
    }
}
