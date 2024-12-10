using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace _2024_day_10 {
    public struct Tile {
        public Location Location;
        public int Height;
        public bool Visited;

    }
    public struct Location {
        public int row;
        public int col;
    }
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        Tile[,] Map;
        int MatrixSize;
        List<Location> StartLocations = new List<Location>();
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
                if (firstline) {
                    firstline = false;
                    MatrixSize = lineOfText.Count();
                    //initialize input
                    Map = new Tile[MatrixSize, MatrixSize];
                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    Location newLoc = new Location();
                    newLoc.row = Row;
                    newLoc.col = Col;
                    Map[Row, Col] = new Tile();
                    Map[Row, Col].Height = Convert.ToInt32(c.ToString());
                    Map[Row, Col].Location = newLoc;
                    if (Map[Row, Col].Height == 0) {
                        StartLocations.Add(newLoc);
                    }
                    Col++;
                }
                Row++;
            }

        }

        public void PartOne() {
            int partialResult = 0;
            foreach(Location startloc in StartLocations) {
                var tmp = ProcessFromStart(startloc);
                //Console.WriteLine("Cycle one result: " + tmp.ToString());
                partialResult += tmp;

            }
            Result_PartOne = (uint)partialResult;
        }

        private int ProcessFromStart(Location _starLocation) {
            int currentResult = 0;
            Queue<Location> LocationQueue = new Queue<Location>();
            List<Location> PeaksVisitied = new List<Location>();
            //enqueue first
            LocationQueue.Enqueue(_starLocation);

            while(LocationQueue.Count > 0) {
                var current = LocationQueue.Dequeue();
                //check Right
                if (current.col != (MatrixSize - 1)) { // boundary check
                    int newcol = current.col + 1;
                    int newrow = current.row;
                    if (Map[newrow, newcol].Height == (Map[current.row, current.col].Height + 1)) {
                        Map[newrow, newcol].Visited = true;
                        LocationQueue.Enqueue(Map[newrow, newcol].Location);//enqueue
                    }
                }
                //check Down
                if (current.row != (MatrixSize - 1)) {// boundary check
                    int newcol = current.col;
                    int newrow = current.row + 1;
                    if (Map[newrow, newcol].Height == (Map[current.row, current.col].Height + 1)) {
                        Map[newrow, newcol].Visited = true;
                        LocationQueue.Enqueue(Map[newrow, newcol].Location);//enqueue
                    }
                }
                //check Left
                if (current.col != 0) { // boundary check
                    int newcol = current.col - 1;
                    int newrow = current.row;
                    if (Map[newrow, newcol].Height == (Map[current.row, current.col].Height + 1)) {
                        Map[newrow, newcol].Visited = true;
                        LocationQueue.Enqueue(Map[newrow, newcol].Location);//enqueue
                    }
                }
                //check Up
                if (current.row != 0) {// boundary check
                    int newcol = current.col;
                    int newrow = current.row - 1;
                    if (Map[newrow, newcol].Height == (Map[current.row, current.col].Height + 1)) {
                        Map[newrow, newcol].Visited = true;
                        LocationQueue.Enqueue(Map[newrow, newcol].Location);//enqueue
                    }
                }

                if (Map[current.row,current.col].Height == 9) {
                    if(!PeaksVisitied.Any(x=> x.row == current.row && x.col == current.col)) {
                        //uniquepeak
                        currentResult++;
                        PeaksVisitied.Add(current);
                    }
                    Result_PartTwo++;
                    //reached a peak
                }
            }
            return currentResult;
        }

        public void PartTwo() {

        }
    }
}
