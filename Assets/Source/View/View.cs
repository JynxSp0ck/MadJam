using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class View {
        public GameObject obj;

        public Camera camera;
        public World world;
        public Materials materials;

        public Vec3[] cubeverts;
        public int[] cubetris;

        public View() {
            Mesh m = Find.name("Cube").GetComponent<MeshFilter>().mesh;
            obj = Find.name("World");
            Vector3[] v = m.vertices;
            cubeverts = new Vec3[v.Length];
            for (int i = 0; i < v.Length; i++) {
                cubeverts[i] = new Vec3(v[i].x + 0.5f, v[i].y + 0.5f, v[i].z + 0.5f);
            }
            cubetris = m.triangles;
        }

        public void init() {
            materials = new Materials();
            camera = new Camera();
            world = new World();
            world.setChunks();
        }

        public void update() {
            camera.update();
        }
    }
}
