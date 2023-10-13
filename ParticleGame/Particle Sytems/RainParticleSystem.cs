using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ParticleGame.Managers;

namespace ParticleGame.Particle_Sytems
{
    public class RainParticleSystem : ParticleSystem, ISoundEmitter
    {
        Rectangle _source;

        public override string Name => "Rain";

        public Vector2 Wind { get; set; } = Vector2.Zero;

        private SoundEffect _rainSoundEffect;
        private SoundEffectInstance _rainSoundInstance = null;

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

        protected override void LoadContent()
        {
            base.LoadContent();

            _rainSoundEffect = contentManager.Load<SoundEffect>("rain");
            _rainSoundInstance = _rainSoundEffect.CreateInstance();
            _rainSoundInstance.IsLooped = true;
            _rainSoundInstance.Play();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _rainSoundInstance?.Stop();
                _rainSoundInstance?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, new Vector2(-100, 1300), Vector2.Zero, Color.White, lifetime: 2f, rotation: 0.1f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            AddParticles(_source);
        }

        public void PlayNoise()
        {
            _rainSoundInstance?.Play();
        }

        public void StopNoise()
        {
            _rainSoundInstance?.Stop();
        }
    }
}
