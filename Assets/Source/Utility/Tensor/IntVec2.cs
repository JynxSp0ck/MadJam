using System;
using System.Collections.Generic;

namespace Game.Utility {
    public class IntVec2 {
        public int x;
        public int y;

        public IntVec2(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static IntVec2 operator +(IntVec2 v1, IntVec2 v2) {
            return new IntVec2(v1.x + v2.x, v1.y + v2.y);
        }

        public static IntVec2 operator -(IntVec2 v1, IntVec2 v2) {
            return new IntVec2(v1.x - v2.x, v1.y - v2.y);
        }

        public static IntVec2 operator -(IntVec2 v) {
            return new IntVec2(-v.x, -v.y);
        }

        public static IntVec2 operator *(IntVec2 v, int n) {
            return new IntVec2(v.x * n, v.y * n);
        }

        public static IntVec2 operator *(int n, IntVec2 v) {
            return new IntVec2(v.x * n, v.y * n);
        }

        public static IntVec2 operator /(IntVec2 v, int n) {
            return new IntVec2(v.x / n, v.y / n);
        }

        public static bool operator ==(IntVec2 v1, IntVec2 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return true;
            if (object.ReferenceEquals(v1, null))
                return false;
            if (object.ReferenceEquals(v2, null))
                return false;
            return v1.x == v2.x && v1.y == v2.y;
        }

        public static bool operator !=(IntVec2 v1, IntVec2 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return false;
            if (object.ReferenceEquals(v1, null))
                return true;
            if (object.ReferenceEquals(v2, null))
                return true;
            return !(v1.x == v2.x && v1.y == v2.y);
        }

        public Vec2 Float() {
            return new Vec2(x, y);
        }

        public int mag2() {
            return x * x + y * y;
        }

        public float mag() {
            return (float)Math.Sqrt(mag2());
        }

        public IntVec2 clone() {
            return new IntVec2(x, y);
        }
    }
}
