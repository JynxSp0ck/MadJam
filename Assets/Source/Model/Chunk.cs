using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Chunk {
        public IntVec3 pos;
        public string name;
        public Block[,,] blocks;
        public float[] data;
        public bool loaded = false;
        public bool saved = false;

        public Chunk(IntVec3 pos) {
            this.pos = pos;
            name = (pos.x < 0 ? "N" : "P") + Math.Abs(pos.x) + (pos.y < 0 ? "N" : "P") + Math.Abs(pos.y) + (pos.z < 0 ? "N" : "P") + Math.Abs(pos.z);
            blocks = new Block[Settings.chunk_size, Settings.chunk_size, Settings.chunk_size];
            for (int i = 0; i < Settings.chunk_size; i++) {
                for (int j = 0; j < Settings.chunk_size; j++) {
                    for (int k = 0; k < Settings.chunk_size; k++) {
                        blocks[i, j, k] = null;
                    }
                }
            }
            data = Noise.random(512, Client.seed, pos);
        }

        public void depricate() {
            saved = false;
        }
    }
}
