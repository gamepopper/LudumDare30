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
    public class testRoom : Room
    {
        public testRoom ()
        {
            Player = new Player(new Point(1000, 1000), 100);
            bg = new Object.testGround(Point.Zero);
            ObjList.Add(new Object.testPlatformLong(new Point(200, 100)));
            ObjList.Add(new Object.testPlatformShort(new Point(400, 280)));
        }
        public void Update()
        {
            List<Rectangle> l = new List<Rectangle>();
            ObjList.ForEach(delegate(Object o)
            {
                l.Add(o.ColRec);
            });
            if (data.CurrentState == State.SS) l.Add(bg.ColRec);

            Player.Update(l);

            Draw();
        }
        void Draw()
        {
            Drawhandler.Background(bg.DrawObject);
            ObjList.ForEach(delegate(Object o)
            {
                Drawhandler.MainGround(o.DrawObject);
            });
            Drawhandler.UpdateCamera(Player.rec);
        }
    }
}
