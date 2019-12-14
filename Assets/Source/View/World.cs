using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class World {
        public GameObject obj;

        public RenderChunk[,,] chunks;
        public MeshGenerator generator;
        public Texture2D spritemap;

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
            loadTextures();
            generator = new MeshGenerator();
            Mesh m = new MeshReader("cube.obj").getMesh();
            Vector3[] v = m.vertices;
            for (int i = 0; i < v.Length; i++) {
                MeshGenerator.cube.vertices.Add(new Vec3(v[i].x, v[i].y, v[i].z));
            }
            Vector2[] c = m.uv;
            for (int i = 0; i < v.Length; i++) {
                MeshGenerator.cube.coords.Add(new Vec2(c[i].x, c[i].y));
            }
            MeshGenerator.cube.triangles.AddRange(m.triangles);
        }

        public void loadTextures() {
            spritemap = new TextureLoader().load();
            Client.view.materials.setSpriteMap(spritemap);
        }

        public void setChunks() {
            for (int i = -Settings.load_distance; i <= Settings.load_distance; i++) {
                for (int j = -Settings.load_distance; j <= Settings.load_distance; j++) {
                    for (int k = -Settings.load_distance; k <= Settings.load_distance; k++) {
                        if (chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset] == null) {
                            Chunk chunk = Client.model.map.getChunk((new IntVec3(i, j, k) + Client.model.map.chunkpos) * Settings.chunk_size);
                            if (chunk != null && chunk.loaded) {
                                chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset] = new RenderChunk(chunk);
                                chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset].generate();
                            }
                        }
                        else if (chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset].old) {
                            chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset].generate();
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
                        IntVec3 index = new IntVec3(i, j, k) - delta;
                        if (index.x >= 0 && index.y >= 0 && index.z >= 0 && index.x < Settings.map_size && index.y < Settings.map_size && index.z < Settings.map_size) {
                            newchunks[index.x, index.y, index.z] = chunks[i, j, k];
                        }
                        else if (chunks[i, j, k] != null) {
                            chunks[i, j, k].destroy();
                        }
                    }
                }
            }
            chunks = newchunks;
        }

        public void update() {
            setChunks();
            generator.run();
        }
    }
}
