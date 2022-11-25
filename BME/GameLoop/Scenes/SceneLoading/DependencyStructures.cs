using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.GameLoop.Scenes.SceneLoading
{

    public struct DS_Scene {
        string name;

        Vector2 cameraPosition;
        float cameraZoom;
        int maxVertecis;

        string vShaderFile;
        string fShaderFile;

        List<string> depFilename;

        public DS_Scene(string name, Vector2 cameraPosition, float cameraZoom, int maxVertecis, string vShaderFile, string fShaderFile, List<string> depFilename) {
            this.name = name;
            this.cameraPosition = cameraPosition;
            this.cameraZoom = cameraZoom;
            this.maxVertecis = maxVertecis;
            this.vShaderFile = vShaderFile;
            this.fShaderFile = fShaderFile;
            this.depFilename = depFilename;
        }
    }



}
