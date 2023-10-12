using Microsoft.Xna.Framework;
using ParticleGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int _currentResolutionIndex = 0;

        public SettingsMenuScene() : base("Particle Game")
        {
            var soundEffectVolumeEntry = new MenuEntry("Sound Effects (Not supported): #");
            var musicVolumeEntry = new MenuEntry("Music (Not supported): #");
            var resolutionEntry = new MenuEntry("Resolution: 1280x720");
            var applyEntry = new MenuEntry("Apply");
            var backEntry = new MenuEntry("Back");

            resolutionEntry.Selected += SetResolution;
            applyEntry.Selected += Apply;
            backEntry.Selected += Back;

            _menuEntries.Add(soundEffectVolumeEntry);
            _menuEntries.Add(musicVolumeEntry);
            _menuEntries.Add(resolutionEntry);
            _menuEntries.Add(applyEntry);
            _menuEntries.Add(backEntry);
        }

        private void SetResolution(object sender, EventArgs e)
        {
            _currentResolutionIndex++;
            if (_currentResolutionIndex >= _supportedResolutions.Count)
                _currentResolutionIndex = 0;
            _menuEntries[2].Text = $"Resolution: {_supportedResolutions[_currentResolutionIndex].X}x{_supportedResolutions[_currentResolutionIndex].Y}";
        }

        private void Apply(object sender, EventArgs e)
        {
            SceneManager.SetResolution(_supportedResolutions[_currentResolutionIndex]);
        }

        private void Back(object sender, EventArgs e)
        {
            SceneManager.SetScene(new MainMenuScene());
        }
    }
}
