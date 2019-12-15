using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Game.Model;
using Game.Utility;

namespace Game.Controller {
    class ChunkGenerator {
        List<ChunkTask> loadqueue = new List<ChunkTask>();
        List<ChunkTask> savequeue = new List<ChunkTask>();

        public Thread thread = null;

        public ChunkGenerator() {
            Directory.CreateDirectory("Maps/" + Client.model.map.name);
        }

        public void run() {
            generateChunks();
            saveChunks();
            if (loadqueue.Count > 0)
                if (loadqueue[0].finished)
                    loadqueue.RemoveAt(0);
            if (savequeue.Count > 0)
                if (savequeue[0].finished)
                    savequeue.RemoveAt(0);
            startthread();
        }

        public void generateChunks() {
            for (int i = -Settings.load_distance; i <= Settings.load_distance; i++) {
                for (int j = -Settings.load_distance; j <= Settings.load_distance; j++) {
                    for (int k = -Settings.load_distance; k <= Settings.load_distance; k++) {
                        Chunk chunk = Client.model.map.chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset];
                        if (chunk == null) {
                            chunk = new Chunk(new IntVec3(i, j, k) + Client.model.map.chunkpos);
                            Client.model.map.chunks[i + Settings.offset, j + Settings.offset, k + Settings.offset] = chunk;
                            add(File.Exists("Maps/" + Client.model.map.name + "/" + chunk.name) ? "load" : "generate", chunk);
                        }
                    }
                }
            }
        }

        public void saveChunks() {
            for (int i = 0; i < Settings.map_size; i++) {
                for (int j = 0; j < Settings.map_size; j++) {
                    for (int k = 0; k < Settings.map_size; k++) {
                        Chunk chunk = Client.model.map.chunks[i, j, k];
                        if (chunk != null && chunk.loaded && !chunk.saved) {
                            add("save", chunk);
                        }
                    }
                }
            }
        }

        public void add(string type, Chunk chunk) {
            ChunkTask task = new ChunkTask(type, chunk);
            if (type == "save")
                savequeue.Add(task);
            else
                loadqueue.Add(task);
            startthread();
        }

        void startthread() {
            bool running = false;
            if (loadqueue.Count > 0)
                if (loadqueue[0].started)
                    running = true;
            if (savequeue.Count > 0)
                if (savequeue[0].started)
                    running = true;
            if (running)
                return;
            if (loadqueue.Count > 0)
                startload();
            else if (savequeue.Count > 0)
                startsave();
        }

        void startload() {
            if (loadqueue.Count == 0)
                return;
            ChunkTask task = loadqueue[0];
            thread = new Thread(new ThreadStart(task.start));
            task.started = true;
            try {
                thread.Start();
            }
            catch (ThreadStateException e) {
                UnityEngine.Debug.Log("Thread Error" + e.ToString());
            }
        }

        void startsave() {
            if (savequeue.Count == 0)
                return;
            ChunkTask task = savequeue[0];
            thread = new Thread(new ThreadStart(task.start));
            task.started = true;
            try {
                thread.Start();
            }
            catch (ThreadStateException e) {
                UnityEngine.Debug.Log("Thread Error" + e.ToString());
            }
        }
    }
}
