using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Chunk {
        public IntVec3 pos;
        public Block[,,] blocks;
        public bool loaded = false;
        public bool saved = false;

        public Chunk(IntVec3 pos) {
            this.pos = pos;
            blocks = new Block[Settings.chunk_size, Settings.chunk_size, Settings.chunk_size];
            for (int i = 0; i < Settings.chunk_size; i++) {
                for (int j = 0; j < Settings.chunk_size; j++) {
                    for (int k = 0; k < Settings.chunk_size; k++) {
                        blocks[i, j, k] = null;
                    }
                }
            }
        }

        public void depricate() {
            saved = false;
        }
    }
}
