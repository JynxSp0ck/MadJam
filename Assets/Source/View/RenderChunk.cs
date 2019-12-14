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
            IntVec3 pos = chunk.index;
            obj = new GameObject("Chunk " + (pos.x < 0 ? "N" : "P") + Mathf.Abs(pos.x) + (pos.y < 0 ? "N" : "P") + Mathf.Abs(pos.y) + (pos.z < 0 ? "N" : "P") + Mathf.Abs(pos.z));
            obj.transform.position = Conv.ert((pos * Settings.chunk_size).Float());
            obj.transform.parent = Client.view.world.obj.transform;
            mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            filter = obj.AddComponent<MeshFilter>();
            renderer = obj.AddComponent<MeshRenderer>();
            renderer.material = Client.view.materials.block;
            collider = obj.AddComponent<MeshCollider>();
            setMesh();
        }

        void setMesh() {
            vertices = new List<Vector3>();
            triangles = new List<int>();

            for (int i = 0; i < Settings.chunk_size; i++) {
                for (int j = 0; j < Settings.chunk_size; j++) {
                    for (int k = 0; k < Settings.chunk_size; k++) {
                        setBlock(new IntVec3(i, j, k));
                    }
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            filter.sharedMesh = mesh;
            collider.sharedMesh = mesh;
        }

        void setBlock(IntVec3 index) {
            Block block = Client.model.map.getBlock(index + chunk.index * Settings.chunk_size);
            if (block == null)
                return;
            if (block.hidden)
                return;
            if (block.type.transparent)
                return;
            int offset = vertices.Count;
            for (int i = 0; i < Client.view.cubeverts.Length; i++)
                vertices.Add(Conv.ert(Client.view.cubeverts[i] + index.Float()));
            for (int i = 0; i < Client.view.cubetris.Length; i++)
                triangles.Add(Client.view.cubetris[i] + offset);
        }
    }
}
