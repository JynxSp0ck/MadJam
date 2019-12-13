using System;
using System.Collections.Generic;

namespace Game.Utility {
    public class Vec4 {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vec4(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Vec4 operator +(Vec4 v1, Vec4 v2) {
            return new Vec4(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }

        public static Vec4 operator -(Vec4 v1, Vec4 v2) {
            return new Vec4(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        public static Vec4 operator -(Vec4 v) {
            return new Vec4(-v.x, -v.y, -v.z, -v.w);
        }

        public static Vec4 operator *(Vec4 v, float n) {
            return new Vec4(v.x * n, v.y * n, v.z * n, v.w * n);
        }

        public static Vec4 operator *(float n, Vec4 v) {
            return new Vec4(v.x * n, v.y * n, v.z * n, v.w * n);
        }

        public static Vec4 operator /(Vec4 v, float n) {
            return new Vec4(v.x / n, v.y / n, v.z / n, v.w / n);
        }

        public static bool operator ==(Vec4 v1, Vec4 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return true;
            if (object.ReferenceEquals(v1, null))
                return false;
            if (object.ReferenceEquals(v2, null))
                return false;
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w;
        }

        public static bool operator !=(Vec4 v1, Vec4 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return false;
            if (object.ReferenceEquals(v1, null))
                return true;
            if (object.ReferenceEquals(v2, null))
                return true;
            return !(v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
        }

        public IntVec4 Int() {
            return new IntVec4((int)x, (int)y, (int)z, (int)w);
        }

        public Vec4 clone() {
            return new Vec4(x, y, z, w);
        }
    }
}
