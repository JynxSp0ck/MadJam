using System;
using System.Collections.Generic;

namespace Game.Utility {
    public class IntVec3 {
        public int x;
        public int y;
        public int z;

        public IntVec3(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static IntVec3 operator +(IntVec3 v1, IntVec3 v2) {
            return new IntVec3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static IntVec3 operator -(IntVec3 v1, IntVec3 v2) {
            return new IntVec3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static IntVec3 operator -(IntVec3 v) {
            return new IntVec3(-v.x, -v.y, -v.z);
        }

        public static IntVec3 operator *(IntVec3 v, int n) {
            return new IntVec3(v.x * n, v.y * n, v.z * n);
        }

        public static IntVec3 operator *(int n, IntVec3 v) {
            return new IntVec3(v.x * n, v.y * n, v.z * n);
        }

        public static IntVec3 operator /(IntVec3 v, int n) {
            return new IntVec3(v.x / n, v.y / n, v.z / n);
        }

        public static bool operator ==(IntVec3 v1, IntVec3 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return true;
            if (object.ReferenceEquals(v1, null))
                return false;
            if (object.ReferenceEquals(v2, null))
                return false;
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public static bool operator !=(IntVec3 v1, IntVec3 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return false;
            if (object.ReferenceEquals(v1, null))
                return true;
            if (object.ReferenceEquals(v2, null))
                return true;
            return !(v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
        }

        public Vec3 Float() {
            return new Vec3(x, y, z);
        }

        public int mag2() {
            return x * x + y * y + z * z;
        }

        public float mag() {
            return (float)Math.Sqrt(mag2());
        }

        public IntVec3 clone() {
            return new IntVec3(x, y, z);
        }
    }
}
