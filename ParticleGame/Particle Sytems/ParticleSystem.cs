using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

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

        Particle[] particles;

        Queue<int> freeParticles;

        Texture2D texture;

        Vector2 origin;

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

        public int FreeParticleCount => freeParticles.Count;

        #endregion

        public ParticleSystem(Game game, int maxParticles) : base(game)
        {
            particles = new Particle[maxParticles];
            freeParticles = new Queue<int>(maxParticles);
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Initialize(Vector2.Zero);
                freeParticles.Enqueue(i);
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

            texture = contentManager.Load<Texture2D>(textureFilename);

            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < particles.Length; i++)
            {

                if (particles[i].Active)
                {
                    UpdateParticle(ref particles[i], dt);

                    if (!particles[i].Active)
                    {
                        freeParticles.Enqueue(i);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(blendState: blendState);

            foreach (Particle p in particles)
            {
                if (!p.Active)
                    continue;

                spriteBatch.Draw(texture, p.Position, null, p.Color,
                    p.Rotation, origin, p.Scale, SpriteEffects.None, 0.0f);
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
            for (int i = 0; i < numParticles && freeParticles.Count > 0; i++)
            {
                // grab a particle from the freeParticles queue, and Initialize it.
                int index = freeParticles.Dequeue();
                InitializeParticle(ref particles[index], where);
            }
        }

        protected void AddParticles(Rectangle where)
        {
            int numParticles =
                RandomHelper.Next(minNumParticles, maxNumParticles);

            for (int i = 0; i < numParticles && freeParticles.Count > 0; i++)
            {
                int index = freeParticles.Dequeue();
                InitializeParticle(ref particles[index], RandomHelper.RandomPosition(where));
            }
        }

        #endregion
    }
}
