using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Controller {
    class ChunkTask {
        public static int N = 0;
        public bool started = false;
        public bool finished = false;
        public string type;
        public Game.Model.Chunk chunk;
        public Game.Model.Chunk[,,] chunks;

        public ChunkTask(string type, Game.Model.Chunk chunk) {
            this.type = type;
            this.chunk = chunk;
            chunks = new Model.Chunk[Model.Settings.data_distance * 2 + 1, Model.Settings.data_distance * 2 + 1, Model.Settings.data_distance * 2 + 1];
            if (type == "generate") {
                for (int i = -Model.Settings.data_distance; i <= Model.Settings.data_distance; i++) {
                    for (int j = -Model.Settings.data_distance; j <= Model.Settings.data_distance; j++) {
                        for (int k = -Model.Settings.data_distance; k <= Model.Settings.data_distance; k++) {
                            chunks[i + Model.Settings.data_distance, j + Model.Settings.data_distance, k + Model.Settings.data_distance] = Client.model.map.getChunk((chunk.pos + new Utility.IntVec3(i, j, k)) * Model.Settings.chunk_size);
                        }
                    }
                }
            }
        }

        public void start() {
            N++;
            if (type == "generate")
                generate();
            if (type == "save")
                save();
            if (type == "load")
                load();
            finished = true;
            N--;
        }

        public void save() {
            List<string> lines = new List<string>();
            for (int i = 0; i < Game.Model.Settings.chunk_size; i++) {
                for (int j = 0; j < Game.Model.Settings.chunk_size; j++) {
                    for (int k = 0; k < Game.Model.Settings.chunk_size; k++) {
                        lines.Add("" + chunk.blocks[i, j, k].type.index);
                    }
                }
            }
            File.WriteAllLines("Maps/" + Client.model.map.name + "/" + chunk.name, lines);
            chunk.saved = true;
        }

        public void load() {
            Game.Utility.Reader r = new Utility.Reader("Maps/" + Client.model.map.name + "/" + chunk.name);
            for (int i = 0; i < Game.Model.Settings.chunk_size; i++) {
                for (int j = 0; j < Game.Model.Settings.chunk_size; j++) {
                    for (int k = 0; k < Game.Model.Settings.chunk_size; k++) {
                        chunk.blocks[i, j, k] = new Model.Block(Model.BlockType.get(int.Parse(r.read())));
                    }
                }
            }
            chunk.loaded = true;
            chunk.saved = true;
        }

        public void generate() {
            for (int i = 0; i < Game.Model.Settings.chunk_size; i++) {
                for (int j = 0; j < Game.Model.Settings.chunk_size; j++) {
                    for (int k = 0; k < Game.Model.Settings.chunk_size; k++) {
                        chunk.blocks[i, j, k] = genBlock(new Game.Utility.IntVec3(i, j, k));
                    }
                }
            }
            chunk.loaded = true;
        }

        Game.Model.Block genBlock(Game.Utility.IntVec3 index) {
            Game.Utility.IntVec3 pos = index + chunk.pos * Game.Model.Settings.chunk_size;
            float height = getHeight(index);
            float cc = -0.002f * pos.y;
            if (cc > 0.1)
                cc = 0.1f;
            float tc = pos.y * 0.01f + 0.8f;
            return pos.y >= height ? new Game.Model.Block("air") : (Client.random.NextDouble() < cc ? new Game.Model.Block("coal") : (Client.random.NextDouble() < tc ? new Game.Model.Block("trash") : new Game.Model.Block("dirt")));
        }

        float getHeight(Game.Utility.IntVec3 index) {
            float[,] hm = new float[2, 2];
            hm[0, 0] = chunks[Model.Settings.data_distance, Model.Settings.data_distance, Model.Settings.data_distance].surf[0];
            hm[0, 1] = chunks[Model.Settings.data_distance, Model.Settings.data_distance, Model.Settings.data_distance + 1].surf[0];
            hm[1, 0] = chunks[Model.Settings.data_distance + 1, Model.Settings.data_distance, Model.Settings.data_distance].surf[0];
            hm[1, 1] = chunks[Model.Settings.data_distance + 1, Model.Settings.data_distance, Model.Settings.data_distance + 1].surf[0];
            float h = Utility.Func.lerp(new Utility.Vec2(index.x, index.z) / Model.Settings.chunk_size, hm);
            if (h > 0)
                h *= 10;
            else
                h *= 2;
            return h;
        }
    }
}
