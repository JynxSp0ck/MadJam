﻿using System;
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
            for (int i = -Settings.load_distance; i <= Settings.load_distance; i++) {
                for (int j = -Settings.load_distance; j <= Settings.load_distance; j++) {
                    for (int k = -Settings.load_distance; k <= Settings.load_distance; k++) {
                        chunks[i + Settings.memory_distance, j + Settings.memory_distance, k + Settings.memory_distance] = new Chunk(new IntVec3(i, j, k));
                    }
                }
            }
        }

        public Chunk getChunk(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            IntVec3 cindex = localindex - chunkpos;
            if (cindex.x < -Settings.memory_distance || cindex.y < -Settings.memory_distance || cindex.z < -Settings.memory_distance || cindex.x > Settings.memory_distance || cindex.y > Settings.memory_distance || cindex.z > Settings.memory_distance)
                return null;
            cindex += new IntVec3(Settings.memory_distance, Settings.memory_distance, Settings.memory_distance);
            return chunks[cindex.x, cindex.y, cindex.z];
        }

        public Block getBlock(IntVec3 pos) {
            IntVec3 localindex = (pos.Float() / Settings.chunk_size).Floor();
            Chunk chunk = getChunk(pos);
            if (chunk == null)
                return null;
            IntVec3 bindex = pos - localindex * Settings.chunk_size;
            return chunk.blocks[bindex.x, bindex.y, bindex.z];
        }
    }
}