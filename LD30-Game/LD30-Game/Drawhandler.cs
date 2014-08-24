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
    public class Drawhandler
    {
        static List<DrawObject> bg = new List<DrawObject>();
        public static void Background(DrawObject DO)
        {
            bg.Add(DO);
        }
        static List<DrawObject> mg = new List<DrawObject>();
        public static void MainGround(DrawObject mgo)
        {
            mg.Add(mgo);
        }
        static List<DrawObject> fg = new List<DrawObject>();
        public static void Foreground(DrawObject foreground)
        {
            fg.Add(foreground);
        }
        public static void Update(SpriteBatch spriteBatch)
        {
            Matrix tMatrix = Matrix.CreateTranslation(Camera.X, Camera.Y, 0);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, tMatrix);

            bg.ForEach(delegate(DrawObject o)
            {
                spriteBatch.Draw(o.t, o.p, o.s, o.c, o.r, o.o, o.spfx, 1);
            });
            bg.Clear();

            mg.ForEach(delegate(DrawObject o)
            {
                spriteBatch.Draw(o.t, o.p, o.s, o.c, o.r, o.o, o.spfx, 1);
            });
            mg.Clear();

            fg.ForEach(delegate(DrawObject o)
            {
                spriteBatch.Draw(o.t, o.p, o.s, o.c, o.r, o.o, o.spfx, 1);
            });
            fg.Clear();
            spriteBatch.End();
        }

        static Point Camera = Point.Zero;
        static Rectangle Bounds = new Rectangle(400, 300, 400, 400);
        public static void UpdateCamera(Rectangle Player)
        {
            
            if (Player.X < Bounds.X - Camera.X) Camera.X = -Player.X + Bounds.X;
            if (Player.Right > Bounds.Right - Camera.X) Camera.X = -Player.Right + Bounds.Right;
            if (Player.Y < Bounds.Y - Camera.Y) Camera.Y = -Player.Y + Bounds.Y;
            if (Player.Bottom > Bounds.Bottom - Camera.Y) Camera.Y = -Player.Bottom + Bounds.Bottom;


        }
        
    }
    
    public class DrawObject
    {
        public Texture2D t;
        public Rectangle p;
        public Rectangle s;
        public Color c;
        public float r;
        public Vector2 o;
        public SpriteEffects spfx;

        public DrawObject()
        {

        }
        public DrawObject(Texture2D Texture, Rectangle Pos, Color Color)
        {
            t = Texture;
            p = Pos;
            s = new Rectangle(0, 0, t.Width, t.Height);
            c = Color;
            r = 0;
            o = Vector2.Zero;
            spfx = SpriteEffects.None;
        }
        public DrawObject(Texture2D Texture, Rectangle Pos, Rectangle Source, Color Color)
        {
            t = Texture;
            p = Pos;
            s = Source;
            c = Color;
            r = 0;
            o = Vector2.Zero;
            spfx = SpriteEffects.None;
        }
        public DrawObject(Texture2D Texture, Rectangle Pos, Rectangle Source, Color Color, float Rotation, Vector2 Origin, SpriteEffects SpriteEffects)
        {
            t = Texture;
            p = Pos;
            s = Source;
            c = Color;
            r = Rotation;
            o = Origin;
            spfx = SpriteEffects;
        }
    }
}
