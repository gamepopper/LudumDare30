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
        int selection = 0;
        float soundVol = 1;

        Button testButton;

        public MenuState(Game1 game, Rectangle viewport)
            : base(game, viewport)
        {
            game.IsMouseVisible = true;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void LoadContent(ContentManager Content)
        {
            testButton = new Button(Content.Load<Texture2D>("testButton"), Content.Load<SpriteFont>("Menu"), new Vector2(Viewport.Width / 2, Viewport.Height / 2), "PLAY");
            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            
            //To switch states, you change the value of CurrentState from Game. 
            //Game1 was passed in as a reference so the change should take effect before the next update call.
            if (testButton.State == "Pressed" && mouse.LeftButton == ButtonState.Released)
            {
                game.CurrentState = new GameplayState(game, Viewport);
            }

            if (testButton.BoundingBox.Intersects(new Rectangle(mouse.X, mouse.Y, 1, 1)))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    testButton.SetStatePressed();
                }
                else
                {
                    testButton.SetStateOver();
                }
            }
            else
            {
                testButton.SetStateNone();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Red);

            spriteBatch.Begin();
            testButton.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }
    }
}
