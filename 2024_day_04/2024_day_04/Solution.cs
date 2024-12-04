using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2024_day_04 {
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        char[,] Matrix;
        int MatrixSize;
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
                    Matrix = new char[MatrixSize, MatrixSize];
                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    //Matrix[Row, Col] = new Tile();
                    Matrix[Row, Col] = c;
                    Col++;
                }
                Row++;
            }

        }

        public void PartOne() {
            int result = 0;
            int CurrentResult = 0;
            //columns
            string Result;
            for (int col = 0; col < MatrixSize; col++) {
                Result = "";
                for (int row = 0; row < MatrixSize; row++) {
                    Result += Matrix[row, col];
                }
                CurrentResult += XMASCount(Result);
            }
            result += CurrentResult;
            Console.WriteLine("Column: " + CurrentResult.ToString());
            CurrentResult = 0;
            //rows
            string Row;
            for (int row = 0; row < MatrixSize; row++) {
                Result = "";
                for (int col = 0; col < MatrixSize; col++) {
                    Result += Matrix[row, col];
                }
                CurrentResult += XMASCount(Result);
            }

            result += CurrentResult;
            Console.WriteLine("Rows: " + CurrentResult.ToString());
            CurrentResult = 0;
            //SecondaryDiagonals
            for (int row = 0; row < MatrixSize; row++) {
                Result = "";
                Result = SecondaryDiagonals(row, 0);
                CurrentResult += XMASCount(Result);
            }
            for (int col = 1; col < MatrixSize; col++) {
                Result = "";
                Result = SecondaryDiagonals(MatrixSize-1, col);
                CurrentResult += XMASCount(Result);
            }
            result += CurrentResult;
            Console.WriteLine("Secondary diags: " + CurrentResult.ToString());
            CurrentResult = 0;
            //Primary Diagonals
            for (int row = 0; row < MatrixSize; row++) {
                Result = "";
                Result = PrimaryDiagonals(row, MatrixSize-1);
                CurrentResult += XMASCount(Result);
            }
            for (int col = 0; col < MatrixSize-1; col++) {
                Result = "";
                Result = PrimaryDiagonals(MatrixSize-1, col);
                CurrentResult += XMASCount(Result);
            }
            result += CurrentResult;
            Console.WriteLine("Primary diags: " + CurrentResult.ToString());


            Result_PartOne = (uint)result;
        }
        private int XMASCount(string input) {
            MatchCollection matchList = Regex.Matches(input, "(?=(XMAS)|(SAMX))");
            var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
            return matchList.Count;

            //MatchCollection xmasmatch = Regex.Matches(input, "XMAS");
            //var xmasist = xmasmatch.Cast<Match>().Select(match => match.Value).ToList();
            //MatchCollection samxmatch = Regex.Matches(input, "SAMX");
            //var samxlist = samxmatch.Cast<Match>().Select(match => match.Value).ToList();
            //return xmasist.Count + samxlist.Count;
        }
        private string SecondaryDiagonals(int startRow,int StartCol) {
            string retVal = "";
            int row = startRow;
            int col = StartCol;
            while(row >= StartCol) {
                retVal += Matrix[row, col];
                row--;
                col++;
            }
            return retVal;
        }
        private string PrimaryDiagonals(int startRow, int StartCol) {
            string retVal = "";
            int row = startRow;
            int col = StartCol;

            while (row >= 0 && col >= 0) {
                retVal += Matrix[row, col];
                row--;
                col--;
            }
            return retVal;
        }
        public void PartTwo() {
            int result = 0;
            for (int col = 1; col < MatrixSize-1; col++) {
                for (int row = 1; row < MatrixSize-1; row++) { //inner square
                    if (Matrix[row, col] == 'A') {
                        if(Part2Checker(row, col)) {
                            result++;
                        }
                    }
                }
            }
            Result_PartTwo = (uint)result;
        }
        private bool Part2Checker(int row, int col) {
            bool retVal = false;
            bool AnyError = false;
            //first diag
            if(Matrix[row-1, col-1] == 'M') {
                if(Matrix[row+1, col+1] == 'S') {

                } else {
                    AnyError = true;
                }
            }else if (Matrix[row-1, col-1] == 'S') {
                if (Matrix[row + 1, col + 1] == 'M') {

                } else {
                    AnyError = true;
                }
            } else {
                AnyError = true;
            }

            //second diag
            if (Matrix[row - 1, col + 1] == 'M') {
                if (Matrix[row + 1, col - 1] == 'S') {

                } else {
                    AnyError = true;
                }
            } else if (Matrix[row - 1, col + 1] == 'S') {
                if (Matrix[row + 1, col - 1] == 'M') {

                } else {
                    AnyError = true;
                }
            } else {
                AnyError = true;
            }



            if (!AnyError) {
                retVal = true;
            }
            return retVal;
        }
    }
}
