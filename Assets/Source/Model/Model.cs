using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Model {
        public Player player;
        public Map map;

        public Model() {
            setupblocks();
            player = new Player();
            map = new Map("map");
        }

        void setupblocks() {
            BlockType.add("air", true, false, 0);
            BlockType.add("dirt", false, true, 10);
            BlockType.add("coal", false, true, 40);
        }
    }
}
