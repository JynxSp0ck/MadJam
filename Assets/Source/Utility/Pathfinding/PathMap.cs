using System;
using System.Collections.Generic;

namespace Game.Utility {
    class PathMap {
        List<Node> onodes;
        List<PathNode> nodes = new List<PathNode>();
        List<PathNode> adjacents = new List<PathNode>();
        string mode = "dest";
        PathNode destination;
        float distance;
        Func<Object, float> cost;
        int loop = 0;

        public PathMap(List<Node> input) {
            List<Object> nodes = new List<object>();
            List<List<Connection>> connections = new List<List<Connection>>();
            foreach (Node node in input) {
                nodes.Add(node);
                connections.Add(node.getConnections());
            }
            onodes = input;
            for (int i = 0; i < nodes.Count; i++)
                this.nodes.Add(new PathNode(input[i]));
            for (int i = 0; i < nodes.Count; i++) {
                for (int j = 0; j < connections[i].Count; j++) {
                    this.nodes[i].connections.Add(new PathConnection(this.nodes[nodes.IndexOf(connections[i][j].node)]));
                }
            }
        }

        PathNode getNode(Node node) {
            for (int i = 0; i < nodes.Count; i++)
                if (onodes[i] == node)
                    return nodes[i];
            return null;
        }

        void setStart(Node start) {
            PathNode node = getNode(start);
            checkNode(-1, 1, node);
        }

        public List<Node> getPath(Node start, Node destination, Func<Object, float> cost) {
            setStart(start);
            this.destination = getNode(destination);
            this.cost = cost;
            mode = "dest";
            loop = 0;
            while (!iterate()) { }
            List<Node> path = new List<Node>();
            PathNode node = this.destination;
            path.Add(node.node);
            while (node.distance > 0) {
                node = getMin(node);
                path.Add(node.node);
            }
            reset();
            return path;
        }

        public float getCost(Node start, Node destination, Func<Object, float> cost) {
            setStart(start);
            this.destination = getNode(destination);
            this.cost = cost;
            mode = "dest";
            loop = 0;
            while (!iterate()) { }
            float value = this.destination.distance;
            reset();
            return value;
        }
        
        public List<Node> getNetwork(Node start, float distance, Func<Object, float> cost) {
            setStart(start);
            this.distance = distance;
            this.cost = cost;
            mode = "dist";
            loop = 0;
            while (!iterate()) { }
            List<Node> network = new List<Node>();
            for (int i = 0; i < nodes.Count; i++)
                if (nodes[i].check)
                    network.Add(onodes[i]);
            reset();
            return network;
        }

        bool iterate() {
            PathNode node = nextNode();
            loop++;
            if (node == null)
                return true;
            if (checkDestination(node))
                return true;
            if (loop > 10000) {
                return true;
            }
            foreach (PathConnection con in node.connections)
                checkNode(node.distance, cost(con.node.node), (PathNode)con.node);
            adjacents.Remove(node);
            return false;
        }

        PathNode nextNode() {
            if (adjacents.Count == 0)
                return null;
            PathNode next = adjacents[0];
            foreach (PathNode node in adjacents) {
                if (node.distance < next.distance)
                    next = node;
            }
            return next;
        }

        void checkNode(float distance, float cost, PathNode node) {
            if (cost == 0)
                return;
            if (node.check)
                return;
            if (!node.adjacent) {
                adjacents.Add(node);
                node.adjacent = true;
                node.check = true;
            }
            if (distance + cost < node.distance || node.distance < 0)
                node.distance = distance + cost;
        }

        bool checkDestination(PathNode node) {
            if (mode == "dest" && node == destination)
                return true;
            if (mode == "dist" && node.distance >= distance)
                return true;
            return false;
        }

        PathNode getMin(PathNode node) {
            PathConnection min = null;
            foreach (PathConnection con in node.connections) {
                if (con.node.check) {
                    if (min == null) {
                        if (((PathNode)con.node).distance >= 0)
                            min = con;
                    }
                    else {
                        if (((PathNode)con.node).distance < ((PathNode)min.node).distance)
                            min = con;
                    }
                }
            }
            return min.node;
        }

        public void reset() {
            adjacents = new List<PathNode>();
            for (int i = 0; i < nodes.Count; i++) {
                nodes[i].reset();
            }
        }
    }
}
