using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Player {
        public Vec3 pos = new Vec3(8, 8, 8);
        public Vec3 vel = new Vec3(0, 0, 0);
        public Character character;

        public Player() {
            character = new Character();
        }
    }
}
