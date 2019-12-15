using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class MoneyView {
        GameObject panel;
        Text text;

        public MoneyView() {
            panel = Find.name(Client.view.ui.obj, "MoneyPanel");
            text = Find.name(panel, "Text").GetComponent<Text>();
        }

        public void update() {
            text.text = "£" + Client.model.money;
        }
    }
}
