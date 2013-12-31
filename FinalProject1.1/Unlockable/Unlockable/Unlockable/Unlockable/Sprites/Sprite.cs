using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Unlockable
{
    public class Sprite
    {
        public Vector2 spritePosition = new Vector2(20, 410);
        private Texture2D spriteTexture;

        public string AssetName;

        public Rectangle Size;

        private float scale = 1.0f;

        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                //Rectangle the size of the sprite with the new scale
                Size = new Rectangle(0,0, (int)(spriteTexture.Width * scale), (int)(spriteTexture.Height * scale));
            }
        }

        //Load content for the sprite using the Content Pipeline
        public void LoadContent(ContentManager contentManager, string assetName)
        {
            spriteTexture = contentManager.Load<Texture2D>("hero");
            AssetName = assetName;
            Size = new Rectangle(0, 0, (int)(spriteTexture.Width * scale), (int)(spriteTexture.Height * scale));
        }

        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            spritePosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        //Create hero drawn stuff here
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spritePosition, new Rectangle(0,0, spriteTexture.Width, spriteTexture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
