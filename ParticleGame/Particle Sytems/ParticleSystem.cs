using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using ParticleGame.Managers;

namespace ParticleGame.Particle_Sytems
{
    public abstract class ParticleSystem : DrawableGameComponent
    {
        #region Constants

        public const int AlphaBlendDrawOrder = 100;

        public const int AdditiveBlendDrawOrder = 200;

        #endregion

        #region Static Fields

        protected static SpriteBatch spriteBatch;

        protected static ContentManager contentManager;

        #endregion

        #region Private Fields

        Particle[] _particles;

        Queue<int> _freeParticles;

        Texture2D _texture;

        Vector2 _origin;

        SceneManager _sceneManager;

        #endregion

        #region Protected Fields

        /// <summary>The BlendState to use with this particle system</summary>
        protected BlendState blendState = BlendState.AlphaBlend;

        /// <summary>The filename of the texture to use for the particles</summary>
        protected string textureFilename;

        /// <summary>The minimum number of particles to add when AddParticles() is called</summary>
        protected int minNumParticles;

        /// <summary>The maximum number of particles to add when AddParticles() is called</summary>
        protected int maxNumParticles;

        #endregion

        #region Public Properties 

        public int FreeParticleCount => _freeParticles.Count;

        public abstract string Name { get; }

        #endregion

        public ParticleSystem(Game game, int maxParticles, SceneManager sceneManager) : base(game)
        {
            _sceneManager = sceneManager;

            _particles = new Particle[maxParticles];
            _freeParticles = new Queue<int>(maxParticles);
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Initialize(Vector2.Zero);
                _freeParticles.Enqueue(i);
            }

            InitializeConstants();
        }

        #region Virtual Hook Methods

        protected abstract void InitializeConstants();

        protected virtual void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where);
        }

        protected virtual void UpdateParticle(ref Particle particle, float dt)
        {
            particle.Velocity += particle.Acceleration * dt;
            particle.Position += particle.Velocity * dt;

            particle.AngularVelocity += particle.AngularAcceleration * dt;
            particle.Rotation += particle.AngularVelocity * dt;

            particle.TimeSinceStart += dt;
        }

        #endregion

        #region Override Methods

        protected override void LoadContent()
        {
            if (contentManager == null) contentManager = new ContentManager(Game.Services, "Content");
            if (spriteBatch == null) spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            if (string.IsNullOrEmpty(textureFilename))
            {
                string message = "textureFilename wasn't set properly, so the " +
                    "particle system doesn't know what texture to load. Make " +
                    "sure your particle system's InitializeConstants function " +
                    "properly sets textureFilename.";
                throw new InvalidOperationException(message);
            }

            _texture = contentManager.Load<Texture2D>(textureFilename);

            _origin.X = _texture.Width / 2;
            _origin.Y = _texture.Height / 2;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < _particles.Length; i++)
            {

                if (_particles[i].Active)
                {
                    UpdateParticle(ref _particles[i], dt);

                    if (!_particles[i].Active)
                    {
                        _freeParticles.Enqueue(i);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: _sceneManager.ScaleMatrix, blendState: blendState);

            foreach (Particle p in _particles)
            {
                if (!p.Active)
                    continue;

                spriteBatch.Draw(_texture, p.Position, null, p.Color,
                    p.Rotation, _origin, p.Scale, SpriteEffects.None, 0.0f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region AddParticles Methods 

        protected void AddParticles(Vector2 where)
        {
            int numParticles =
                RandomHelper.Next(minNumParticles, maxNumParticles);

            // create that many particles, if you can.
            for (int i = 0; i < numParticles && _freeParticles.Count > 0; i++)
            {
                // grab a particle from the freeParticles queue, and Initialize it.
                int index = _freeParticles.Dequeue();
                InitializeParticle(ref _particles[index], where);
            }
        }

        protected void AddParticles(Rectangle where)
        {
            int numParticles =
                RandomHelper.Next(minNumParticles, maxNumParticles);

            for (int i = 0; i < numParticles && _freeParticles.Count > 0; i++)
            {
                int index = _freeParticles.Dequeue();
                InitializeParticle(ref _particles[index], RandomHelper.RandomPosition(where));
            }
        }

        #endregion
    }
}
