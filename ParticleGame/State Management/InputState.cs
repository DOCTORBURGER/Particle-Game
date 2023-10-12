using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ParticleGame.State_Management
{
    public class InputState
    {
        public KeyboardState CurrentKeyboardState;

        private KeyboardState _previousKeyboardState;

        public InputState()
        {
            CurrentKeyboardState = new KeyboardState();

            _previousKeyboardState = new KeyboardState();
        }

        public void Update()
        {
            _previousKeyboardState = CurrentKeyboardState;

            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool IsNewKeyPress(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }
    }
}
