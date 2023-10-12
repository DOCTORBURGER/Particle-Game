using Microsoft.Xna.Framework;
using ParticleGame.Managers;
using ParticleGame.State_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleGame.Scenes
{
    public interface IScene
    {
        SceneManager SceneManager { get; set; }

        void LoadContent();

        void UnloadContent();

        void HandleInput(GameTime gameTime, InputState input);

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);
    }
}
