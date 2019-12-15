using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Utility;

namespace Game.Controller {
    class CharacterIO {
        public CharacterIO() {

        }

        public void save() {
            saveCharacterList();
            foreach (Character character in Client.model.characters)
                saveCharacter(character);
        }

        void saveCharacterList() {
            List<string> text = new List<string>();
            foreach (Character character in Client.model.characters)
                text.Add(character.name);
            File.WriteAllLines("Maps/" + Client.model.map.name + "/charlist.txt", text);
        }

        void saveCharacter(Character character) {
            List<string> text = new List<string>();
            text.Add(character.pos.x + " " + character.pos.y + " " + character.pos.z);
            text.Add(character.digspeed + " " + character.range);
            foreach (Stack slot in character.inventory.slots) {
                if (slot.type == null)
                    text.Add("null " + slot.count);
                else
                    text.Add(slot.type.name + " " + slot.count);
            }
            File.WriteAllLines("Maps/" + Client.model.map.name + "/" + character.name + ".char", text);
        }

        public void load() {
            loadCharacterList();
        }

        void loadCharacterList() {
            if (!File.Exists("Maps/" + Client.model.map.name + "/charlist.txt")) {
                Client.model.player.character = new Character("player");
                Client.model.characters.Add(Client.model.player.character);
                return;
            }
            Reader r = new Reader("Maps/" + Client.model.map.name + "/charlist.txt");
            while (!r.EOF()) {
                loadCharacter(r.read());
            }
            Client.model.player.character = Client.model.characters[0];
        }

        void loadCharacter(string name) {
            Reader r = new Reader("Maps/" + Client.model.map.name + "/" + name + ".char");
            Character character = new Character(name);
            string[] s;
            s = r.read().Split(Reader.space);
            character.pos = new Vec3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
            s = r.read().Split(Reader.space);
            character.digspeed = float.Parse(s[0]);
            character.range = float.Parse(s[1]);
            List<Stack> slots = new List<Stack>();
            while (!r.EOF()) {
                s = r.read().Split(Reader.space);
                if (s[0] == "null")
                    slots.Add(new Stack());
                else
                    slots.Add(new Stack(BlockType.get(s[0]), int.Parse(s[1])));
            }
            character.inventory.slots = slots.ToArray();
            Client.model.characters.Add(character);
        }
    }
}
