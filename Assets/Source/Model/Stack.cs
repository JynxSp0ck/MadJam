using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Stack {
        public BlockType type;
        public int count;

        public Stack() {
            empty();
        }

        public Stack(BlockType type, int count) {
            this.type = type;
            this.count = count;
        }

        public void empty() {
            type = null;
            count = 0;
        }
    }
}
