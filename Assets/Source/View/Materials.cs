using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class Materials {
        public Material block;
        public int texcode;

        public Materials() {
            block = new Material(Shader.Find("BlockShader"));
        }

        public void setSpriteMap(Texture2D spritemap) {
            block.SetTexture("_Texture", spritemap);
            texcode = spritemap.GetHashCode();
        }
    }
}
