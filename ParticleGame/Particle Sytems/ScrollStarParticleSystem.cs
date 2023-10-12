using Microsoft.Xna.Framework;

namespace ParticleGame.Particle_Sytems
{
    public class ScrollStarParticleSystem : ParticleSystem
    {
        Rectangle _source;

        private double _timer;

        private const float SPEED = 0.125f;

        public ScrollStarParticleSystem(Game game, Rectangle source) : base(game, 400)
        {
            _source = source;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "Pixel5x5";
            minNumParticles = 1;
            maxNumParticles = 5;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, Vector2.UnitY * 100, Vector2.Zero, Color.White, lifetime: 5);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > SPEED)
            {
                AddParticles(_source);
                _timer -= SPEED;
            }
        }
    }
}
