using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class BlockType {
        public static List<BlockType> types = new List<BlockType>();

        public static void add(string name, bool transparent) {
            types.Add(new BlockType(name, types.Count, transparent));
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

        public BlockType(string name, int index, bool transparent) {
            this.name = name;
            this.index = index;
            this.transparent = transparent;
        }
    }
}
