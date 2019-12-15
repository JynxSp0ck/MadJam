﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Utility;

namespace Game.Controller {
    class InventoryController {
        public InventoryController() {

        }

        public void update() {
            int selected = -1;
            if (Input.GetKeyDown(KeyCode.Alpha1))
                selected = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                selected = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3))
                selected = 2;
            if (Input.GetKeyDown(KeyCode.Alpha4))
                selected = 3;
            if (Input.GetKeyDown(KeyCode.Alpha5))
                selected = 4;
            if (selected >= 0)
                Client.view.ui.inventoryview.selected = selected;
        }
    }
}
