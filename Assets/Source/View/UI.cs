using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class UI {
        InventoryView inventoryview;

        public UI() {
            inventoryview = new InventoryView();
        }

        public void update() {
            inventoryview.update();
        }
    }
}
