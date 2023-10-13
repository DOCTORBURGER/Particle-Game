using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using ParticleGame.Managers;
using System;

namespace ParticleGame.Particle_Sytems
{
    public class FireBallParticleSystem : ParticleSystem, IEmit, ISoundEmitter
    {
        public override string Name => "Fire";

        private SoundEffect _fireballSoundEffect;
        private SoundEffectInstance _fireballSoundInstance = null;

        public FireBallParticleSystem(Game game, SceneManager sceneManager) : base(game, 2500, sceneManager) { }

        protected override void InitializeConstants()
        {
            textureFilename = "Pixel5x5";
            minNumParticles = 15;
            maxNumParticles = 20;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _fireballSoundEffect = contentManager.Load<SoundEffect>("fire");
            _fireballSoundInstance = _fireballSoundEffect.CreateInstance();
            _fireballSoundInstance.IsLooped = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fireballSoundInstance?.Stop();
                _fireballSoundInstance?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(200, 300);

            var lifetime = RandomHelper.NextFloat(0.2f, 0.35f);

            var acceleration = Vector2.UnitY * -1000;

            var rotation = MathF.Atan2(velocity.Y, velocity.X) + MathF.PI / 2f + MathF.PI;

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var scale = RandomHelper.NextFloat(2, 3f);

            p.Initialize(where, velocity, acceleration, new Color(252, 78, 3), lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Emit(Vector2 where) 
        {
            AddParticles(where);
        }

        public void PlayNoise()
        {
            _fireballSoundInstance?.Play();
        }

        public void StopNoise()
        {
            _fireballSoundInstance?.Stop();
        }
    }
}
