﻿using System;
using System.Collections.Generic;

namespace Game.View {
    class MeshTask {
        public bool finished = false;
        public Game.Model.Chunk chunk;
        public ThreadMesh mesh = new ThreadMesh();

        public MeshTask(Game.Model.Chunk chunk) {
            this.chunk = chunk;
        }

        public void start() {
            for (int i = 0; i < Game.Model.Settings.chunk_size; i++) {
                for (int j = 0; j < Game.Model.Settings.chunk_size; j++) {
                    for (int k = 0; k < Game.Model.Settings.chunk_size; k++) {
                        setBlock(new Game.Utility.IntVec3(i, j, k));
                    }
                }
            }
            finished = true;
        }

        void setBlock(Game.Utility.IntVec3 index) {
            Game.Model.Block block = Client.model.map.getBlock(index + chunk.pos * Game.Model.Settings.chunk_size);
            Game.Utility.Vec2 pos = block.type.rectpos;
            Game.Utility.Vec2 dim = block.type.rectdim;
            if (block == null)
                return;
            if (block.hidden)
                return;
            if (block.type.transparent)
                return;
            int offset = mesh.vertices.Count;

            for (int i = 0; i < MeshGenerator.cube.vertices.Count; i++)
                mesh.vertices.Add(MeshGenerator.cube.vertices[i] + index.Float());

            for (int i = 0; i < MeshGenerator.cube.coords.Count; i++)
                mesh.coords.Add(new Game.Utility.Vec2(MeshGenerator.cube.coords[i].x * dim.x, MeshGenerator.cube.coords[i].y * dim.y) + pos);

            for (int i = 0; i < MeshGenerator.cube.triangles.Count; i++)
                mesh.triangles.Add(MeshGenerator.cube.triangles[i] + offset);
        }
    }
}
