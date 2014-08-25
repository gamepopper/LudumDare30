using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD30_Game
{
    class Slider
    {
        Vector2 Position = Vector2.Zero;
        public int BarX
        {
            set
            {
                value = (int)MathHelper.Clamp(value, Position.X - graphTex.Width / 2, Position.X + graphTex.Width / 2);
                barX = value;
            }
            get
            {
                return barX;
            }
        }
        int barX;
        public float BarLevel
        {
            get
            {
                return Math.Abs((Position.X - graphTex.Width/2) - BarX) / graphTex.Width;
            }
        }
        Texture2D graphTex;
        Texture2D barTex;

        public Rectangle SliderBox
        {
            get
            {
                return new Rectangle(0, 
                    (int)Position.Y - graphTex.Height/2, 
                    1200, graphTex.Height);
            }
        }

        public Slider(Texture2D graphTex, Texture2D barTex, Vector2 Position)
        {
            this.graphTex = graphTex;
            this.barTex = barTex;
            this.Position = Position;
            BarX = (int)Position.X;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(graphTex, Position, new Rectangle(0, 0, graphTex.Width, graphTex.Height), Color.White, 0, new Vector2(graphTex.Width / 2, graphTex.Height / 2), 1, SpriteEffects.None, 0);
            spritebatch.Draw(barTex, new Vector2(BarX, Position.Y), new Rectangle(0, 0, barTex.Width, barTex.Height), Color.White, 0, new Vector2(barTex.Width / 2, barTex.Height / 2), 1, SpriteEffects.None, 0);
        }
    }
}
