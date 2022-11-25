using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BME.Rendering.Display;
using GLFW;
using static System.Collections.Specialized.BitVector32;

namespace BME.GameLoop.Input
{
    public class InputManager
    {
        Dictionary<string, Tuple<KeyCode, KeyState>> keyBindings;
        List<Tuple<KeyCode,KeyState>> keyStates;

        public InputManager() {
            keyBindings = new Dictionary<string, Tuple<KeyCode, KeyState>>();
            keyStates = new List<Tuple<KeyCode, KeyState>>();
        }

        public void Update() {
            for (int i = 0; i < keyBindings.Count; i++) {
                KeyState _newKeyState = keyStates[i].Item2;

                bool _keyPressed = Glfw.GetKey(DisplayManager.window, (Keys)keyStates[i].Item1) == InputState.Press;

                if (_keyPressed) {
                    if (_newKeyState == KeyState.NotPressed) _newKeyState = KeyState.Down;
                    else if (_newKeyState == KeyState.Down)  _newKeyState = KeyState.Pressed;
                } else {
                    if (_newKeyState == KeyState.Pressed) _newKeyState = KeyState.Up;
                    else if (_newKeyState == KeyState.Up) _newKeyState = KeyState.NotPressed;
                }

                keyStates[i] = new Tuple<KeyCode, KeyState>(keyStates[i].Item1, _newKeyState);
            }
        }

        public void AddKeyAction(string _actionName, KeyCode _key) {
            Tuple<KeyCode, KeyState> _newKeyState = new Tuple<KeyCode, KeyState>(_key, KeyState.NotPressed);

            keyBindings.Add(_actionName, _newKeyState);
            keyStates.Add(_newKeyState);
        }

        public void RemoveAction(string _actionName) {
            keyBindings.TryGetValue(_actionName);
        }


        public static bool GetKey(KeyCode _key) {
            InputState _state = Glfw.GetKey(DisplayManager.window, (Keys)_key);
            
            if (_state == InputState.Press) return true;

            return false;
        }

        public static bool GetKeyUp(KeyCode _key) {
            InputState _state = Glfw.GetKey(DisplayManager.window, (Keys)_key);
            if (_state == InputState.Release) return true;

            return false;
        }

        public static bool GetMouseButton(MouseButton _button) {
            InputState _state = Glfw.GetMouseButton(DisplayManager.window, (GLFW.MouseButton)_button);
            if (_state == InputState.Press) return true;

            return false;
        }

        public static Vector2 GetMousePosition() {
            double _dX, _dY;
            Glfw.GetCursorPosition(DisplayManager.window, out _dX, out _dY);
            return new Vector2((float)_dX, (float)_dY);
        }
    }
}
