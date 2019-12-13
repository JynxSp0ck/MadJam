using System;
using System.Collections.Generic;

namespace Game.Utility {
    class Func {
        public static float lerp(Vec2 pos, float[,] d) {
            Vec2 p = pos.clone();
            if (p.x < 0)
                p.x = 0;
            if (p.y < 0)
                p.y = 0;
            if (p.x >= d.GetLength(0) - 1)
                p.x = d.GetLength(0) - 1.00001f;
            if (p.y >= d.GetLength(1) - 1)
                p.y = d.GetLength(1) - 1.00001f;
            int i = (int)p.x;
            int j = (int)p.y;
            float ri = p.x - i;
            float rj = p.y - j;
            float a = d[i, j] * (1 - ri) + d[i + 1, j] * ri;
            float b = d[i, j + 1] * (1 - ri) + d[i + 1, j + 1] * ri;
            return a * (1 - rj) + b * rj;
        }

        public static float slerp(Vec2 pos, float[,] d) {
            Vec2 p = pos.clone();
            if (p.x < 0)
                p.x = 0;
            if (p.y < 0)
                p.y = 0;
            if (p.x >= d.GetLength(0))
                p.x = d.GetLength(0) - 0.00001f;
            if (p.y >= d.GetLength(1))
                p.y = d.GetLength(1) - 0.00001f;
            int i = (int)p.x;
            int j = (int)p.y;
            float ri = p.x - i;
            float rj = p.y - j;
            float a = d[i, j] * (1 - ri) + d[i + 1, j] * ri;
            float b = d[i, j + 1] * (1 - ri) + d[i + 1, j + 1] * ri;
            return a * (1 - rj) + b * rj;
        }
    }
}
