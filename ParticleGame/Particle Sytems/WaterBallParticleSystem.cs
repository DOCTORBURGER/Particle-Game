using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ParticleGame.Managers;
using System;

namespace ParticleGame.Particle_Sytems
{
    public class WaterBallParticleSystem : ParticleSystem, IEmit, ISoundEmitter
    {
        public override string Name => "Water";

        public WaterBallParticleSystem(Game game, SceneManager sceneManager) : base(game, 2500, sceneManager) { }

        private SoundEffect _waterballSoundEffect;
        private SoundEffectInstance _waterballSoundInstance = null;

        protected override void InitializeConstants()
        {
            textureFilename = "Pixel5x5";
            minNumParticles = 3;
            maxNumParticles = 5;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _waterballSoundEffect = contentManager.Load<SoundEffect>("water");
            _waterballSoundInstance = _waterballSoundEffect.CreateInstance();
            _waterballSoundInstance.IsLooped = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _waterballSoundInstance?.Stop();
                _waterballSoundInstance?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(50, 100);

            var lifetime = 3f;

            var acceleration = Vector2.UnitY * 800;

            var rotation = MathF.Atan2(velocity.Y, velocity.X) + MathF.PI / 2f + MathF.PI;

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var scale = 3f;

            p.Initialize(where, velocity, acceleration, new Color(3, 186, 252), lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
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
            _waterballSoundInstance?.Play();
        }

        public void StopNoise()
        {
            _waterballSoundInstance?.Stop();
        }
    }
}
