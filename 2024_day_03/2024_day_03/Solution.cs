using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2024_day_03 {
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        string input = "";
        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string[] lineArray;
            bool firstline = true;
           
            int Row = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                input += lineOfText;
                Row++;
            }
        }

        public void PartOne() {
            MatchCollection matchList = Regex.Matches(input, "mul\\(?\\d?\\d?\\d,?\\d?\\d?\\d\\)");
            var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
            foreach(string mul in list) {
                string inner = mul.Replace("mul", "").Replace("(", "").Replace(")", "");
                uint a = Convert.ToUInt32(inner.Split(',')[0]);
                uint b = Convert.ToUInt32(inner.Split(',')[1]);
                Result_PartOne += (a * b);
            }
        }
        public void PartTwo() {
            MatchCollection matchList = Regex.Matches(input, "(mul\\(\\d+,\\d+\\)|don't\\(\\)|do\\(\\))");
            var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
            bool Enabled = true;
            foreach (string cmd in list) {
                if (cmd == "don't()") {
                    Enabled = false;
                } else if (cmd == "do()") {
                    Enabled = true;
                } else {
                    if (Enabled) {
                        string inner = cmd.Replace("mul", "").Replace("(", "").Replace(")", "");
                        uint a = Convert.ToUInt32(inner.Split(',')[0]);
                        uint b = Convert.ToUInt32(inner.Split(',')[1]);
                        Result_PartTwo += (a * b);
                    }
                }
            }
        }
    }
}
