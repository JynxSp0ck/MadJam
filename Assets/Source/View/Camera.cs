using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class Camera {
        GameObject camera;
        public Vec3 pos = new Vec3(0, 0, 0);
        public float ha = 0;//horizontal angle
        public float va = 0;//vertical angle

        public Camera() {
            camera = UnityEngine.Camera.main.gameObject;
        }

        public void update() {
            pos = Client.model.player.pos;
            camera.transform.position = Conv.ert(pos + new Vec3(0, 1.5f, 0));
            camera.transform.rotation = Quaternion.Euler(Conv.ert(new Vec3(va, ha, 0)));
        }
    }
}
