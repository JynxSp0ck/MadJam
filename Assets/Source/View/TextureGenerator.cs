using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class TextureGenerator {
        public TextureGenerator() {

        }

        public void generate() {
            int dim = 16;
            Texture2D texture = new Texture2D(dim * 3, dim * 2, TextureFormat.ARGB32, false);

            float[,] n = new Noise(dim, 4).getData();
            for (int i = 0; i < dim; i++) {
                for (int j = 0; j < dim; j++) {
                    Color c = new Color(n[i, j] / 10 + 0.5f, n[i, j] / 10 + 0.25f, n[i, j] / 10 + 0.125f);
                    texture.SetPixel(i, j, c);
                    texture.SetPixel(i + dim, j, c);
                    texture.SetPixel(i + 2 * dim, j, c);
                    texture.SetPixel(i, j + dim, c);
                    texture.SetPixel(i + dim, j + dim, c);
                    texture.SetPixel(i + 2 * dim, j + dim, c);
                }
            }
            texture.Apply();
            File.WriteAllBytes("Assets/Textures/dirt.png", texture.EncodeToPNG());
        }
    }
}
