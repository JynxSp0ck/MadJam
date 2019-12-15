using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class UI {
        public GameObject obj;
        public InventoryView inventoryview;

        public UI(GameObject obj) {
            this.obj = obj;
        }

        public void init() {
            inventoryview = new InventoryView();
        }

        public void update() {
            inventoryview.update();
        }
    }
}
