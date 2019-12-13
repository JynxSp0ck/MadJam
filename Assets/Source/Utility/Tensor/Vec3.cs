using System;
using System.Collections.Generic;

namespace Game.Utility {
    public class Vec3 {
        public float x;
        public float y;
        public float z;

        public Vec3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vec3 operator +(Vec3 v1, Vec3 v2) {
            return new Vec3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vec3 operator -(Vec3 v1, Vec3 v2) {
            return new Vec3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vec3 operator -(Vec3 v) {
            return new Vec3(-v.x, -v.y, -v.z);
        }

        public static Vec3 operator *(Vec3 v, float n) {
            return new Vec3(v.x * n, v.y * n, v.z * n);
        }

        public static Vec3 operator *(float n, Vec3 v) {
            return new Vec3(v.x * n, v.y * n, v.z * n);
        }

        public static Vec3 operator /(Vec3 v, float n) {
            return new Vec3(v.x / n, v.y / n, v.z / n);
        }

        public static bool operator ==(Vec3 v1, Vec3 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return true;
            if (object.ReferenceEquals(v1, null))
                return false;
            if (object.ReferenceEquals(v2, null))
                return false;
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public static bool operator !=(Vec3 v1, Vec3 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return false;
            if (object.ReferenceEquals(v1, null))
                return true;
            if (object.ReferenceEquals(v2, null))
                return true;
            return !(v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
        }

        public IntVec3 Int() {
            return new IntVec3((int)x, (int)y, (int)z);
        }

        public float mag2() {
            return x * x + y * y + z * z;
        }

        public float mag() {
            return (float)Math.Sqrt(mag2());
        }

        public Vec3 clone() {
            return new Vec3(x, y, z);
        }
    }
}
