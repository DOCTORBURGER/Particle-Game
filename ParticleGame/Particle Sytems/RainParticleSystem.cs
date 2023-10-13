using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleGame.Managers;

namespace ParticleGame.Particle_Sytems
{
    public class RainParticleSystem : ParticleSystem
    {
        Rectangle _source;

        public override string Name => "Rain";

        public Vector2 Wind { get; set; } = Vector2.Zero;

        public RainParticleSystem(Game game, Rectangle source, SceneManager sceneManager) : base(game, 2500, sceneManager)
        {
            _source = source;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "raindrop";
            minNumParticles = 1;
            maxNumParticles = 3;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, new Vector2(-100, 1300), Vector2.Zero, Color.White, lifetime: 2f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            AddParticles(_source);
        }
    }
}
