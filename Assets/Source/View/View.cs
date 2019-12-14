using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class View {
        public Camera camera;

        public View() {

        }

        public void init() {
            camera = new Camera();
        }

        public void update() {
            camera.update();
        }
    }
}
