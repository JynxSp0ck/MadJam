using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Chunk {
        public IntVec3 pos;
        public string name;
        public Block[,,] blocks;
        public float[] data;
        public bool started = false;
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

        public Block getBlock(IntVec3 index) {
            if (index.x < 0 || index.y < 0 || index.z < 0 || index.x >= Settings.chunk_size || index.y >= Settings.chunk_size || index.z >= Settings.chunk_size)
                return new Block(BlockType.get("error"));
            return blocks[index.x, index.y, index.z];
        }

        public void depricate() {
            saved = false;
        }
    }
}
