﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ParticleGame.Managers;
using System;

namespace ParticleGame.Particle_Sytems
{
    public class MouseSparksParticleSystem : ParticleSystem, IEmit, ISoundEmitter
    {
        public override string Name => "Sparks";

        public MouseSparksParticleSystem(Game game, SceneManager sceneManager) : base(game, 2500, sceneManager) { }

        private SoundEffect _sparksSoundEffect;
        private SoundEffectInstance _sparksSoundInstance = null;

        protected override void InitializeConstants()
        {
            textureFilename = "raindrop";
            minNumParticles = 5;
            maxNumParticles = 10;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _sparksSoundEffect = contentManager.Load<SoundEffect>("sparks");
            _sparksSoundInstance = _sparksSoundEffect.CreateInstance();
            _sparksSoundInstance.IsLooped = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sparksSoundInstance?.Stop();
                _sparksSoundInstance?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(600, 800);

            var lifetime = RandomHelper.NextFloat(0.1f, 0.25f);

            var acceleration = Vector2.Zero;

            var rotation = MathF.Atan2(velocity.Y, velocity.X) + MathF.PI / 2f + MathF.PI;

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var scale = RandomHelper.NextFloat(1, 1.5f);

            p.Initialize(where, velocity, acceleration, Color.Yellow, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
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
            _sparksSoundInstance?.Play();
        }

        public void StopNoise()
        {
            _sparksSoundInstance?.Stop();
        }
    }
}
