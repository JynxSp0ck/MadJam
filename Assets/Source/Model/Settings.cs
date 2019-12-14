using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Settings {
        public static int chunk_size = 16;
        public static int load_distance = 1;
        public static int memory_distance = 2;
        public static int data_distance = 2;
        public static int offset;
        public static int map_size;

        public static void calculate() {
            offset = data_distance + load_distance;
            if (memory_distance > offset)
                offset = memory_distance;
            map_size = offset * 2 + 1;
        }
    }
}
