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
    class Cuboid
    {
        public Cuboid()
        { }
        public int X = 0;
        public int Y = 0;
        public int H = 0;
        public int Width = 0;
        public int Height = 0;
        public int SideHeight = 0;
        public Cuboid(int x, int y, int h, int width, int height, int sideHeight)
        {
            X = x;
            Y = y;
            H = h;
            Width = width;
            Height = height;
            SideHeight = sideHeight;
        }
        public Rectangle TD
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }
        public Rectangle SS

        {
            get
            {
                return new Rectangle(Y, H, Height, SideHeight);
            }
        }
    }
}
