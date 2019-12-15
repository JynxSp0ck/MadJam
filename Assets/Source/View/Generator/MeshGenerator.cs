using System;
using System.Collections.Generic;
using System.Threading;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class MeshGenerator {
        public static ThreadMesh cube = new ThreadMesh();

        List<MeshTask> tasks = new List<MeshTask>();

        public Thread thread = null;

        public MeshGenerator() {
        }

        public void run() {
            if (tasks.Count == 0)
                return;
            if (!tasks[0].finished)
                return;
            process(tasks[0]);
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

        void process(MeshTask task) {
            IntVec3 pos = Client.model.map.getChunkIndex(task.chunk.pos * Settings.chunk_size);
            if (pos == null)
                return;
            RenderChunk rc = Client.view.world.chunks[pos.x, pos.y, pos.z];
            rc.setMesh(task.mesh);
        }

        public void add(MeshTask task) {
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

        public void priority(MeshTask task) {
            if (tasks.Count <= 1) {
                add(task);
                return;
            }
            tasks.Insert(1, task);
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
