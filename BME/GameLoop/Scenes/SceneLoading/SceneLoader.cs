using BME.GameLoop.Scenes.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.GameLoop.Scenes.SceneLoading
{
    class SceneLoader
    {
        private static Dictionary<int, Action<Entity, Scene>> depLoaders;

        public static void SaveScene(Scene _scene) {
            List<string> _deblist = new List<string>();

            if (depLoaders == null) InitDepLoaders();

            foreach (Entity e in _scene.entityManeger.entities) {

            }

            DS_Scene _db_Scene = new DS_Scene(
                _scene.name,
                _scene.camera.focusPosition, _scene.camera.Zoom,
                10_000,
                "./res/shaders/spriteShader.vs", "./res/shaders/spriteShader.fs",
                _deblist);
        }

        private static void InitDepLoaders() {
            depLoaders = new Dictionary<int, Action<Entity, Scene>>();
            //depLoaders.Add((int)DependencyType.Sprite, );
        }

    }
}
