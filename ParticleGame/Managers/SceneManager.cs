using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ParticleGame.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ParticleGame.Managers
{
    public class SceneManager : DrawableGameComponent
    {
        private Scene currentScene;

        private readonly ContentManager content;

        public SceneManager(Game game) : base(game)
        {
            content = new ContentManager(game.Services, "Content");
        }

        public void SetScene(Scene newScene)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {

        }
    }
}
