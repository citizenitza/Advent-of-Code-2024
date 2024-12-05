using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024_day_05 {
    public static class Extensions {
        public static void Swap<T>(this List<T> list, int i, int j) {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    public class OrderRule {
        public uint Before;
        public uint After;
    }
    public class Update {
        public List<uint> Pages = new List<uint>();
        public bool PartOneViolation = false;
        public uint ViolationCnt = 0;
    }
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo; 
        public List<OrderRule> ruleList = new List<OrderRule>();
        public List<Update> updateList = new List<Update>();

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
            bool Firstpart = true;
            while ((lineOfText = reader.ReadLine()) != null) {
                if (Firstpart) {
                    if (lineOfText == "") {
                        Firstpart = false;
                        continue;
                    }
                    OrderRule newrule = new OrderRule();
                    newrule.Before = Convert.ToUInt32(lineOfText.Split('|')[0]);
                    newrule.After = Convert.ToUInt32(lineOfText.Split('|')[1]);
                    ruleList.Add(newrule);

                } else {
                    Update newupdate = new Update();
                    newupdate.Pages = lineOfText.Split(',').ToList().Select(uint.Parse).ToList();
                    updateList.Add(newupdate);
                }

                Row++;
            }

        }

        public void PartOne() {
            foreach(Update update in  updateList) {
                Dictionary<uint,int> Cache = new Dictionary<uint,int>();//(rule, index)
                bool RuleViolation = false;
                foreach(OrderRule rule in ruleList) {
                    int BeforeIndex = 0;
                    int AfterIndex = 0;
                    if (Cache.TryGetValue(rule.Before, out BeforeIndex)) {

                    } else {
                        BeforeIndex = update.Pages.IndexOf(rule.Before);
                        Cache[rule.Before] = BeforeIndex;
                    }
                    if (Cache.TryGetValue(rule.After, out AfterIndex)) {

                    } else {
                        AfterIndex = update.Pages.IndexOf(rule.After);
                        Cache[rule.After] = AfterIndex;
                    }
                    if (BeforeIndex > -1 && AfterIndex > -1) {
                        if (BeforeIndex > AfterIndex) {
                            //failed
                            RuleViolation = true;
                            update.PartOneViolation = true;
                            update.ViolationCnt++;
                            //break;
                        }
                    }
                }
                if(!RuleViolation) {
                    Result_PartOne += update.Pages[(update.Pages.Count() - 1) / 2];
                }
            }
        }
        public void PartTwo() {
            var IncorrectList = updateList.Where(x => x.PartOneViolation).ToList();
            foreach (Update update in IncorrectList) {
                Dictionary<uint, int> Cache = new Dictionary<uint, int>();//(rule, index)
                bool RuleViolation = false;
                for(int i= 0; i<ruleList.Count; i++) {
                    //for each rule
                    int BeforeIndex = 0;
                    int AfterIndex = 0;
                    if (Cache.TryGetValue(ruleList[i].Before, out BeforeIndex)) {

                    } else {
                        BeforeIndex = update.Pages.IndexOf(ruleList[i].Before);
                        Cache[ruleList[i].Before] = BeforeIndex;
                    }
                    if (Cache.TryGetValue(ruleList[i].After, out AfterIndex)) {

                    } else {
                        AfterIndex = update.Pages.IndexOf(ruleList[i].After);
                        Cache[ruleList[i].After] = AfterIndex;
                    }
                    if (BeforeIndex > -1 && AfterIndex > -1) {
                        if (BeforeIndex > AfterIndex) {
                            //failed
                            RuleViolation = true;
                            //swap
                            update.Pages.Swap(BeforeIndex, AfterIndex);
                            //refresh indexes
                            Cache[ruleList[i].After] = BeforeIndex;
                            Cache[ruleList[i].Before] = AfterIndex;

                            //restart check
                            //i = -1;
                            continue;
                        }
                    }
                }
                //if (!RuleViolation) {
                var tmp = update.Pages[(update.Pages.Count() - 1) / 2];
                Result_PartTwo += tmp;
                //}
            }
            ;
        }

    }
}
