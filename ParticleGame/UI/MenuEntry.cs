using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using ParticleGame.Scenes;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleGame.UI
{
    public class MenuEntry
    {
        public string Text { get; set; }
        
        public Vector2 Position { get; set; }

        public bool IsSelected { get; set; }

        public MenuEntry(string Text) 
        {
            this.Text = Text;
        }

        public virtual void Draw(MainMenuScene screen, GameTime gameTime)
        {
            // Draw text, centered on the middle of each line.
            var screenManager = screen.SceneManager;
            var spriteBatch = screenManager.SpriteBatch;
            var font = screenManager.Font;

            var origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, Text, Position, Color.White, 0,
                origin, 1.0f, SpriteEffects.None, 0);
        }

        public int GetHeight(MainMenuScene screen)
        {
            return (int)screen.SceneManager.Font.MeasureString(Text).Y;
        }

        public int GetWidth(MainMenuScene screen)
        {
            return (int)screen.SceneManager.Font.MeasureString(Text).X;
        }
    }
}
