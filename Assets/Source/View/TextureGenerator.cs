using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class TextureGenerator {
        float[,] dm;

        public TextureGenerator() {

        }

        public void generate() {
            generateDirt();
            generateCoal();
            generateTrash();
        }

        public void generateDirt() {
            int dim = 16;
            Texture2D texture = new Texture2D(dim * 3, dim * 2, TextureFormat.ARGB32, false);

            dm = new Noise(dim, 4).getData();
            for (int i = 0; i < dim; i++) {
                for (int j = 0; j < dim; j++) {
                    Color c = new Color(dm[i, j] / 10 + 0.5f, dm[i, j] / 10 + 0.25f, dm[i, j] / 10 + 0.125f);
                    texture.SetPixel(i, j, c);
                    texture.SetPixel(i + dim, j, c);
                    texture.SetPixel(i + 2 * dim, j, c);
                    texture.SetPixel(i, j + dim, c);
                    texture.SetPixel(i + dim, j + dim, c);
                    texture.SetPixel(i + 2 * dim, j + dim, c);
                }
            }
            texture.Apply();
            File.WriteAllBytes("Assets/Resources/Textures/dirt.png", texture.EncodeToPNG());
        }

        public void generateCoal() {
            int dim = 16;
            Texture2D texture = new Texture2D(dim * 3, dim * 2, TextureFormat.ARGB32, false);
            
            float[,] nc = new Noise(dim, 4).getData();
            for (int i = 0; i < dim; i++) {
                for (int j = 0; j < dim; j++) {
                    float cf = 1 - Mathf.Clamp((nc[i, j] - 0.25f) * 6, 0, 1);
                    Color c = new Color((dm[i, j] / 10 + 0.5f) * cf, (dm[i, j] / 10 + 0.25f) * cf, (dm[i, j] / 10 + 0.125f) * cf);
                    texture.SetPixel(i, j, c);
                    texture.SetPixel(i + dim, j, c);
                    texture.SetPixel(i + 2 * dim, j, c);
                    texture.SetPixel(i, j + dim, c);
                    texture.SetPixel(i + dim, j + dim, c);
                    texture.SetPixel(i + 2 * dim, j + dim, c);
                }
            }
            texture.Apply();
            File.WriteAllBytes("Assets/Resources/Textures/coal.png", texture.EncodeToPNG());
        }

        public void generateTrash() {
            int dim = 16;
            Texture2D texture = new Texture2D(dim * 3, dim * 2, TextureFormat.ARGB32, false);

            float[,] r = new Noise(dim, 4).getData();
            float[,] g = new Noise(dim, 4).getData();
            float[,] b = new Noise(dim, 4).getData();
            for (int i = 0; i < dim; i++) {
                for (int j = 0; j < dim; j++) {
                    Color c = new Color(r[i, j] / 5 + 0.375f, g[i, j] / 5 + 0.25f, b[i, j] / 5 + 0.25f);
                    texture.SetPixel(i, j, c);
                    texture.SetPixel(i + dim, j, c);
                    texture.SetPixel(i + 2 * dim, j, c);
                    texture.SetPixel(i, j + dim, c);
                    texture.SetPixel(i + dim, j + dim, c);
                    texture.SetPixel(i + 2 * dim, j + dim, c);
                }
            }
            texture.Apply();
            File.WriteAllBytes("Assets/Resources/Textures/trash.png", texture.EncodeToPNG());
        }
    }
}
