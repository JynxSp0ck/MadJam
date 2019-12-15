using System;
using System.Collections.Generic;

namespace Game.Utility {
    class Format {
        public static string name(string str) {
            char[] chars = str.ToCharArray();
            if (chars.Length == 0)
                return "";
            chars[0] = char.ToUpper(chars[0]);
            for (int i = 0; i < chars.Length - 1; i++)
                if (char.IsWhiteSpace(chars[i]))
                    chars[i + 1] = char.ToUpper(chars[i + 1]);
            return string.Concat(chars);
        }

        public static List<string> indent(List<string> data) {
            int tab = 0;
            for (int i = 0; i < data.Count; i++) {
                if (data[i].EndsWith("}"))
                    tab--;
                string s = data[i];
                for (int j = 0; j < tab; j++)
                    s = "    " + s;
                if (data[i].EndsWith("{"))
                    tab++;
                data[i] = s;
            }
            return data;
        }
    }
}
