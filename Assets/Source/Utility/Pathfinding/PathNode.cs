using System;
using System.Collections.Generic;

namespace Game.Utility {
    class PathNode {
        public Node node;
        public List<PathConnection> connections = new List<PathConnection>();
        public float distance = -1;
        public bool check = false;
        public bool adjacent = false;

        public PathNode(Node node) {
            this.node = node;
        }

        public void reset() {
            distance = -1;
            check = false;
            adjacent = false;
        }
    }
}
