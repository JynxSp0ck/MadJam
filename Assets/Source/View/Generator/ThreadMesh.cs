﻿using System;
using System.Collections.Generic;

namespace Game.View {
    class ThreadMesh {
        public List<int> triangles = new List<int>();
        public List<Game.Utility.Vec3> vertices = new List<Game.Utility.Vec3>();
        public List<Game.Utility.Vec2> coords = new List<Game.Utility.Vec2>();

        public ThreadMesh() {

        }
    }
}
