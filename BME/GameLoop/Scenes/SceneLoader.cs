using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
using BME.Rendering.Camera;
using BME.Rendering.Shaders;
using BME.Rendering.Renderer;

namespace BME.GameLoop.Scenes
{
    class SceneLoader
    {

        // TODO: testing and safty

        public static Scene LoadScene(string _sceneFilePath) {
            Scene _scene = new Scene();

            string _rawSceneFile = File.ReadAllText(_sceneFilePath);
            Console.WriteLine($"Loading Scene {_sceneFilePath}");

            string _sceneFile = Regex.Replace(_rawSceneFile, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            string[] _lines = _sceneFile.Split('\n');

            _scene.name = ReadUtil.ReadStrValue(_lines[0]);

            Vector3 _cameraPosition = ReadUtil.ReadVec3Value(_lines[1]);
            float _cameraZoom = ReadUtil.ReadFloatValue(_lines[2]);
            int _maxVertecis = ReadUtil.ReadIntValue(_lines[3]);
            _scene.camera = new Camera2D(new Vector2(_cameraPosition.X, _cameraPosition.Y), _cameraZoom);

            string _vShaderPath = ReadUtil.ReadStrValue(_lines[4]);
            string _fShaderPath = ReadUtil.ReadStrValue(_lines[5]);
            _scene.shader = new Shader(_vShaderPath, _fShaderPath);

            _scene.entityManeger = new EntityManeger();
            _scene.batchRenderer = new BatchRenderer(_scene.camera, _maxVertecis);
            _scene.renderer = new SpriteRenderer(_scene.shader, _scene.camera);
            _scene.textRenderer = new TextRenderer(_scene.camera);


            DependencyLoader _dependencyLoader = new DependencyLoader();
            for (int i = 5; i < _lines.Length; i++) {
                if(_lines[i].StartsWith("D"))
                    _dependencyLoader.LoadDependency(_lines[i], _scene);
            }
            
            return _scene;
        }
    }
}

/* BME Scene protocol
 * 
 * - Value Setup ----------------------------------------------
 * Int Value:   I <ValueName>=<Int> 
 * Float Value: F <ValueName>=<Float>
 * Str Value:   S <ValueName>=<Str>
 * Vec3 Value:  V <ValueName>=<Float>,<Float>,<Float>
 * 
 * Dep Value:   D <ValueName>=<DepID>,<DepPath>
 *
 * - Main File Setup -------------------------------------------
 * Line 1: Internal Scene name:      S internalName=<Name>
 *      
 * Line 2: Camera position:          V cameraPos=<Vector3>
 * Line 3: Camera zoom:              F cameraZoom=<ZoomValue>
 * Line 4: Max Vertecis:             I maxVertecis=<Int>
 * 
 * Line 4: Base VertexShaderPath     S vertexShaderPath=<path>
 * Line 5: Base FragmentShaderPath   S fragmentShaderpath=<path>
 * 
 *  -- Dependencys --
 * 
 */
