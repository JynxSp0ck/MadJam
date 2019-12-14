using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller {
    class ChunkTask {
        public bool finished = false;
        public Game.Model.Chunk chunk;

        public ChunkTask(Game.Model.Chunk chunk) {
            this.chunk = chunk;
        }

        public void start() {
            for (int i = 0; i < Game.Model.Settings.chunk_size; i++) {
                for (int j = 0; j < Game.Model.Settings.chunk_size; j++) {
                    for (int k = 0; k < Game.Model.Settings.chunk_size; k++) {
                        chunk.blocks[i, j, k] = setBlock(new Game.Utility.IntVec3(i, j, k));
                    }
                }
            }
            chunk.generated = true;
            finished = true;
        }

        Game.Model.Block setBlock(Game.Utility.IntVec3 index) {
            Game.Utility.IntVec3 pos = index + chunk.pos * Game.Model.Settings.chunk_size;
            return pos.y >= Client.random.Next(-3, 0) ? new Game.Model.Block("air") : new Game.Model.Block("dirt");
        }
    }
}
