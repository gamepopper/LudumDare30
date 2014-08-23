using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD30_Game
{
    public class GameState
    {
        //Game1 used to access the CurrentState variable for changing states.
        protected Game1 game;
        protected Rectangle Viewport;

        //Constructor calls both initialize and load content so data can be loaded and initialized without calling Game1 functions.
        public GameState(Game1 game, Rectangle viewport)
        {
            this.game = game;
            Viewport = viewport;
            Initialize();
            LoadContent(game.Content);
        }

        //All your functions originally found in Game1 are here! (Except UnloadContent, which could be added if needs be)
        public virtual void Initialize() { }
        public virtual void LoadContent(ContentManager Content) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { } //GameTime and Spritebatch are passed here incase the gameTime is needed in the draw function.
    }
}
