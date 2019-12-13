using System;
using System.Collections.Generic;

namespace Game.Utility {
    public class Vec2 {
        public float x;
        public float y;

        public Vec2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static Vec2 operator +(Vec2 v1, Vec2 v2) {
            return new Vec2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vec2 operator -(Vec2 v1, Vec2 v2) {
            return new Vec2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vec2 operator -(Vec2 v) {
            return new Vec2(-v.x, -v.y);
        }

        public static Vec2 operator *(Vec2 v, float n) {
            return new Vec2(v.x * n, v.y * n);
        }

        public static Vec2 operator *(float n, Vec2 v) {
            return new Vec2(v.x * n, v.y * n);
        }

        public static Vec2 operator /(Vec2 v, float n) {
            return new Vec2(v.x / n, v.y / n);
        }

        public static bool operator ==(Vec2 v1, Vec2 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return true;
            if (object.ReferenceEquals(v1, null))
                return false;
            if (object.ReferenceEquals(v2, null))
                return false;
            return v1.x == v2.x && v1.y == v2.y;
        }

        public static bool operator !=(Vec2 v1, Vec2 v2) {
            if (object.ReferenceEquals(v1, null) && object.ReferenceEquals(v2, null))
                return false;
            if (object.ReferenceEquals(v1, null))
                return true;
            if (object.ReferenceEquals(v2, null))
                return true;
            return !(v1.x == v2.x && v1.y == v2.y);
        }

        public IntVec2 Int() {
            return new IntVec2((int)x, (int)y);
        }

        public float mag2() {
            return x * x + y * y;
        }

        public float mag() {
            return (float)Math.Sqrt(mag2());
        }

        public Vec2 clone() {
            return new Vec2(x, y);
        }
    }
}
