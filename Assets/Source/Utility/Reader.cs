using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Utility {
    class Reader {
        List<string> data;
        int index;

        public static char[] space = " ".ToCharArray();

        public Reader(string address) {
            data = new List<string>();
            data.AddRange(File.ReadAllLines(address));
            for (int i = 0; i < data.Count; i++)
                data[i] = data[i].Trim();
            index = 0;
        }

        public Reader(List<string> input) {
            data = new List<string>();
            data.AddRange(input);
            for (int i = 0; i < data.Count; i++)
                data[i] = data[i].Trim();
            index = 0;
        }

        public string next() {
            if (index < data.Count) {
                return data[index];
            }
            else {
                return null;
            }
        }

        public string read() {
            if (index < data.Count) {
                return data[index++];
            }
            else {
                data = new List<string>();
                index = 0;
                return null;
            }
        }

        public string[] splitread() {
            return read().Split(space);
        }

        public string[] read(int n) {
            List<string> s = new List<string>();
            for (int i = 0; i < n; i++)
                s.Add(read());
            return s.ToArray();
        }

        public bool EOF() {
            return index >= data.Count;
        }
    }
}
