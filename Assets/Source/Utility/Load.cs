using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Utility {
    class Load {
        public static Texture2D texture(string path) {
            byte[] data = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(16, 16, TextureFormat.ARGB32, true, true);
            tex.LoadImage(data);
            return tex;
        }
    }
}
