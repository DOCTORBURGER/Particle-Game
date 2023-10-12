using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ParticleGame.Scenes;
using ParticleGame.State_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ParticleGame.Managers
{
    public class SceneManager : DrawableGameComponent
    {
        private IScene _currentScene;

        private readonly ContentManager _content;
        private readonly InputState _input = new InputState();

        public SpriteBatch SpriteBatch { get; private set; }

        public SpriteFont Font { get; private set; }

        public SceneManager(Game game) : base(game)
        {
            _content = new ContentManager(game.Services, "Content");
        }

        public void SetScene(IScene newScene)
        {
            if (_currentScene != null) { _currentScene.UnloadContent(); }

            _currentScene = newScene;
            newScene.SceneManager = this;

            _currentScene.LoadContent();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = _content.Load<SpriteFont>("Silkscreen");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _input.Update();

            _currentScene?.HandleInput(gameTime, _input);
            _currentScene?.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _currentScene?.Draw(gameTime);
        }
    }
}
