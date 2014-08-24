using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD30_Game
{
    public class Player
    {
        public Rectangle td_rec = new Rectangle(0, 0, 64, 64);
        public Rectangle ss_rec = new Rectangle(0, 0, 64, 128);




        List<Rectangle> list = new List<Rectangle>();
        public Player(Point StartPoint, int StartHeight)
        {
            td_rec.X = StartPoint.X;
            td_rec.Y = StartPoint.Y;
            ss_rec.Y = StartHeight;
        }
        string PreviousState = State.TD;
        public void Update(List<Rectangle> List)
        {
            if (data.CurrentState != PreviousState)
            {
                if (data.CurrentState == State.TD)
                {
                    td_rec.Y = data.Size.X - ss_rec.X;
                }
                if (data.CurrentState == State.SS)
                {
                    ss_rec.X = data.Size.Y - td_rec.Y;
                }
                PreviousState = data.CurrentState;
            }
            list = List;
            DetermineDirection();
            if (data.CurrentState == State.TD) Birdseye();
            if (data.CurrentState == State.SS) Sidescroll();
            //Trace.WriteLine(rec);
            Draw();
        }


        void Birdseye()
        {
            MovePlayerTopDown();
        }
        void Sidescroll()
        {
            MovePlayerSideways();
        }


        void MovePlayerTopDown()
        {
            KeyboardState ks = Keyboard.GetState();
            Point MoveAmount = Point.Zero;
            if (data.CurrentState == State.TD)
            {
                int speed = 4;
                
                if (IsWalking)
                {
                    switch (Direction)
                    {
                        case 1:
                            MoveAmount.Y = -speed;
                            if (ks.IsKeyDown(Keys.Right)) MoveAmount.X = speed;
                            if (ks.IsKeyDown(Keys.Left)) MoveAmount.X = -speed;
                            break;
                        case 2:
                            MoveAmount.X = speed;
                            if (ks.IsKeyDown(Keys.Up)) MoveAmount.Y = -speed;
                            if (ks.IsKeyDown(Keys.Down)) MoveAmount.Y = speed;
                            break;
                        case 3:
                            MoveAmount.Y = speed;
                            if (ks.IsKeyDown(Keys.Right)) MoveAmount.X = speed;
                            if (ks.IsKeyDown(Keys.Left)) MoveAmount.X = -speed;
                            break;
                        case 4:
                            MoveAmount.X = -speed;
                            if (ks.IsKeyDown(Keys.Up)) MoveAmount.Y = -speed;
                            if (ks.IsKeyDown(Keys.Down)) MoveAmount.Y = speed;
                            break;
                        default: break;
                    } 
                }
                Move(MoveAmount);
                System.Diagnostics.Trace.WriteLine(Direction);
            }
        }
        int JumpAmount = -14;
        float FallSpeed = 0;
        float Gravity = 0.4f;
        int SidescrollSpeed = 8;
        void MovePlayerSideways()
        {
            KeyboardState ks = Keyboard.GetState();
            //X-value
            Point MoveAmount = Point.Zero;
            if (ks.IsKeyDown(Keys.Right))
            {
                MoveAmount.X = SidescrollSpeed;
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                MoveAmount.X = -SidescrollSpeed;
            }
            if (IsOnGround)
            {
                FallSpeed = 0;
                if (ks.IsKeyDown(Keys.X)) FallSpeed = JumpAmount;
            }
            if (!IsOnGround)
            {
                FallSpeed += Gravity;
            }
            MoveAmount.Y = (int)FallSpeed;
            Move(MoveAmount);
        }
        int Direction = 1;
        int FirstPressed = 0;

        bool Move(Point MoveAmount)
        {
            bool ret = false;
            if (data.CurrentState == State.TD)
            {
                for (int i = 0; i < Math.Abs(MoveAmount.X); i++)
                {
                    Rectangle testRec = td_rec;
                    testRec.X += MoveAmount.X / Math.Abs(MoveAmount.X);

                    foreach (Rectangle r in list)
                    {
                        if (testRec.Intersects(r))
                        {
                            i = Math.Abs(MoveAmount.X);
                            ret = true;
                        }
                    }
                    if (i == Math.Abs(MoveAmount.X)) break;
                    td_rec.X += MoveAmount.X / Math.Abs(MoveAmount.X);
                }

                for (int i = 0; i < Math.Abs(MoveAmount.Y); i++)
                {
                    Rectangle testRec = td_rec;
                    testRec.Y += MoveAmount.Y / Math.Abs(MoveAmount.Y);
                    bool temp = false;
                    foreach (Rectangle r in list)
                    {
                        if (testRec.Intersects(r))
                        {
                            temp = true;
                            ret = true;
                        }
                    }
                    if (temp) break;
                    td_rec.Y += MoveAmount.Y / Math.Abs(MoveAmount.Y);
                } 
            }
            if (data.CurrentState == State.SS)
            {
                for (int i = 0; i < Math.Abs(MoveAmount.X); i++)
                {
                    Rectangle testRec = ss_rec;
                    testRec.X += MoveAmount.X / Math.Abs(MoveAmount.X);

                    foreach (Rectangle r in list)
                    {
                        if (testRec.Intersects(r))
                        {
                            i = Math.Abs(MoveAmount.X);
                            ret = true;
                        }
                    }
                    if (i == Math.Abs(MoveAmount.X)) break;
                    ss_rec.X += MoveAmount.X / Math.Abs(MoveAmount.X);
                }

                for (int i = 0; i < Math.Abs(MoveAmount.Y); i++)
                {
                    Rectangle testRec = ss_rec;
                    testRec.Y += MoveAmount.Y / Math.Abs(MoveAmount.Y);
                    bool temp = false;
                    foreach (Rectangle r in list)
                    {
                        if (testRec.Intersects(r))
                        {
                            temp = true;
                            ret = true;
                        }
                    }
                    if (temp) break;
                    ss_rec.Y += MoveAmount.Y / Math.Abs(MoveAmount.Y);
                } 
            
            }
            return ret;

        }
        void DetermineDirection()
        {
            KeyboardState ks = Keyboard.GetState();
            if (data.CurrentState == State.TD)
            {
                if (ks.IsKeyDown(Keys.Up) && FirstPressed == 0) { Direction = 1; FirstPressed = 1; } if (!ks.IsKeyDown(Keys.Up) && FirstPressed == 1) FirstPressed = 0;
                if (ks.IsKeyDown(Keys.Right) && FirstPressed == 0) { Direction = 2; FirstPressed = 2; } if (!ks.IsKeyDown(Keys.Right) && FirstPressed == 2) FirstPressed = 0;
                if (ks.IsKeyDown(Keys.Down) && FirstPressed == 0) { Direction = 3; FirstPressed = 3; } if (!ks.IsKeyDown(Keys.Down) && FirstPressed == 3) FirstPressed = 0;
                if (ks.IsKeyDown(Keys.Left) && FirstPressed == 0) { Direction = 4; FirstPressed = 4; } if (!ks.IsKeyDown(Keys.Left) && FirstPressed == 4) FirstPressed = 0; 
            }
            if (data.CurrentState == State.SS)
            {
                if (ks.IsKeyDown(Keys.Right) && FirstPressed == 0) { Direction = 1; FirstPressed = 1; } if (!ks.IsKeyDown(Keys.Right) && FirstPressed == 1) FirstPressed = 0;
                if (ks.IsKeyDown(Keys.Left) && FirstPressed == 0) { Direction = 1; FirstPressed = 1; } if (!ks.IsKeyDown(Keys.Right) && FirstPressed == 1) FirstPressed = 0;
            }


        }
        bool IsWalking
        {
           
            get
            {
                KeyboardState ks = Keyboard.GetState();
                bool ret = false;
                if (data.CurrentState == State.TD) { if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.Down)) ret = true; }
                if (data.CurrentState == State.SS) { if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Right)) ret = true; }
                return ret;
            }
        }
        public Rectangle rec
        {
            get
            {
                Rectangle r = new Rectangle();
                if (data.CurrentState == State.TD) r = td_rec;
                if (data.CurrentState == State.SS) r = ss_rec;
                return r;
            }
        }
        bool IsOnGround
        {
            get
            {
                bool ret = false;
                list.ForEach(delegate(Rectangle r)
                {
                    if (new Rectangle(ss_rec.X, ss_rec.Y + 1, ss_rec.Width, ss_rec.Height).Intersects(r)) ret = true;
                });
                return ret;
            }
        }
        ///§§ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ Breaker ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ §§

        void Draw()
        {
            if (data.CurrentState == State.TD)
            {
                Drawhandler.MainGround(new DrawObject(data.Content.Load<Texture2D>(@"test/test player"), td_rec, Color.White));
            }
            if (data.CurrentState == State.SS)
            {
                Drawhandler.MainGround(new DrawObject(data.Content.Load<Texture2D>(@"test/test player"), ss_rec, Color.White));
            }
        }

        
    }
}
