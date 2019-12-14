using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Block {
        public BlockType type;
        public bool hidden = false;

        public Block(string name) {
            type = BlockType.get(name);
        }
        
        public Block(BlockType name) {
            type = name;
        }

        public void update() {
            //TODO
        }
    }
}
