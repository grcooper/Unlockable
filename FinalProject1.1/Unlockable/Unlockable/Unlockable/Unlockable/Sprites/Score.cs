using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unlockable
{
    public class Score
    {
        public int heroScore { get; set; }

        private Vector2 scorePosition = new Vector2(20f, 20f);

        private SpriteFont _font;

        Hero hero = new Hero();

        public Score(SpriteFont font)
        {
            _font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Score: " + heroScore, scorePosition, Color.Black);
        }
    }
}
