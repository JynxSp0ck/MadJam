using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Map {
        public string name;
        public List<Chunk> chunks;

        public Map(string name) {
            this.name = name;
            chunks = new List<Chunk>();
        }
        
        public Chunk getChunk(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            foreach (Chunk chunk in chunks)
                if (chunk.pos == localindex)
                    return chunk;
            return null;
        }

        public List<Chunk> createChunks(Vec3 pos, int dist) {
            return createChunks((pos / Settings.chunk_size).Floor(), dist);
        }

        public List<Chunk> createChunks(IntVec3 pos, int dist) {
            List<Chunk> chunks = new List<Chunk>();
            for (int i = -dist; i <= dist; i++) {
                for (int j = -dist; j <= dist; j++) {
                    for (int k = -dist; k <= dist; k++) {
                        IntVec3 index = pos + new IntVec3(i, j, k);
                        Chunk chunk = getChunk(index * Settings.chunk_size);
                        if (chunk == null) {
                            chunk = new Chunk(index);
                            this.chunks.Add(chunk);
                        }
                        chunks.Add(chunk);
                    }
                }
            }
            return chunks;
        }

        public List<Chunk> getChunks(Vec3 pos, int dist) {
            return getChunks((pos / Settings.chunk_size).Floor(), dist);
        }

        public List<Chunk> getChunks(IntVec3 pos, int dist) {
            List<Chunk> chunks = new List<Chunk>();
            for (int i = -dist; i <= dist; i++) {
                for (int j = -dist; j <= dist; j++) {
                    for (int k = -dist; k <= dist; k++) {
                        IntVec3 index = pos + new IntVec3(i, j, k);
                        Chunk chunk = getChunk(index * Settings.chunk_size);
                        if (chunk != null)
                            chunks.Add(chunk);
                    }
                }
            }
            return chunks;
        }

        public IntVec3 getBlockIndex(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            Chunk chunk = getChunk(pos);
            if (chunk == null)
                return null;
            if (!chunk.loaded)
                return null;
            return pos - localindex * Settings.chunk_size;
        }

        public Block getBlock(IntVec3 pos) {
            if (pos == null)
                return new Block("error");
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            Chunk chunk = getChunk(pos);
            if (chunk == null)
                return new Block("error");
            if (!chunk.loaded)
                return new Block("error");
            IntVec3 bindex = pos - localindex * Settings.chunk_size;
            return chunk.blocks[bindex.x, bindex.y, bindex.z];
        }
    }
}
