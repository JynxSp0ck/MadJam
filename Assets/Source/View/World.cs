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
                        if (chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset] == null) {
                            chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset] = new RenderChunk(Client.model.map.getChunk(new IntVec3(i, j, k) * Settings.chunk_size));
                        }
                    }
                }
            }
        }

        public void move(IntVec3 delta) {
            RenderChunk[,,] newchunks = new RenderChunk[Settings.map_size, Settings.map_size, Settings.map_size];
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
                        IntVec3 oldindex = new IntVec3(i, j, k) + delta;
                        if (oldindex.x >= 0 && oldindex.y >= 0 && oldindex.z >= 0 && oldindex.x < Settings.map_size && oldindex.y < Settings.map_size && oldindex.z < Settings.map_size) {
                            newchunks[i, j, k] = chunks[oldindex.x, oldindex.y, oldindex.z];
                        }
                        else {
                            newchunks[i, j, k].destroy();
                        }
                    }
                }
            }
            chunks = newchunks;
            setChunks();
        }
    }
}
