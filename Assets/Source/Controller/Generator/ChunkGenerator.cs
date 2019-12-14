﻿using System;
using System.Collections.Generic;
using System.Threading;
using Game.Model;
using Game.Utility;

namespace Game.Controller {
    class ChunkGenerator {
        List<ChunkTask> tasks = new List<ChunkTask>();

        public Thread thread = null;

        public ChunkGenerator() {

        }

        public void run() {
            generateChunks();
            if (tasks.Count == 0)
                return;
            if (!tasks[0].finished)
                return;
            tasks.RemoveAt(0);
            if (tasks.Count == 0)
                return;
            thread = new Thread(new ThreadStart(tasks[0].start));
            try {
                thread.Start();
            }
            catch (ThreadStateException e) {
                UnityEngine.Debug.Log("Thread Error" + e.ToString());
            }
        }

        public void generateChunks() {
            for (int i = -Settings.load_distance; i <= Settings.load_distance; i++) {
                for (int j = -Settings.load_distance; j <= Settings.load_distance; j++) {
                    for (int k = -Settings.load_distance; k <= Settings.load_distance; k++) {
                        Chunk chunk = Client.model.map.chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset];
                        if (chunk == null) {
                            chunk = new Chunk(new IntVec3(i, j, k) + Client.model.map.chunkpos);
                            Client.model.map.chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset] = chunk;
                            add(chunk);
                        }
                    }
                }
            }
        }

        public void add(Chunk chunk) {
            ChunkTask task = new ChunkTask(chunk);
            tasks.Add(task);
            if (tasks[0] != task)
                return;
            thread = new Thread(new ThreadStart(task.start));
            try {
                thread.Start();
            }
            catch (ThreadStateException e) {
                UnityEngine.Debug.Log("Thread Error" + e.ToString());
            }
        }
    }
}