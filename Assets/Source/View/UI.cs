using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class UI {
        public GameObject obj;
        public MoneyView moneyview;
        public InventoryView inventoryview;

        public UI(GameObject obj) {
            this.obj = obj;
        }

        public void init() {
            inventoryview = new InventoryView();
            moneyview = new MoneyView();
        }

        public void update() {
            inventoryview.update();
            moneyview.update();
        }
    }
}
