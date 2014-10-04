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

        KeyboardState prevKeyboard;

        List<Button> ButtonList = new List<Button>();
        Slider slider;

        SpriteFont menuFont;

        public MenuState(Game1 game, Rectangle viewport)
            : base(game, viewport)
        {
            game.IsMouseVisible = true;
            prevKeyboard = Keyboard.GetState();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void LoadContent(ContentManager Content)
        {
            menuFont = Content.Load<SpriteFont>("Menu");
            
            ButtonList.Add(new Button(Content.Load<Texture2D>("testButton"), menuFont, new Vector2(Viewport.Width / 2, Viewport.Height / 2), "PLAY"));
            ButtonList.Add(new Button(Content.Load<Texture2D>("testButton"), menuFont, new Vector2(Viewport.Width / 2, Viewport.Height / 2 + 80), "EXIT"));
            slider = new Slider(Content.Load<Texture2D>("sliderGraph"), Content.Load<Texture2D>("sliderBar"), new Vector2(Viewport.Width / 2, Viewport.Height / 2 + 160));
            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();
            
            //To switch states, you change the value of CurrentState from Game. 
            //Game1 was passed in as a reference so the change should take effect before the next update call.
            if (prevKeyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyDown(Keys.Up))
            {
                selection--;
            }
            if (prevKeyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyDown(Keys.Down))
            {
                selection++;
            }

            if (selection > 1)
                selection -= 2;
            else if (selection < 0)
                selection += 2;

            for (int i = 0; i < ButtonList.Count; i++)
            {
                if (ButtonList[i].BoundingBox.Intersects(new Rectangle(mouse.X, mouse.Y, 1, 1)))
                {
                    selection = i;

                    if (ButtonList[i].State == "Over" && mouse.LeftButton == ButtonState.Pressed)
                    {
                        ButtonList[i].SetStatePressed();
                    }
                    else if (ButtonList[i].State == "Pressed" && mouse.LeftButton == ButtonState.Released)
                    {
                        if (selection == 0)
                        {
                            game.CurrentState = new GameplayState(game, Viewport);
                        }
                        else
                        {
                            game.Exit();
                        }
                    }
                }

                if (selection == i)
                {
                    if (ButtonList[i].State == "Pressed")
                    {
                        if (keyboard.IsKeyUp(Keys.Enter))
                        {
                            if (selection == 0)
                            {
                                game.CurrentState = new GameplayState(game, Viewport);
                            }
                            else
                            {
                                game.Exit();
                            }
                        }
                    }
                    else if (ButtonList[i].State == "Over")
                    {
                        if (keyboard.IsKeyDown(Keys.Enter))
                        {
                            ButtonList[i].SetStatePressed();
                        }
                    }
                    else
                    {
                        ButtonList[i].SetStateOver();
                    }
                }
                else
                {
                    ButtonList[i].SetStateNone();
                }
            }

            if (slider.SliderBox.Intersects(new Rectangle(mouse.X, mouse.Y, 1, 1)) && 
                mouse.LeftButton == ButtonState.Pressed)
            {
                slider.BarX = mouse.X;
            }

            soundVol = slider.BarLevel;

            prevKeyboard = keyboard;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Red);

            spriteBatch.Begin();
            foreach (Button b in ButtonList)
            {
                b.Draw(spriteBatch);
            }
            slider.Draw(spriteBatch);

            float volWidth = menuFont.MeasureString("Volume: " + soundVol).X;
            spriteBatch.DrawString(menuFont, "Volume: " + soundVol, new Vector2(Viewport.Width / 2 - volWidth/2, Viewport.Height / 2 + 240), Color.White);
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }
    }
}
