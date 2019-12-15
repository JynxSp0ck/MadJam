using System;
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
            int selected = Client.view.ui.inventoryview.selected;
            if (Input.GetKeyDown(KeyCode.UpArrow))
                selected--;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                selected++;
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
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Client.view.ui.inventoryview.upPage();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                Client.view.ui.inventoryview.downPage();
            if (selected < 0)
                selected = 0;
            if (selected > 4)
                selected = 4;
            Client.view.ui.inventoryview.selected = selected;
        }
    }
}
