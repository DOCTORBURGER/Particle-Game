using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleGame.Managers;
using ParticleGame.State_Management;
using ParticleGame.UI;
using System.Collections.Generic;

namespace ParticleGame.Scenes
{
    public abstract class MenuScene : IScene
    {
        public SceneManager SceneManager { get; set; }

        protected List<MenuEntry> _menuEntries = new List<MenuEntry>();
        private int _selectedIndex = 0;

        private readonly InputAction _menuUp;
        private readonly InputAction _menuDown;
        private readonly InputAction _menuSelect;
        private readonly InputAction _menuCancel;

        private string _menuTitle;

        protected MenuScene(string menuTitle)
        {
            _menuTitle = menuTitle;

            _menuUp = new InputAction(new[] { Keys.Up }, true);
            _menuDown = new InputAction(new[] { Keys.Down }, true);
            _menuSelect = new InputAction(new[] { Keys.Enter, Keys.Space }, true);
            _menuCancel = new InputAction(new[] { Keys.Back }, true);
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        private void UpdateMenuEntryLocations()
        {
            var position = new Vector2(0f, 175f);

            foreach (var entry in _menuEntries)
            {
                position.X = SceneManager.VirtualResolution.X / 2 - entry.GetWidth(this) / 2;

                // set the entry's position
                entry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += entry.GetHeight(this);
            }
        }

        public void HandleInput(GameTime gameTime, InputState input)
        {
            if(_menuUp.Occurred(input))
            {
                _selectedIndex--;

                if (_selectedIndex < 0)
                    _selectedIndex = _menuEntries.Count - 1;
            }

            if (_menuDown.Occurred(input))
            {
                _selectedIndex++;

                if (_selectedIndex >= _menuEntries.Count)
                    _selectedIndex = 0;
            }

            if (_menuSelect.Occurred(input))
                OnSelectEntry(_selectedIndex);
            else if (_menuCancel.Occurred(input))
                OnCancel();
        }

        protected virtual void OnSelectEntry(int index)
        {
            _menuEntries[index].OnSelectEntry();
        }

        protected void OnCancel()
        {
            
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            UpdateMenuEntryLocations();

            var spriteBatch = SceneManager.SpriteBatch;
            var font = SceneManager.Font;

            spriteBatch.Begin(transformMatrix: SceneManager.ScaleMatrix, samplerState: SamplerState.PointClamp);

            for (int i = 0; i < _menuEntries.Count; i++)
            {
                var menuEntry = _menuEntries[i];
                bool isSelected = i == _selectedIndex;
                menuEntry.Draw(this, isSelected, gameTime);
            }

            var titlePosition = new Vector2(SceneManager.VirtualResolution.X / 2, 80);
            var titleOrigin = font.MeasureString(_menuTitle) / 2;
            var titleColor = new Color(192, 192, 192);
            const float titleScale = 2f;

            spriteBatch.DrawString(font, _menuTitle, titlePosition, titleColor,
                0, titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
