using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Utility {
    class Noise {
        static ComputeShader cs;
        static int wnk;//white noise kernel
        static int elk;//enlarge kernel
        static int grk;//enlarge kernel
        static int adk;//add kernel
        static int mlk;//mul kernel
        static int ank;//add n kernel
        static int mnk;//mul n kernel
        static int clk;//clamp kernel
        static int abk;//abs kernel
        static int mak;//max kernel
        static int mik;//min kernel

        public ComputeBuffer cb;
        public int size;

        public Noise(Noise n) {
            size = n.size;
            cb = new ComputeBuffer(size * size, 32);
            cs.SetBuffer(adk, "_Input", n.cb);
            cs.SetBuffer(adk, "_Result", cb);
            cs.Dispatch(adk, cb.count / 64, 1, 1);
        }

        public Noise(int size) {
            this.size = size;
            cb = new ComputeBuffer(size * size, 32);
        }

        public Noise(int size, int period) {
            this.size = size;
            cb = pinkNoise(size, period);
        }

        public Noise(int size, int period, float magnitude) {
            this.size = size;
            cb = pinkNoise(size, period, magnitude / (1 - 0.5f / period));
        }

        public Noise add(Noise n) {
            cb = add(cb, n.cb);
            return this;
        }

        public Noise mul(Noise n) {
            cb = mul(cb, n.cb);
            return this;
        }

        public Noise add(float n) {
            cb = add(cb, n);
            return this;
        }

        public Noise mul(float n) {
            cb = mul(cb, n);
            return this;
        }

        public Noise clamp(float l, float h) {
            cb = clamp(cb, l, h);
            return this;
        }

        public Noise abs() {
            cb = abs(cb);
            return this;
        }

        public Noise max(Noise n) {
            cb = max(cb, n.cb);
            return this;
        }

        public Noise min(Noise n) {
            cb = min(cb, n.cb);
            return this;
        }

        public Noise enlarge(int f) {
            cb = enlarge(cb, 1, f);
            size *= f;
            return this;
        }

        public static void init(ComputeShader s) {
            cs = s;
            wnk = cs.FindKernel("WhiteNoise");
            elk = cs.FindKernel("Enlarge");
            grk = cs.FindKernel("Grey");
            adk = cs.FindKernel("Add");
            mlk = cs.FindKernel("Mul");
            ank = cs.FindKernel("AddN");
            mnk = cs.FindKernel("MulN");
            clk = cs.FindKernel("Clamp");
            abk = cs.FindKernel("Abs");
            mak = cs.FindKernel("Max");
            mik = cs.FindKernel("Min");
        }

        static ComputeBuffer pinkNoise(int size, int period) {
            return pinkNoise(size, period, 1);
        }

        static ComputeBuffer pinkNoise(int size, int period, float magnitude) {
            if (period <= 1) {
                return mul(whiteNoise(size, 1), magnitude / 2);
            }
            else {
                return add(perlinNoise(size, period, magnitude / 2), pinkNoise(size, period / 2, magnitude / 2));
            }
        }

        static ComputeBuffer perlinNoise(int size, int period, float magnitude) {
            return mul(enlarge(whiteNoise(size / period, 3), size / period, period), magnitude);
        }

        static ComputeBuffer whiteNoise(int size, int perpixel) {
            int count = size * size * perpixel;
            ComputeBuffer cb = new ComputeBuffer(count, 32);
            cs.SetInt("_Size", 1);
            cs.SetInt("_Key0", Client.random.Next(int.MaxValue));
            cs.SetInt("_Key1", Client.random.Next(int.MaxValue));
            cs.SetInt("_Key2", Client.random.Next(int.MaxValue));
            cs.SetInt("_Key3", Client.random.Next(int.MaxValue));
            cs.SetBuffer(wnk, "_Result", cb);
            cs.Dispatch(wnk, count < 512 ? 1 : (count / 512), 1, 1);
            return cb;
        }

        public static ComputeBuffer whiteNoise(int size) {
            return whiteNoise(size, 1);
        }

        public static ComputeBuffer enlarge(ComputeBuffer cbi, int size, int factor) {
            //if (cbi.count == size * size * 3) {
            ComputeBuffer cb = new ComputeBuffer(size * size * factor * factor, 32);
            cs.SetInt("_Size", size);
            cs.SetInt("_Factor", factor);
            cs.SetBuffer(elk, "_Input", cbi);
            cs.SetBuffer(elk, "_Result", cb);
            int dispatchcount = (size * size * factor * factor) / 64;
            cs.Dispatch(elk, dispatchcount > 0 ? dispatchcount : 1, 1, 1);
            cbi.Dispose();
            return cb;
            //}
            return null;
        }

        public static ComputeBuffer add(ComputeBuffer cb1, ComputeBuffer cb2) {
            cs.SetBuffer(adk, "_Input", cb2);
            cs.SetBuffer(adk, "_Result", cb1);
            cs.Dispatch(adk, cb1.count / 64, 1, 1);
            cb2.Dispose();
            return cb1;
        }

        public static ComputeBuffer mul(ComputeBuffer cb1, ComputeBuffer cb2) {
            cs.SetBuffer(mlk, "_Input", cb2);
            cs.SetBuffer(mlk, "_Result", cb1);
            cs.Dispatch(mlk, cb1.count / 64, 1, 1);
            cb2.Dispose();
            return cb1;
        }

        public static ComputeBuffer add(ComputeBuffer cb1, float n) {
            cs.SetFloat("_Number", n);
            cs.SetBuffer(ank, "_Result", cb1);
            cs.Dispatch(ank, cb1.count / 64, 1, 1);
            return cb1;
        }

        public static ComputeBuffer mul(ComputeBuffer cb1, float n) {
            cs.SetFloat("_Number", n);
            cs.SetBuffer(mnk, "_Result", cb1);
            cs.Dispatch(mnk, cb1.count / 64, 1, 1);
            return cb1;
        }

        public static ComputeBuffer clamp(ComputeBuffer cb1, float l, float h) {
            cs.SetFloat("_Number", l);
            cs.SetFloat("_Number2", h);
            cs.SetBuffer(clk, "_Result", cb1);
            cs.Dispatch(clk, cb1.count / 64 / 64, 1, 1);
            return cb1;
        }

        public static ComputeBuffer abs(ComputeBuffer cb1) {
            cs.SetBuffer(abk, "_Result", cb1);
            cs.Dispatch(abk, cb1.count / 64, 1, 1);
            return cb1;
        }

        public static ComputeBuffer max(ComputeBuffer cb1, ComputeBuffer cb2) {
            cs.SetBuffer(mak, "_Input", cb2);
            cs.SetBuffer(mak, "_Result", cb1);
            cs.Dispatch(mak, cb1.count / 64, 1, 1);
            cb2.Dispose();
            return cb1;
        }

        public static ComputeBuffer min(ComputeBuffer cb1, ComputeBuffer cb2) {
            cs.SetBuffer(mik, "_Input", cb2);
            cs.SetBuffer(mik, "_Result", cb1);
            cs.Dispatch(mik, cb1.count / 64, 1, 1);
            cb2.Dispose();
            return cb1;
        }

        public static void grey(float[] indata, int size) {
            if (size * size <= indata.Length) {
                ComputeBuffer cbi = new ComputeBuffer(size * size, 32);
                cbi.SetData(indata);
                ComputeBuffer cb = new ComputeBuffer(size * size, 32);
                cs.SetInt("_Size", size);
                cs.SetBuffer(grk, "_Input", cbi);
                cs.SetBuffer(grk, "_ResultB4", cb);
                cs.Dispatch(grk, (size * size) / 64, 1, 1);
                cbi.Dispose();
                byte[] data = new byte[size * size * 4];
                cb.GetData(data);
                cb.Dispose();
                Texture2D t = new Texture2D(size, size, TextureFormat.ARGB32, false);
                t.LoadRawTextureData(data);
                File.WriteAllBytes("comptex.png", t.EncodeToPNG());
            }
        }

        public void grey() {
            ComputeBuffer cbo = new ComputeBuffer(size * size, 32);
            cs.SetInt("_Size", size);
            cs.SetBuffer(grk, "_Input", cb);
            cs.SetBuffer(grk, "_ResultB4", cbo);
            cs.Dispatch(grk, (size * size) / 64, 1, 1);
            cb.Dispose();
            byte[] data = new byte[size * size * 4];
            cbo.GetData(data);
            cbo.Dispose();
            Texture2D t = new Texture2D(size, size, TextureFormat.ARGB32, false);
            t.LoadRawTextureData(data);
            File.WriteAllBytes("comptex.png", t.EncodeToPNG());
        }

        public static float[] random(int size, int seed, IntVec3 pos) {
            int count = size * size;
            ComputeBuffer cb = new ComputeBuffer(count, 32);
            cs.SetInt("_Size", 1);
            cs.SetInt("_Key0", seed);
            cs.SetInt("_Key1", pos.x);
            cs.SetInt("_Key2", pos.y);
            cs.SetInt("_Key3", pos.z);
            cs.SetBuffer(wnk, "_Result", cb);
            int threads = (int)Mathf.Ceil(count / 512f);
            cs.Dispatch(wnk, threads, 1, 1);
            float[] data = new float[threads * 512];
            cb.GetData(data);
            return data;
        }

        public float[,] getData() {
            float[,] result = new float[size, size];
            cb.GetData(result);
            cb.Dispose();
            return result;
        }

        public void destroy() {
            cb.Dispose();
        }
    }
}