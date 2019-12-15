using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility {
    class Find {
        public static GameObject name(GameObject ancestor, string name) {
            Component[] descendents = ancestor.GetComponentsInChildren<Component>(true);
            for (int i = 0; i < descendents.Length; i++)
                if (descendents[i].name.Equals(name))
                    return descendents[i].gameObject;
            return null;
        }

        public static Component name(GameObject ancestor, Type type, string name) {
            Component[] descendents = ancestor.GetComponentsInChildren(type, true);
            for (int i = 0; i < descendents.Length; i++)
                if (descendents[i].name.Equals(name))
                    return descendents[i];
            return null;
        }

        public static bool parent(GameObject child, string name) {
            bool found = false;
            GameObject ancestor = child;
            while (!found) {
                if (ancestor.name == name)
                    return true;
                if (ancestor.transform.parent == null)
                    return false;
                ancestor = ancestor.transform.parent.gameObject;
            }
            return false;
        }
    }
}
