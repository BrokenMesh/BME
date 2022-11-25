using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using BME.Rendering.Display;

namespace BME.Rendering.Camera {
    public class Camera2D
    {

        public Vector2 focusPosition { get; private set; }
        public float Zoom { get; private set; }

        private Matrix4x4 ProjectionMat;

        public Camera2D(Vector2 focusPosition, float zoom) {
            SetCamera(focusPosition, zoom);
        }

        public void SetCamera(Vector2 focusPosition, float zoom) {
            this.focusPosition = focusPosition;
            Zoom = zoom;

            float _left = focusPosition.X - DisplayManager.windowSize.X / 2f;
            float _right = focusPosition.X + DisplayManager.windowSize.X / 2f;
            float _top = focusPosition.Y - DisplayManager.windowSize.Y / 2f;
            float _bottom = focusPosition.Y + DisplayManager.windowSize.Y / 2f;

            //Matrix4x4 _orthoMatrix = Matrix4x4.CreatePerspectiveFieldOfView(80.0f * ((float)Math.PI / 180.0f), DisplayManager.windowSize.X / DisplayManager.windowSize.Y, 0.01f, 100f);
            Matrix4x4 _orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(_left, _right, _bottom, _top, 0.01f, 100f);
            Matrix4x4 _zoomMatrix = Matrix4x4.CreateScale(Zoom);

            ProjectionMat =  _orthoMatrix * _zoomMatrix;
        }

        public Matrix4x4 GetProjectionMatrix() {
            return ProjectionMat;
        }

    }
}
