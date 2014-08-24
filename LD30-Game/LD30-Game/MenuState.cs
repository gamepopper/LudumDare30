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
    class MenuState : GameState
    {
        public MenuState(Game1 game, Rectangle viewport)
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
            //To switch states, you change the value of CurrentState from Game. 
            //Game1 was passed in as a reference so the change should take effect before the next update call.
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                game.CurrentState = new GameplayState(game, Viewport);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Red);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
