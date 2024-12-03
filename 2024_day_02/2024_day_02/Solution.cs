using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _2024_day_02 {
    public class Report {
        public List<int> Levels;
    }
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        public List<Report> Reports = new List<Report>();

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
                Report rep = new Report();
                rep.Levels = new List<int>();
                lineArray = lineOfText.Split(" ");
                foreach(string str in lineArray) {
                        rep.Levels.Add(Convert.ToInt32(str));
                }
                Reports.Add(rep);
                Row++;
            }

        }

        public void PartOne() {
            Result_PartOne = Process(false);
        }
        public void PartTwo() {
            Result_PartTwo = Process(true);
        }

        private uint Process(bool bruteforceP2) {
            uint retVal = 0;
            foreach (Report rep in Reports) {

                bool Safe = ProcessSingle(rep.Levels);
                if (!Safe && bruteforceP2) {
                    for(int i = 0; i < rep.Levels.Count; i++) {
                        var Cloned = new List<int>(rep.Levels);
                        Cloned.RemoveAt(i);
                        //recalculate
                        Safe = ProcessSingle(Cloned);
                        if (Safe) {
                            break;
                        }
                    }
                }

                if (Safe) {
                    retVal++;
                }
            }
            return retVal;
        }

        private bool ProcessSingle(List<int> Levels) {
            bool Safe = true;
            bool increase = false;
            int tolerance = 0;
            for (int i = 0; i < Levels.Count; i++) {
                if (i == 0) {
                    if (Levels[i] > Levels[i + 1]) {
                        increase = false;
                    } else if (Levels[i] < Levels[i + 1]) {
                        increase = true;
                    } else {
                        Safe = false;
                        break;
                    }
                } else {
                    if (increase) {
                        int diff = Levels[i] - Levels[i - 1];
                        if (diff <= 3 && diff > 0) {
                            continue;
                        } else {
                            Safe = false;
                            break;
                        }
                    } else {
                        int diff = Levels[i - 1] - Levels[i];
                        if (diff <= 3 && diff > 0) {
                            continue;
                        } else {
                            Safe = false;
                            break;

                        }
                    }
                }
            }
            return Safe;
        }
    }
}
