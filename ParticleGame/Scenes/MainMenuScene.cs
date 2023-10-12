using Microsoft.Xna.Framework;
using ParticleGame.Managers;
using ParticleGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleGame.Scenes
{
    public class MainMenuScene : IScene
    {
        public SceneManager SceneManager { get; set; }

        private List<MenuEntry> _menuEntries = new List<MenuEntry>();
        private int _selectedIndex = 0;

        public void LoadContent()
        {
            _menuEntries.Add(new MenuEntry("Start Game"));
            _menuEntries.Add(new MenuEntry("Options"));
            _menuEntries.Add(new MenuEntry("Exit"));
        }

        public void UnloadContent()
        {

        }

        private void UpdateMenuEntryLocations()
        {
            var position = new Vector2(0f, 175f);

            foreach (var entry in _menuEntries)
            {
                position.X = SceneManager.GraphicsDevice.Viewport.Width / 2 - entry.GetWidth(this) / 2;

                // set the entry's position
                entry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += entry.GetHeight(this);
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            UpdateMenuEntryLocations();

            var graphics = SceneManager.GraphicsDevice;
            var spriteBatch = SceneManager.SpriteBatch;
            var font = SceneManager.Font;

            spriteBatch.Begin();

            for (int i = 0; i < _menuEntries.Count; i++)
            {
                var menuEntry = _menuEntries[i];
                menuEntry.Draw(this, gameTime);
            }

            spriteBatch.End();
        }
    }
}
