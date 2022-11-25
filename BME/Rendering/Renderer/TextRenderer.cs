using System;
using System.Collections.Generic;
using BME.Rendering.Camera;
using BME.Rendering.Shaders;
using BME.Rendering.Sprites;
using BME.Rendering.Textures;
using static OpenGL.OpenGL.GL;

namespace BME.Rendering.Renderer
{
    public class TextRenderer
    {
        private Camera2D camera;
        private Shader textShader;

        List<TextSprite> texts = new List<TextSprite>();

        public unsafe TextRenderer(Camera2D _camera) {
            camera = _camera;

            textShader = new Shader("./res/shaders/textRenderer.vs", "./res/shaders/textRenderer.fs");
            textShader.Bind();
        }

        public void Render() {
            foreach (TextSprite _text in texts) {
                RenderText(_text);
            }
        }

        public void AddText(TextSprite _text) {
            texts.Add(_text);
            texts.Sort((s1, s2) => s1.zDepth.CompareTo(s2.zDepth));
        }

        public void RemoveText(TextSprite _text) {
            texts.Remove(_text);
            texts.Sort((s1, s2) => s1.zDepth.CompareTo(s2.zDepth));
        }

        private void RenderText(TextSprite _text) {
            Texture _texture = _text.GetTexture();

            textShader.SetSlot("u_Atlas", 0);
            textShader.SetTexture(_texture, 0);

            textShader.Bind();
            textShader.SetMatrix4x4("u_Projection", camera.GetProjectionMatrix());
            textShader.SetMatrix4x4("u_Model", _text.GetModelMatrix());

            textShader.SetFloat("u_Width", _text.width);
            textShader.SetFloat("u_Edge", _text.fadeWidth);
            textShader.SetFloat("u_BorderWidth", _text.borderWidth);
            textShader.SetFloat("u_BorderEdge", _text.borderFadeWidth);
            textShader.SetVec3("u_OutlineColor", _text.borderColor);
            textShader.SetVec2("u_Offset", _text.borderOffset);

            glDepthMask(false);
            glBindVertexArray(_text.GetVAO());
            glDrawArrays(GL_TRIANGLES, 0, (int)_text.GetVertexCount());
            glBindVertexArray(0);
            glDepthMask(true);
        }

        public void Delete() {
            textShader.Delete();
        }

        public void DeleteAll() {
            textShader.Delete();
            foreach (TextSprite _t in texts)
                _t.Delete();
        }

    }
}
