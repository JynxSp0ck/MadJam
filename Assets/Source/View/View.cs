using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class View {
        public GameObject obj;

        public Camera camera;
        public World world;
        public Materials materials;

        public View() {
            obj = Find.name("World");
        }

        public void init() {
            materials = new Materials();
            camera = new Camera();
            world = new World();
            world.setChunks();
        }

        public void update() {
            camera.update();
            world.update();
        }
    }
}
