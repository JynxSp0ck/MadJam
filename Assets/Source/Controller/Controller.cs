using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Utility;

namespace Game.Controller {
    class Controller {
        PlayerController playcon;

        public Controller() {

        }

        public void init() {
            Cursor.lockState = CursorLockMode.Locked;
            playcon = new PlayerController();
        }

        public void update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
            playcon.update();
        }
    }
}
