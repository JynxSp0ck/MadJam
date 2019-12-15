using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Model;
using Game.Utility;

namespace Game.View {
    class InventoryView {
        GameObject panel;
        Text[] text;
        public int selected = 0;
        int page = 0;
        int per = 5;

        public InventoryView() {
            panel = Find.name(Client.view.ui.obj, "InventoryPanel");
            text = new Text[per];
            for (int i = 0; i < per; i++) {
                text[i] = Find.name(Find.name(panel, "Slot" + (i + 1)), "Text").GetComponent<Text>();
            }
        }

        public void update() {
            for (int i = 0; i < per; i++) {
                text[i].text = getText(Client.model.player.character.inventory.slots[i + page * per]);
                if (i == selected) {
                    text[i].color = new Color(0.5f, 0, 0);
                    text[i].fontStyle = FontStyle.Bold;
                }
                else {
                    text[i].color = new Color(0, 0, 0);
                    text[i].fontStyle = FontStyle.Normal;
                }
            }
        }

        public int getSelected() {
            return selected + page * per;
        }

        string getText(Stack stack) {
            if (stack.type == null)
                return "";
            return Format.name(stack.type.name) + " x " + stack.count;
        }
    }
}
