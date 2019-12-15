using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Controller;
using Game.Utility;
using System.Diagnostics;

namespace Game {
    class Client : MonoBehaviour {
        public static System.Random random;
        public ComputeShader cs;

        public static Model.Model model;
        public static View.View view;
        public static Controller.Controller controller;
        public static int seed = 1;

        Stopwatch clock;
        static long lastTime = 0;
        public static long time { get { return lastTime; } }
        // Start is called before the first frame update
        void Start() {
            Settings.calculate();
            random = new System.Random();
            Noise.init(cs);

            //new TextureGenerator().generate();

            model = new Model.Model();
            view = new View.View();
            controller = new Controller.Controller();
            view.init();
            controller.init();
            clock = new Stopwatch();
            clock.Start();
        }

        // Update is called once per frame
        void Update() {
            long time = clock.ElapsedMilliseconds;
            if (time - lastTime > 10) {
                lastTime = time;
                controller.update();
                view.update();
            }
        }
    }
}