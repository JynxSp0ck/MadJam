using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Utility;

namespace Game.Controller {
    class Controller {
        PlayerController playcon;
        InventoryController invcon;
        CharacterIO cio;
        public ChunkGenerator chunkgen;

        public Controller() {

        }

        public void init() {
            Cursor.lockState = CursorLockMode.Locked;
            playcon = new PlayerController();
            invcon = new InventoryController();
            cio = new CharacterIO();
            chunkgen = new ChunkGenerator();
            cio.load();
        }

        public void update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                cio.save();
                for (int i = 0; i < Settings.map_size; i++) {
                    for (int j = 0; j < Settings.map_size; j++) {
                        for (int k = 0; k < Settings.map_size; k++) {
                            Chunk chunk = Client.model.map.chunks[i, j, k];
                            if (chunk != null && chunk.loaded && !chunk.saved) {
                                ChunkTask task = new ChunkTask("save", chunk);
                                task.start();
                            }
                        }
                    }
                }
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
            playcon.update();
            invcon.update();
            chunkgen.run();
        }
    }
}
