using Microsoft.Xna.Framework;
using ParticleGame.Managers;
using ParticleGame.Particle_Sytems;
using ParticleGame.State_Management;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleGame.Scenes
{
    public class GameplayScene : IScene
    {
        public SceneManager SceneManager { get; set; }

        private readonly InputAction _nextEffect;
        private readonly InputAction _previousEffect;

        private List<ParticleSystem> _particleSystems;
        private int _currentEffectIndex = 0;

        public GameplayScene()
        {
            _nextEffect = new InputAction(new[] { Keys.Left }, true);
            _previousEffect = new InputAction(new[] { Keys.Right }, true);

            _particleSystems = new List<ParticleSystem>();
        }

        public void LoadContent()
        {
            var rain = new RainParticleSystem(SceneManager.Game, new Rectangle(0, 0, SceneManager.VirtualResolution.X, 1), SceneManager);
            _particleSystems.Add(rain);

            var sparks = new MouseSparksParticleSystem(SceneManager.Game, SceneManager);
            _particleSystems.Add(sparks);

            var water = new WaterBallParticleSystem(SceneManager.Game, SceneManager);
            _particleSystems.Add(water);

            var fire = new FireBallParticleSystem(SceneManager.Game, SceneManager);
            _particleSystems.Add(fire);
             
            SceneManager.Game.Components.Add(_particleSystems[_currentEffectIndex]);
        }

        public void UnloadContent()
        {
            foreach (var system in _particleSystems)
            {
                SceneManager.Game.Components.Remove(system);
            }
        }

        public void HandleInput(GameTime gameTime, InputState input)
        {
            if(_nextEffect.Occurred(input)) { ChangeEffect(1); }
            else if (_previousEffect.Occurred(input)) { ChangeEffect(-1); }

            if (_particleSystems[_currentEffectIndex] is IEmit emitter)
            {
                emitter.Emit(input.GetAdjustedMouseLocation(SceneManager.ScaleMatrix));
            }
        }

        private void ChangeEffect(int direction)
        {
            SceneManager.Game.Components.Remove(_particleSystems[_currentEffectIndex]);

            _currentEffectIndex += direction;
            if (_currentEffectIndex < 0) _currentEffectIndex = _particleSystems.Count - 1;
            if (_currentEffectIndex >= _particleSystems.Count) _currentEffectIndex = 0;

            SceneManager.Game.Components.Add(_particleSystems[_currentEffectIndex]);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            var spriteBatch = SceneManager.SpriteBatch;
            var font = SceneManager.Font;

            var effectTitle = "< " + _particleSystems[_currentEffectIndex].Name + " >";

            var effectTitlePosition = new Vector2(SceneManager.VirtualResolution.X / 2, SceneManager.VirtualResolution.Y - 80);
            var effectTitleOrigin = font.MeasureString(effectTitle) / 2;
            var effectTitleColor = new Color(192, 192, 192);
            const float effectTitleScale = 2f;

            spriteBatch.Begin(transformMatrix: SceneManager.ScaleMatrix, samplerState: SamplerState.PointClamp);

            spriteBatch.DrawString(font, effectTitle, effectTitlePosition, effectTitleColor,
                0, effectTitleOrigin, effectTitleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
