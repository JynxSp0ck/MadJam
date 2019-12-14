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
        }

        public void setChunkPos(IntVec3 pos) {
            if (pos == chunkpos)
                return;
            IntVec3 delta = pos - chunkpos;
            Chunk[,,] newchunks = new Chunk[Settings.map_size, Settings.map_size, Settings.map_size];
            for (int i = 0; i < Settings.map_size; i++) {
                for (int j = 0; j < Settings.map_size; j++) {
                    for (int k = 0; k < Settings.map_size; k++) {
                        newchunks[i, j, k] = null;
                    }
                }
            }
            for (int i = 0; i < Settings.map_size; i++) {
                for (int j = 0; j < Settings.map_size; j++) {
                    for (int k = 0; k < Settings.map_size; k++) {
                        IntVec3 index = new IntVec3(i, j, k) - delta;
                        if (index.x >= 0 && index.y >= 0 && index.z >= 0 && index.x < Settings.map_size && index.y < Settings.map_size && index.z < Settings.map_size) {
                            newchunks[index.x, index.y, index.z] = chunks[i, j, k];
                        }
                    }
                }
            }
            chunks = newchunks;
            chunkpos = pos;
        }

        public bool chunkOnMap(IntVec3 pos) {
            return pos.x >= 0 && pos.y >= 0 && pos.z >= 0 && pos.x < Settings.map_size && pos.y < Settings.map_size && pos.z < Settings.map_size;
        }

        public IntVec3 getChunkIndex(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            IntVec3 cindex = localindex - chunkpos + new IntVec3(Settings.offset, Settings.offset, Settings.offset);
            if (!chunkOnMap(cindex))
                return null;
            return cindex;
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
