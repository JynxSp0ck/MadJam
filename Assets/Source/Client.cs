using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Controller;
using Game.Utility;

namespace Game {
    class Client : MonoBehaviour {
        public static System.Random random;
        public ComputeShader cs;

        public static Model.Model model;
        public static View.View view;
        public static Controller.Controller controller;
        // Start is called before the first frame update
        void Start() {
            random = new System.Random();
            Noise.init(cs);
            model = new Model.Model();
            view = new View.View();
            controller = new Controller.Controller();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}