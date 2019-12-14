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
            map = new Map();
        }

        void setupblocks() {
            BlockType.add("air", true);
            BlockType.add("dirt", false);
        }
    }
}
