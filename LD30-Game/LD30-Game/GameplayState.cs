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
    //New states are written by extending GameState.
    //You should override the functions so you can use them.
    class GameplayState : GameState
    {
        public GameplayState(Game1 game, Rectangle viewport)
            : base(game, viewport)
        {

        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void LoadContent(ContentManager Content)
        {
            
            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Blue);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
