using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class Materials {
        public Material block;

        public Materials() {
            block = new Material(Shader.Find("Standard"));
        }
    }
}
