using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Utility {
    public class MeshReader {
        int tricount = 0;
        bool hascoords = false;
        List<Vector3> vertices;
        List<Vector2> coords;
        List<int> triangles;

        public MeshReader(string[] text) {
            setup(text, new Vector2(0, 0), new Vector2(1, 1));
        }

        public MeshReader(string address) {
            setup(address, new Vector2(0, 0), new Vector2(1, 1));
        }

        public MeshReader(string address, Vector2 texdim) {
            setup(address, new Vector2(0, 0), texdim);
        }

        public MeshReader(string address, Vector2 texpos, Vector2 texdim) {
            setup(address, texpos, texdim);
        }

        void setup(string[] text, Vector2 texpos, Vector2 texdim) {
            vertices = new List<Vector3>();
            coords = new List<Vector2>();
            triangles = new List<int>();
            Vector2 ts = new Vector2(1, 1);

            Reader r = new Reader(text);

            int v = 0;
            int nv = 0;

            while (!r.EOF()) {
                string[] line = r.read().Split(' ');
                if (line.Length > 0) {
                    if (!line[0].StartsWith("#")) {
                        if (line[0].Equals("s")) {//surface
                            v = nv;
                        }
                        else if (line[0].Equals("ts")) {//texture scale
                            ts = new Vector2(float.Parse(line[1]), float.Parse(line[2]));
                        }
                        else if (line[0].Equals("v")) {//vertex
                            vertices.Add(new Vector3(float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3])));
                            nv++;
                        }
                        else if (line[0].Equals("vt")) {//vertex texture
                            coords.Add(new Vector2(float.Parse(line[1]) * texdim.x / ts.x + texpos.x, float.Parse(line[2]) * texdim.y / ts.y + texpos.y));
                            hascoords = true;
                        }
                        else if (line[0].Equals("f")) {//face
                            triangles.Add(int.Parse(line[1]) + v - 1);
                            triangles.Add(int.Parse(line[2]) + v - 1);
                            triangles.Add(int.Parse(line[3]) + v - 1);
                            tricount++;
                        }
                    }
                }
            }
        }

        void setup(string address, Vector2 texpos, Vector2 texdim) {
            vertices = new List<Vector3>();
            coords = new List<Vector2>();
            triangles = new List<int>();
            Vector2 ts = new Vector2(1, 1);

            Reader r = new Reader("Assets/Resources/Models/" + address);

            int v = 0;
            int nv = 0;

            while (!r.EOF()) {
                string[] line = r.read().Split(' ');
                if (line.Length > 0) {
                    if (!line[0].StartsWith("#")) {
                        if (line[0].Equals("s")) {//surface
                            v = nv;
                        }
                        else if (line[0].Equals("ts")) {//texture scale
                            ts = new Vector2(float.Parse(line[1]), float.Parse(line[2]));
                        }
                        else if (line[0].Equals("v")) {//vertex
                            vertices.Add(new Vector3(float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3])));
                            nv++;
                        }
                        else if (line[0].Equals("vt")) {//vertex texture
                            coords.Add(new Vector2(float.Parse(line[1]) * texdim.x / ts.x + texpos.x, float.Parse(line[2]) * texdim.y / ts.y + texpos.y));
                            hascoords = true;
                        }
                        else if (line[0].Equals("f")) {//face
                            triangles.Add(int.Parse(line[1]) + v - 1);
                            triangles.Add(int.Parse(line[2]) + v - 1);
                            triangles.Add(int.Parse(line[3]) + v - 1);
                            tricount++;
                        }
                    }
                }
            }
        }

        public Mesh getMesh() {
            Mesh mesh = new Mesh();
            if (vertices.Count >= 65536 || triangles.Count >= 65536)
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            if (hascoords) {
                mesh.uv = coords.ToArray();
            }
            return mesh;
        }
    }
}