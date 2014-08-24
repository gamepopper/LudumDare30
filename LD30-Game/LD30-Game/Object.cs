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
    public class Object
    {
        protected Texture2D td_tx;
        protected Rectangle td_rec;
        protected int td_frames = 1;
        protected int td_framespeed = 0;
        protected int td_frame_counter = 0;
        protected int td_frame = 0;
        //----------------------------------------------------------
        protected Texture2D ss_tx;
        protected Rectangle ss_rec;
        protected int ss_frames = 1;
        protected int ss_framespeed = 0;
        protected int ss_frame_counter = 0;
        protected int ss_frame = 0;
        //----------------------------------------------------------
        public Rectangle ColRec
        {
            get
            {
                Rectangle pos = Rectangle.Empty;
                if (data.CurrentState == State.TD)
                {
                    pos = td_rec;
                }
                if (data.CurrentState == State.SS)
                {
                    pos = ss_rec;
                }
                return pos;
            }
        }
        public DrawObject DrawObject
        {
            get
            {
                DrawObject d = new DrawObject();
                if (data.CurrentState == State.TD)
                {
                    td_frame_counter++;
                    if (td_frame_counter == td_framespeed)
                    {
                        td_frame++;
                        if (td_frame > td_frames) td_frame = 0;
                        td_frame_counter = 0;
                    }
                    d = new DrawObject(td_tx, td_rec, new Rectangle(td_frame * td_rec.Width, 0, td_rec.Width, td_rec.Height), Color.White);
                }
                if (data.CurrentState == State.SS)
                {
                    ss_frame_counter++;
                    if (ss_frame_counter == ss_framespeed)
                    {
                        ss_frame++;
                        if (ss_frame > ss_frames) ss_frame = 0;
                        ss_frame_counter = 0;
                    }
                    d = new DrawObject(ss_tx, ss_rec, new Rectangle(ss_frame * ss_rec.Width, 0, ss_rec.Width, ss_rec.Height), Color.White);
                }
                return d;
            }
        }
        public class testBG : Object
        {
            public testBG(Point td_pos, Point ss_pos)
            {
                td_rec = new Rectangle(td_pos.X, td_pos.Y, 2000, 2000);
                td_tx = data.Content.Load<Texture2D>(@"test/test floor");

                ss_rec = new Rectangle(ss_pos.X, ss_pos.Y, 2000, 100);
                ss_tx = data.Content.Load<Texture2D>(@"test/test floor");
            }
            
        }
        public class testPlatformShort : Object
        {
            public testPlatformShort(Point td_pos)
            {
                td_tx = data.Content.Load<Texture2D>(@"test/platf_s");
                ss_tx = data.Content.Load<Texture2D>(@"test/platf_s");

                td_rec = new Rectangle(td_pos.X, td_pos.Y, td_tx.Width, td_tx.Height);
                ss_rec = new Rectangle(data.Size.X - td_rec.Bottom, data.Size.Y - ss_tx.Height, ss_tx.Width, ss_tx.Height);
            }
        }
        public class testPlatformLong : Object
        {
            public testPlatformLong(Point td_pos)
            {
                td_tx = data.Content.Load<Texture2D>(@"test/platf_s");
                ss_tx = data.Content.Load<Texture2D>(@"test/plat_l");

                td_rec = new Rectangle(td_pos.X, td_pos.Y, td_tx.Width, td_tx.Height);
                ss_rec = new Rectangle(data.Size.X - td_rec.Bottom, data.Size.Y - ss_tx.Height, ss_tx.Width, ss_tx.Height);
            }
        }

        public class testGround : Object
        {
            public testGround(Point pos)
            {
                td_tx = data.Content.Load<Texture2D>(@"test/ground_td");
                ss_tx = data.Content.Load<Texture2D>(@"test/ground_ss");

                td_rec = new Rectangle(pos.X, pos.Y, td_tx.Width, td_tx.Height);
                ss_rec = new Rectangle(data.Size.X - td_rec.Bottom, data.Size.Y - ss_tx.Height, ss_tx.Width, ss_tx.Height);
            }
        }
        
    }
    public class State
    {
        public string thing = "";
        public State(String state)
        {
            thing = state;
        }
        public static string TD
        {
            get
            {
                return "TD";
            }
        }
        public static string SS
        {
            get { return "SS"; }
        }
    }

    
}
