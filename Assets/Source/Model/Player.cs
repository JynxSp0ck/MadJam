using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Player {
        public Vec3 pos { get { return character.pos; } set { character.pos = value; } }
        public Vec3 vel { get { return character.vel; } set { character.vel = value; } }
        public Character character;

        public Player() {
        }
    }
}
