using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace spaceshooter
{
    class PrintText
    {
        SpriteFont font;

        public PrintText(SpriteFont font)
        {
            this.font = font;
        }

        public void Print(string text, SpriteBatch _spriteBatch, int X, int Y)
        {
            _spriteBatch.DrawString(font, text, new Vector2(X, Y), Color.White);
        }
    }
}