using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD30_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public GameState CurrentState;

        testRoom testRoom;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1200;
            data.CurrentState = "TD";
            data.Content = Content;
            data.Size = new Point(2000, 2000);
        }
        List<Object> o = new List<Object>();

        protected override void Initialize()
        {
            CurrentState = new MenuState(this, new Rectangle(0,0,this.graphics.PreferredBackBufferWidth,this.graphics.PreferredBackBufferHeight));

            testRoom = new testRoom();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }
        bool Switch = true;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R) && Switch)
            {
                string newS = data.CurrentState;
                if (data.CurrentState == State.TD) newS = State.SS;
                if (data.CurrentState == State.SS) newS = State.TD;
                data.CurrentState = newS;
                Switch = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.R)) Switch = true;
            testRoom.Update();
            
            CurrentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Drawhandler.Update(spriteBatch);
            CurrentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    
    }
}
