using System;
using System.Collections.Generic;

namespace Game.Utility {
    public class IntVec4 {
        public int x;
        public int y;
        public int z;
        public int w;

        public IntVec4(int x, int y, int z, int w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static IntVec4 operator +(IntVec4 v1, IntVec4 v2) {
            return new IntVec4(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }

        public static IntVec4 operator -(IntVec4 v1, IntVec4 v2) {
            return new IntVec4(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        public static IntVec4 operator -(IntVec4 v) {
            return new IntVec4(-v.x, -v.y, -v.z, -v.w);
        }

        public static IntVec4 operator *(IntVec4 v, int n) {
            return new IntVec4(v.x * n, v.y * n, v.z * n, v.w * n);
        }

        public static IntVec4 operator *(int n, IntVec4 v) {
            return new IntVec4(v.x * n, v.y * n, v.z * n, v.w * n);
        }

        public static IntVec4 operator /(IntVec4 v, int n) {
            return new IntVec4(v.x / n, v.y / n, v.z / n, v.w / n);
        }

        public static bool operator ==(IntVec4 v1, IntVec4 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return true;
            if (object.ReferenceEquals(v1, null))
                return false;
            if (object.ReferenceEquals(v2, null))
                return false;
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w;
        }

        public static bool operator !=(IntVec4 v1, IntVec4 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return false;
            if (object.ReferenceEquals(v1, null))
                return true;
            if (object.ReferenceEquals(v2, null))
                return true;
            return !(v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w);
        }

        public Vec4 Float() {
            return new Vec4(x, y, z, w);
        }

        public IntVec4 clone() {
            return new IntVec4(x, y, z, w);
        }
    }
}
