using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024_day_01 {
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        List<int> LeftList = new List<int>();
        List<int> RightList = new List<int>();
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
            int Row = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split("   ");
                LeftList.Add(Convert.ToInt32(lineArray[0]));
                RightList.Add(Convert.ToInt32(lineArray[1]));
            }

        }

        public void PartOne() {
            LeftList.Sort();
            RightList.Sort();
            uint result = 0;
            for(int i=0; i < LeftList.Count; i++) {
                result+=  (uint)Math.Abs(LeftList[i] - RightList[i]);
            }
            Result_PartOne = result;
        }
        public void PartTwo() {
            int result = 0;
            for (int i = 0; i < LeftList.Count; i++) {
                result += RightList.Where(x => x == LeftList[i]).Count() * LeftList[i];
            }
            Result_PartTwo = (uint)result;

        }
    }
}
