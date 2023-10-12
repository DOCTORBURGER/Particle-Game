using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ParticleGame.Scenes;
using ParticleGame.State_Management;

namespace ParticleGame.Managers
{
    public class SceneManager : DrawableGameComponent
    {
        private IScene _currentScene;

        private readonly ContentManager _content;
        private readonly InputState _input = new InputState();

        private Matrix _scaleMatrix = Matrix.Identity;
        public readonly Point _virtualResolution = new Point(1280, 720);

        public Matrix ScaleMatrix => _scaleMatrix;

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
            CalculateMatrix();
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

        public void CalculateMatrix()
        {
            float scaleX = (float)Game.GraphicsDevice.Viewport.Width / _virtualResolution.X;
            float scaleY = (float)Game.GraphicsDevice.Viewport.Height / _virtualResolution.Y;

            _scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}
