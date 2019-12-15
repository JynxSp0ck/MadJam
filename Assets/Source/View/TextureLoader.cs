using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class TextureLoader {
        Texture2D map;

        public TextureLoader() {

        }

        public Texture2D load() {
            map = new Texture2D(16, 16, TextureFormat.ARGB32, false);
            List<Texture2D> sprites = new List<Texture2D>();
            for (int i = 0; i < BlockType.types.Count; i++)
                sprites.Add(Load.texture("Assets/Resources/Textures/" + BlockType.types[i].name + ".png"));
            Rect[] rects = map.PackTextures(sprites.ToArray(), 1);
            map.mipMapBias = -10;
            for (int i = 0; i < rects.Length; i++) {
                //suppress error messages
                if (rects[i].y > 0)
                    map.SetPixels((int)(rects[i].x * map.width), (int)(rects[i].y * map.height) - 1, (int)(rects[i].width * map.width), 1, map.GetPixels((int)(rects[i].x * map.width), (int)(rects[i].y * map.height), (int)(rects[i].width * map.width), 1));
                if (rects[i].yMax < 1)
                    map.SetPixels((int)(rects[i].x * map.width), (int)(rects[i].yMax * map.height), (int)(rects[i].width * map.width), 1, map.GetPixels((int)(rects[i].x * map.width), (int)(rects[i].yMax * map.height) - 1, (int)(rects[i].width * map.width), 1));
                if (rects[i].x > 0)
                    map.SetPixels((int)(rects[i].x * map.width) - 1, (int)(rects[i].y * map.height), 1, (int)(rects[i].height * map.height), map.GetPixels((int)(rects[i].x * map.width), (int)(rects[i].y * map.height), 1, (int)(rects[i].height * map.height)));
                if (rects[i].xMax < 1)
                    map.SetPixels((int)(rects[i].xMax * map.width), (int)(rects[i].y * map.height), 1, (int)(rects[i].height * map.height), map.GetPixels((int)(rects[i].xMax * map.width) - 1, (int)(rects[i].y * map.height), 1, (int)(rects[i].height * map.height)));
                map.SetPixel((int)(rects[i].x * map.width) - 1, (int)(rects[i].y * map.height) - 1, map.GetPixel((int)(rects[i].x * map.width), (int)(rects[i].y * map.height)));
                map.SetPixel((int)(rects[i].xMax * map.width), (int)(rects[i].y * map.height) - 1, map.GetPixel((int)(rects[i].xMax * map.width) - 1, (int)(rects[i].y * map.height)));
                map.SetPixel((int)(rects[i].x * map.width) - 1, (int)(rects[i].yMax * map.height), map.GetPixel((int)(rects[i].x * map.width), (int)(rects[i].yMax * map.height) - 1));
                map.SetPixel((int)(rects[i].xMax * map.width), (int)(rects[i].yMax * map.height), map.GetPixel((int)(rects[i].xMax * map.width) - 1, (int)(rects[i].yMax * map.height) - 1));
            }
            for (int i = 0; i < BlockType.types.Count; i++) {
                BlockType.types[i].rectpos = new Vec2(rects[i].x, rects[i].y);
                BlockType.types[i].rectdim = new Vec2(rects[i].width, rects[i].height);
            }
            map.wrapMode = TextureWrapMode.Clamp;
            map.filterMode = FilterMode.Point;
            map.Apply();
            File.WriteAllBytes("Assets/Resources/Textures/spritesheet.png", map.EncodeToPNG());
            return map;
        }
    }
}
