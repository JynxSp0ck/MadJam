﻿using System;
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

        Stopwatch clock;
        long lastTime = 0;
        // Start is called before the first frame update
        void Start() {
            Settings.calculate();
            random = new System.Random();
            Noise.init(cs);
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
                controller.update();
                view.update();
                lastTime = time;
            }
        }
    }
}