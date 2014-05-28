#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Super;
using Newtonsoft.Json;
using System.IO;
using SuperMtgPlayer.Logic;
using SuperMtgPlayer.Data;
using SuperMtgPlayer.Display;
using SuperMtgPlayer.Factories;
#endregion

namespace SuperMtgPlayer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CardDataWrapper cardData;
        Player player = new Player();
        SpriteFont font;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.ApplyChanges();

            this.IsMouseVisible = true;

            Globals.Global.localPlayer = player;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Super.SuperContentManager.Global.Initialize(GraphicsDevice, Content);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.font = Content.Load<SpriteFont>("defaultFont");

            // Load json data
            string jsonfile = Path.Combine(Content.RootDirectory, "carddata.json");
            using (StreamReader r = new StreamReader(jsonfile))
            {
                string json = r.ReadToEnd();
                this.cardData = new CardDataWrapper(JsonConvert.DeserializeObject<OutputData>(json));
            }

            this.player.AddCardsToDeck(this.cardData.GetCards(new string[] {
                                                                                "Mana Confluence",
                                                                                "Mana Confluence",
                                                                                "Mana Confluence",
                                                                                "Mana Confluence",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Plains",
                                                                                "Banishing Light",
                                                                                "Banishing Light",
                                                                                "Banishing Light",
                                                                                "Banishing Light",
                                                                                "Oppressive Rays",
                                                                                "Oppressive Rays",
                                                                                "Oppressive Rays",
                                                                                "Oppressive Rays",
                                                                                "Ajani's Presence",
                                                                                "Ajani's Presence",
                                                                                "Ajani's Presence",
                                                                                "Dawnbringer Charioteers",
                                                                                "Dawnbringer Charioteers",
                                                                                "Dawnbringer Charioteers",
                                                                                "Godsend",
                                                                                "Godsend",
                                                                                "Aegis of the Gods",
                                                                                "Aegis of the Gods",
                                                                                "Aegis of the Gods",
                                                                                "Launch the Fleet",
                                                                                "Launch the Fleet",
                                                                                "Launch the Fleet",
                                                                                "Launch the Fleet",
                                                                                "Oreskos Swiftclaw",
                                                                                "Oreskos Swiftclaw",
                                                                                "Oreskos Swiftclaw",
                                                                                "Oreskos Swiftclaw",
                                                                                "Nyx-Fleece Ram",
                                                                                "Nyx-Fleece Ram",
                                                                                "Nyx-Fleece Ram",
                                                                                "Nyx-Fleece Ram",
                                                                                "Font of Vigor",
                                                                                "Font of Vigor",
                                                                                "Skybind",
                                                                                "Skybind",
                                                                                "Quarry Colossus",
                                                                                "Quarry Colossus"
                                                                            }), true);

            this.player.GameStart();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SuperMouse.Global.Update();
            SuperKeyboard.Global.Update();
            Battlefield.Global.Update(gameTime);
            BlendableFloatFactory.Global.Update(gameTime);
            CardDisplayFactory.Global.Update(gameTime);
            this.player.Update(gameTime, graphics);

            if (SuperKeyboard.Global.KeyPress(Keys.Space))
            {
                this.player.DrawCard();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw textures
            this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            List<SuperTexture> textures = DisplayFactory.Global.data;
            foreach(SuperTexture tex in textures)
            {
                if(tex.visible)
                {
                    this.spriteBatch.Draw(tex.texture, tex.drawRect, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, tex.zOrder);
                    //this.spriteBatch.DrawString(this.font, string.Format("{0}", tex.zOrder), new Vector2(tex.drawRect.X, tex.drawRect.Y), Color.Green);
                }
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
