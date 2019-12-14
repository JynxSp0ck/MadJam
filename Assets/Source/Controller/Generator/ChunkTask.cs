using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller {
    class ChunkTask {
        public bool finished = false;
        public string type;
        public Game.Model.Chunk chunk;

        public ChunkTask(string type, Game.Model.Chunk chunk) {
            this.type = type;
            this.chunk = chunk;
        }

        public void start() {
            if (type == "generate")
                generate();
            if (type == "save")
                save();
            if (type == "load")
                load();
            finished = true;
        }

        public void save() {
            chunk.saved = true;
        }

        public void load() {
            chunk.loaded = true;
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
            return pos.y >= Client.random.Next(-3, 0) ? new Game.Model.Block("air") : new Game.Model.Block("dirt");
        }
    }
}
