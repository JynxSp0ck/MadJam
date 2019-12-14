using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Map {
        public Chunk[,,] chunks;
        public IntVec3 chunkpos;

        public Map() {
            chunkpos = new IntVec3(0, 0, 0);
            chunks = new Chunk[Settings.map_size, Settings.map_size, Settings.map_size];
            for (int i = 0; i < Settings.map_size ; i++) {
                for (int j = 0; j < Settings.map_size; j++) {
                    for (int k = 0; k < Settings.map_size; k++) {
                        chunks[i, j, k] = null;
                    }
                }
            }
            generateChunks();
        }

        public void setChunkPos(IntVec3 pos) {
            if (pos == chunkpos)
                return;
            IntVec3 delta = pos - chunkpos;
            Chunk[,,] newchunks = new Chunk[Settings.map_size, Settings.map_size, Settings.map_size];
            for (int i = 0; i < Settings.map_size; i++) {
                for (int j = 0; j < Settings.map_size; j++) {
                    for (int k = 0; k < Settings.map_size; k++) {
                        newchunks[i, j, k] = getChunk(new IntVec3(i, j, k) + chunkpos - new IntVec3(Settings.offset, Settings.offset, Settings.offset) + delta);
                    }
                }
            }
            chunks = newchunks;
            chunkpos = pos;
            generateChunks();
        }

        public void generateChunks() {
            for (int i = -Settings.load_distance; i <= Settings.load_distance; i++) {
                for (int j = -Settings.load_distance; j <= Settings.load_distance; j++) {
                    for (int k = -Settings.load_distance; k <= Settings.load_distance; k++) {
                        chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset] = new Chunk(new IntVec3(i, j, k) + chunkpos);
                    }
                }
            }
        }

        public Chunk getChunk(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            IntVec3 cindex = localindex - chunkpos;
            if (cindex.x < -Settings.offset || cindex.y < -Settings.offset || cindex.z < -Settings.offset || cindex.x > Settings.offset || cindex.y > Settings.offset || cindex.z > Settings.offset)
                return null;
            cindex += new IntVec3(Settings.offset, Settings.offset, Settings.offset);
            return chunks[cindex.x, cindex.y, cindex.z];
        }

        public IntVec3 getBlockIndex(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            Chunk chunk = getChunk(pos);
            if (chunk == null)
                return null;
            if (!chunk.generated)
                return null;
            return pos - localindex * Settings.chunk_size;
        }

        public Block getBlock(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            Chunk chunk = getChunk(pos);
            if (chunk == null)
                return null;
            if (!chunk.generated)
                return null;
            IntVec3 bindex = pos - localindex * Settings.chunk_size;
            return chunk.blocks[bindex.x, bindex.y, bindex.z];
        }
    }
}
