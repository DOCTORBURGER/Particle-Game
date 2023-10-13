using Microsoft.Xna.Framework;
using ParticleGame.UI;
using System;
using System.Collections.Generic;

namespace ParticleGame.Scenes
{
    public class SettingsMenuScene : MenuScene
    {
        private List<Point> _supportedResolutions = new List<Point>
        {
            new Point(1280, 720),
            new Point(1920, 1080),
            new Point(2560, 1440)
        };

        private static int _currentResolutionIndex = 0;

        public SettingsMenuScene() : base("Particle Game")
        {
            var soundEffectVolumeEntry = new MenuEntry("Sound Effects (Not supported): #");
            var musicVolumeEntry = new MenuEntry("Music (Not supported): #");
            var resolutionEntry = new MenuEntry($"Resolution: {_supportedResolutions[_currentResolutionIndex].X}x{_supportedResolutions[_currentResolutionIndex].Y}");
            var backEntry = new MenuEntry("Back");

            resolutionEntry.Selected += SetResolution;
            backEntry.Selected += Back;

            _menuEntries.Add(soundEffectVolumeEntry);
            _menuEntries.Add(musicVolumeEntry);
            _menuEntries.Add(resolutionEntry);
            _menuEntries.Add(backEntry);
        }

        private void SetResolution(object sender, EventArgs e)
        {
            _currentResolutionIndex++;
            if (_currentResolutionIndex >= _supportedResolutions.Count)
                _currentResolutionIndex = 0;
            _menuEntries[2].Text = $"Resolution: {_supportedResolutions[_currentResolutionIndex].X}x{_supportedResolutions[_currentResolutionIndex].Y}";
            SceneManager.SetResolution(_supportedResolutions[_currentResolutionIndex]);
        }

        private void Back(object sender, EventArgs e)
        {
            SceneManager.SetScene(new MainMenuScene());
        }
    }
}
