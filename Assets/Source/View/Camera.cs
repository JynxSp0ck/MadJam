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
            camera = Find.name("Camera");
        }

        public void update() {
            camera.transform.position = Conv.ert(pos);
            camera.transform.rotation = Quaternion.Euler(Conv.ert(new Vec3(0, ha, va)));
        }
    }
}
