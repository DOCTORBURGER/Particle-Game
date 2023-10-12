﻿using Microsoft.Xna.Framework;
using ParticleGame.Managers;
using ParticleGame.State_Management;
using ParticleGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleGame.Scenes
{
    public class MainMenuScene : MenuScene
    {
        public MainMenuScene() : base("Particle Game")
        {
            var startGameEntry = new MenuEntry("Start Game");
            var settingsEntry = new MenuEntry("Settings");
            var exitEntry = new MenuEntry("Exit");

            startGameEntry.Selected += StartGame;
            settingsEntry.Selected += Settings;
            exitEntry.Selected += ExitClicked;

            _menuEntries.Add(startGameEntry);
            _menuEntries.Add(settingsEntry);
            _menuEntries.Add(exitEntry);
        }

        private void StartGame(object sender, EventArgs e)
        {

        }

        private void Settings(object sender, EventArgs e)
        {

        }

        private void ExitClicked(object sender, EventArgs e)
        {
            SceneManager.Game.Exit();
        }
    }
}
