using GLFW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Drawing;
using static OpenGL.OpenGL.GL;

namespace BME.Rendering.Display {
    public static class DisplayManager
    {
        public static Window window { get; set; }
        public static Vector2 windowSize { get; set; }
        public static Vector4 clearColor { get; set; } = Vector4.Zero;
        public static int textureUnitCount { get; private set; }

        public static unsafe void CreateWindow(int _width, int _height, string _title) {
            windowSize = new Vector2(_width, _height);

            Glfw.Init();

            // opengl 3.3 core
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, false);

            // create window
            window = Glfw.CreateWindow(_width, _height, _title, GLFW.Monitor.None, Window.None);

            if (window == Window.None) {
                Console.WriteLine("creation of window has failed");
                return;
            }

            Rectangle _screen = Glfw.PrimaryMonitor.WorkArea;
            int _x = (_screen.Width - _width) / 2;
            int _y = (_screen.Height - _height) / 2;

            Glfw.SetWindowPosition(window, _x, _y);

            Glfw.MakeContextCurrent(window);

            // import all gl functions
            Import(Glfw.GetProcAddress);

            glViewport(0,0, _width, _height);

            // GL Settings
            Glfw.SwapInterval(0); // VSync: 0:off, 1:on 
            glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
            glEnable(GL_BLEND);
            glEnable(GL_DEPTH_TEST);

            int _textureUnitCount;
            glGetIntegerv(GL_MAX_TEXTURE_IMAGE_UNITS, &_textureUnitCount);
            textureUnitCount = _textureUnitCount;
        }

        public static void EnableVSync(bool _isEnabled) {
            if (_isEnabled) {
                Glfw.SwapInterval(1);
                return;
            }
            Glfw.SwapInterval(0);
        }

        public static void CloseWindow() {
            Glfw.Terminate();
        }
    }
}
