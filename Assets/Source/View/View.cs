using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class View {
        public Camera camera;
        public UI ui;
        public World world;
        public Materials materials;

        public View() {
        }

        public void init(GameObject canvas, GameObject world) {
            materials = new Materials();
            camera = new Camera();
            ui = new UI(canvas);
            ui.init();
            this.world = new World(world);
            this.world.setChunks();
        }

        public void update() {
            camera.update();
            world.update();
            ui.update();
        }
    }
}
