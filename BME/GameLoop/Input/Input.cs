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
