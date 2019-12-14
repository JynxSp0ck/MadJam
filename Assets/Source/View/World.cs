using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class World {
        public GameObject obj;

        public RenderChunk[,,] chunks;

        public World() {
            obj = Find.name(Client.view.obj, "Chunks");
            chunks = new RenderChunk[Settings.map_size, Settings.map_size, Settings.map_size];
            for (int i = 0; i < Settings.map_size; i++) {
                for (int j = 0; j < Settings.map_size; j++) {
                    for (int k = 0; k < Settings.map_size; k++) {
                        chunks[i, j, k] = null;
                    }
                }
            }
        }

        public void setChunks() {
            for (int i = -Settings.load_distance; i <= Settings.load_distance; i++) {
                for (int j = -Settings.load_distance; j <= Settings.load_distance; j++) {
                    for (int k = -Settings.load_distance; k <= Settings.load_distance; k++) {
                        chunks[i + Settings.memory_distance, j + Settings.memory_distance, k + Settings.memory_distance] = new RenderChunk(Client.model.map.getChunk(new IntVec3(i, j, k) * Settings.chunk_size));
                    }
                }
            }
        }
    }
}
