using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game.Model {
    class Inventory {
        Stack[] slots;

        public Inventory() {
            slots = new Stack[10];
            for (int i = 0; i < slots.Length; i++)
                slots[i] = new Stack();
        }

        public void add(Stack items) {
            for (int i = 0; i < slots.Length; i++) {
                if (slots[i].type == items.type) {
                    int space = slots[i].type.stacklimit - slots[i].count;
                    if (space > 0) {
                        if (space > items.count) {
                            space = items.count;
                        }
                        slots[i].count += space;
                        items.count -= space;
                    }
                    if (items.count == 0) {
                        items.empty();
                        return;
                    }
                }
            }
            for (int i = 0; i < slots.Length; i++) {
                if (slots[i].type == null) {
                    int space = slots[i].type.stacklimit;
                    if (space > 0) {
                        if (space > items.count) {
                            space = items.count;
                        }
                        slots[i].count += space;
                        items.count -= space;
                        slots[i].type = items.type;
                    }
                    if (items.count == 0) {
                        items.empty();
                        return;
                    }
                }
            }
        }
    }
}
