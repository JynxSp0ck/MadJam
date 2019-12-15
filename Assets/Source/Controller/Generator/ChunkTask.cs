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

        public ChunkTask(string type, Game.Model.Chunk chunk) {
            this.type = type;
            this.chunk = chunk;
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
            float cc = -0.002f * pos.y;
            if (cc > 0.1)
                cc = 0.1f;
            float tc = pos.y * 0.01f + 0.8f;
            return pos.y >= Client.random.Next(-3, 0) ? new Game.Model.Block("air") : (Client.random.NextDouble() < cc ? new Game.Model.Block("coal") : (Client.random.NextDouble() < tc ? new Game.Model.Block("trash") : new Game.Model.Block("dirt")));
        }
    }
}
