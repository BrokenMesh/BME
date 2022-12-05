#pragma warning disable CA1416 // Validate platform compatibility
using System;
using System.Drawing;
using System.Collections.Generic;
using static OpenGL.OpenGL.GL;

namespace BME.Rendering.Textures
{
    public static class TextureLoader
    {
        public static unsafe Texture LoadTexture2D_win(string _file, int _textureFilter) {
            // TODO: find a non platform specific option

            Bitmap _textureImg = new Bitmap(Image.FromFile(_file));
            _textureImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
            System.Drawing.Imaging.BitmapData _bmpData = _textureImg.LockBits(new Rectangle(0, 0, _textureImg.Width, _textureImg.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, _textureImg.PixelFormat);

            Console.WriteLine($"img: src{_file} size={_textureImg.Width}, {_textureImg.Height} format={_textureImg.PixelFormat},  {_textureImg.RawFormat}");

            return new Texture(_file, _textureImg.Width, _textureImg.Height, (uint)_textureImg.PixelFormat, _textureFilter, _bmpData.Scan0);
        }

        public static unsafe Texture LoadTexture2DnoFlip_win(string _file, int _textureFilter)
        {
            // TODO: find a non platform specific option

            Bitmap _textureImg = new Bitmap(Image.FromFile(_file));
            System.Drawing.Imaging.BitmapData _bmpData = _textureImg.LockBits(new Rectangle(0, 0, _textureImg.Width, _textureImg.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, _textureImg.PixelFormat);

            Console.WriteLine($"img: src{_file} size={_textureImg.Width}, {_textureImg.Height} format={_textureImg.PixelFormat},  {_textureImg.RawFormat}");

            return new Texture(_file, _textureImg.Width, _textureImg.Height, (uint)_textureImg.PixelFormat, _textureFilter, _bmpData.Scan0);
        }

        public static unsafe List<Texture> LoadTexture2DList_win(string[] _files, int _textureFilter) {
            List<Texture> _textures = new List<Texture>();

            Console.WriteLine($"img array: src{_files[0]} size={_files.Length}");

            foreach (string _file in _files) {
                _textures.Add(LoadTexture2D_win(_file, _textureFilter));
            }

            return _textures;
        }

        public static unsafe Texture LoadTexture2DPart_win(string _file, int _textureFilter, float _uStart, float _vStart, float _uEnd, float _vEnd) {
            Bitmap _textureImg = new Bitmap(Image.FromFile(_file));
            
            int _imgWidth = (int)((float)_textureImg.Width * (_uEnd - _uStart));
            int _imgHeight = (int)((float)_textureImg.Height * (_vEnd - _vStart));

            Bitmap _texturePartBitmap = _textureImg.Clone(
                new Rectangle(
                    (int)((float)_textureImg.Width * _uStart),
                    (int)((float)_textureImg.Height * _vStart),
                    _imgWidth,
                    _imgHeight), 
                _textureImg.PixelFormat
                );

            _texturePartBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            System.Drawing.Imaging.BitmapData _bmpData = _texturePartBitmap.LockBits(new Rectangle(0, 0, _texturePartBitmap.Width, _texturePartBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, _texturePartBitmap.PixelFormat);

            Console.WriteLine($"img part: src{_file} size={_imgWidth}, {_imgHeight} uv={_uStart}-{_uEnd}, {_vStart}-{_vEnd} format={_textureImg.PixelFormat}");

            return new Texture(_file, _imgWidth, _imgHeight, (uint)_textureImg.PixelFormat, _textureFilter, _bmpData.Scan0);
        }

    }
}

#pragma warning restore CA1416 // Validate platform compatibility