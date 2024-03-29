﻿using ParticleGame.Managers;
using ParticleGame.UI;
using System;

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
            SceneManager.SetScene(new GameplayScene());
        }

        private void Settings(object sender, EventArgs e)
        {
            SceneManager.SetScene(new SettingsMenuScene());
        }

        private void ExitClicked(object sender, EventArgs e)
        {
            SceneManager.Game.Exit();
        }
    }
}
