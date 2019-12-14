using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class RenderChunk {
        Chunk chunk;

        GameObject obj;
        Mesh mesh;
        MeshFilter filter;
        MeshRenderer renderer;
        MeshCollider collider;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        public RenderChunk(Chunk chunk) {
            this.chunk = chunk;
            setChunk();
        }

        void setChunk() {
            if (chunk == null)
                return;
            IntVec3 pos = chunk.pos;
            obj = new GameObject("Chunk " + (pos.x < 0 ? "N" : "P") + Mathf.Abs(pos.x) + (pos.y < 0 ? "N" : "P") + Mathf.Abs(pos.y) + (pos.z < 0 ? "N" : "P") + Mathf.Abs(pos.z));
            obj.transform.position = Conv.ert((pos * Settings.chunk_size).Float());
            obj.transform.parent = Client.view.world.obj.transform;
            obj.SetActive(false);
            mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            filter = obj.AddComponent<MeshFilter>();
            renderer = obj.AddComponent<MeshRenderer>();
            renderer.material = Client.view.materials.block;
            collider = obj.AddComponent<MeshCollider>();
            generate();
        }
        
        public void generate() {
            Client.view.world.generator.add(new MeshTask(chunk));
        }
        
        public void setMesh(ThreadMesh tm) {
            vertices = new List<Vector3>();
            triangles = tm.triangles;

            for (int i = 0; i < tm.vertices.Count; i++)
                vertices.Add(Conv.ert(tm.vertices[i]));

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            filter.sharedMesh = mesh;
            collider.sharedMesh = mesh;
            obj.SetActive(true);//lag
        }

        public void destroy() {
            GameObject.Destroy(obj);
        }
    }
}
