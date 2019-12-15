using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Character {
        public Inventory inventory;
        public Vec3 pos = new Vec3(8, 8, 8);
        public Vec3 vel = new Vec3(0, 0, 0);
        public float dig = 0;
        public float digspeed = 0.02f;
        public float range = 2.5f;

        public Character() {
            inventory = new Inventory();
        }
    }
}
