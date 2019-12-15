using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class BlockType {
        public static List<BlockType> types = new List<BlockType>();

        public static void add(string name, bool transparent, bool mineable, int stacklimit) {
            types.Add(new BlockType(name, types.Count, transparent, mineable, stacklimit));
        }

        public static BlockType get(string name) {
            foreach (BlockType type in types) {
                if (name == type.name) {
                    return type;
                }
            }
            return null;
        }

        public static BlockType get(int index) {
            if (index >= 0 && index < types.Count)
                return types[index];
            return null;
        }

        public string name;
        public int index;
        public bool transparent;
        public bool mineable;
        public int stacklimit;
        public Vec2 rectpos;
        public Vec2 rectdim;

        public BlockType(string name, int index, bool transparent, bool mineable, int stacklimit) {
            this.name = name;
            this.index = index;
            this.transparent = transparent;
            this.mineable = mineable;
            this.stacklimit = stacklimit;
        }
    }
}
