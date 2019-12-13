using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Utility;

namespace Game.Controller {
    class Controller {
        PlayerController playcon;

        public Controller() {

        }

        public void init() {
            playcon = new PlayerController();
        }

        public void update() {
            playcon.update();
        }
    }
}
