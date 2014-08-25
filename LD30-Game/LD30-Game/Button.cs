using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD30_Game
{
    class Button
    {
        Texture2D texture;
        SpriteFont font;
        string state = "None"; //States will be "None", "Over" and "Pressed"
        string text = "";
        int width, height;
        Vector2 position;
        Vector2 origin;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(position.X - width/2), (int)(position.Y - height/2), width, height);
            }
        }
        public string State
        {
            get
            {
                return state;
            }
        }

        public Button(Texture2D texture, SpriteFont font, Vector2 position, string text)
        {
            this.texture = texture;
            this.font = font;
            width = texture.Width;
            height = texture.Height / 3;
            this.position = position;
            origin = new Vector2(width / 2, height / 2);
            this.text = text;
        }

        public void SetStateNone()
        {
            state = "None";
        }

        public void SetStateOver()
        {
            state = "Over";
        }

        public void SetStatePressed()
        {
            state = "Pressed";
        }

        public void Draw(SpriteBatch spritebatch)
        {
            int posY = 0;

            if (state == "Over")
                posY = height;
            else if (state == "Pressed")
                posY = 2 * height;
            
            spritebatch.Draw(texture, position, new Rectangle(0, posY, width, height), Color.White, 0.0f, origin, 1, SpriteEffects.None, 0.0f);
            
            Vector2 fontSize = font.MeasureString(text);
            spritebatch.DrawString(font, text, position, Color.Black, 0, fontSize / 2, 1, SpriteEffects.None, 0);
        }
    }
}
