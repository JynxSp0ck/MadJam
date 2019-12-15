using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Model {
        public int money = 0;
        public Player player;
        public List<Character> characters;
        public Map map;

        public Model() {
            setupblocks();
            characters = new List<Character>();
            player = new Player();
            map = new Map("map");
        }

        void setupblocks() {
            BlockType.add("error", true, false, 0);
            BlockType.add("air", true, false, 0);
            BlockType.add("dirt", false, true, 10);
            BlockType.add("trash", false, true, 20);
            BlockType.add("coal", false, true, 40);
        }
    }
}
