using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility {
    class Conv {
        public static Vector2 ert(Vec2 v) {
            return new Vector2(v.x, v.y);
        }

        public static Vector3 ert(Vec3 v) {
            return new Vector3(v.x, v.y, v.z);
        }

        public static Vector4 ert(Vec4 v) {
            return new Vector4(v.x, v.y, v.z, v.w);
        }

        public static Vec2 ert(Vector2 v) {
            return new Vec2(v.x, v.y);
        }

        public static Vec3 ert(Vector3 v) {
            return new Vec3(v.x, v.y, v.z);
        }

        public static Vec4 ert(Vector4 v) {
            return new Vec4(v.x, v.y, v.z, v.w);
        }
    }
}
