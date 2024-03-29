﻿using System;
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
            Client.model.map.createChunks(Client.model.player.pos, Settings.load_distance + Settings.data_distance);
            List<Chunk> chunks = Client.model.map.createChunks(Client.model.player.pos, Settings.load_distance);
            foreach (Chunk chunk in chunks) {
                if (!chunk.started) {
                    chunk.started = true;
                    add(File.Exists("Maps/" + Client.model.map.name + "/" + chunk.name) ? "load" : "generate", chunk);
                }
            }
            Client.model.map.chunks = Client.model.map.getChunks(Client.model.player.pos, Settings.offset);
        }

        public void saveChunks() {
            foreach (Chunk chunk in Client.model.map.chunks) {
                if (chunk != null && chunk.loaded && !chunk.saved) {
                    add("save", chunk);
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
