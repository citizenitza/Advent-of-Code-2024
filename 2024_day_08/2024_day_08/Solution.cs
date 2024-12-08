using System.Text.RegularExpressions;

namespace _2024_day_08 {
    internal class Solution {
        public uint Result_PartOne;
        public uint Result_PartTwo;
        int MatrixSize;
        Regex _regex = new Regex(@"\d|[a-zA-Z]");
        char[,] Map;
        char[,] VisualizeMap;
        int[,] UniqueMap;
        int[,] UniqueMapP2;
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
                    Map = new char[MatrixSize, MatrixSize];
                    UniqueMap = new int[MatrixSize, MatrixSize];
                    UniqueMapP2 = new int[MatrixSize, MatrixSize];
                    VisualizeMap = new char[MatrixSize, MatrixSize];
                }
                int Col = 0;
                foreach (char c in lineOfText) {
                    //Input[Row, Col] = new Tile();
                    Map[Row, Col] = c;
                    UniqueMap[Row, Col] = 0;
                    Col++;
                }
                Row++;
            }

        }

        public void PartOne() {
            for (int row = 0; row < MatrixSize; row++) {
                for (int col = 0; col < MatrixSize; col++) {
                    if (_regex.IsMatch(Map[row, col].ToString())) {
                        //antenna found
                        FindAntennaPairs(row, col);
                        ;
                    }
                }
            }
            DrawHeatDistanceMap(UniqueMap);
        }

        private void FindAntennaPairs(int _startRow, int _startCol) {
            int iterateStartCol = _startCol + 1;
            for (int row = _startRow; row < MatrixSize; row++) {
                for (int col = iterateStartCol; col < MatrixSize; col++) {
                    if (Map[_startRow, _startCol] == Map[row, col]) {

                        var c = Map[row, col];
                        CalcDistanceAndMarkNode(_startRow, _startCol, row, col);
                    }
                }
                iterateStartCol = 0;
            }
        }
        private void CalcDistanceAndMarkNode(int Arow, int Acol, int Brow, int Bcol) {
            int dRow = Arow - Brow;
            int dCol = Acol - Bcol;
            int newRow = Arow + dRow;
            int newCol = Acol + dCol;
            if (newRow >= 0 && newCol >= 0 && newRow < MatrixSize && newCol < MatrixSize) {//within range
                if (UniqueMap[newRow, newCol] == 0) {
                    Result_PartOne++;
                    UniqueMap[newRow, newCol] += 1;
                }
            }
            //reverse
            dRow = Brow - Arow;
            dCol = Bcol - Acol;
            newRow = Brow + dRow;
            newCol = Bcol + dCol;
            if (newRow >= 0 && newCol >= 0 && newRow < MatrixSize && newCol < MatrixSize) {//within range
                if (UniqueMap[newRow, newCol] == 0) {
                    Result_PartOne++;
                    UniqueMap[newRow, newCol] += 1;
                }
            }
        }

        private void DrawHeatDistanceMap(int[,] uMap, int colorcol = -1, int colorrow = -1, ConsoleColor color = ConsoleColor.Red, int obstacleCol = -1, int obstaclerow = -1) {
            Console.WriteLine("");
            Console.WriteLine("");
            for (int i = 0; i < MatrixSize; i++) {
                for (int j = 0; j < MatrixSize; j++) {
                    if (uMap[i, j] != 0 && Map[i, j] == '.') {
                        VisualizeMap[i, j] = '#';
                    } else {
                        VisualizeMap[i, j] = Map[i, j];
                    }
                }
            }
            var originalColor = Console.ForegroundColor;

            for (int Row = 0; Row < MatrixSize; Row++) {
                string line = "";
                for (int Col = 0; Col < MatrixSize; Col++) {
                    if (Col == colorcol && Row == colorrow) {
                        Console.ForegroundColor = color;
                        Console.Write(VisualizeMap[Row, Col]);
                        Console.ForegroundColor = originalColor;
                    } else if (Col == obstacleCol && Row == obstaclerow) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('O');
                        Console.ForegroundColor = originalColor;
                    } else {
                        Console.Write(VisualizeMap[Row, Col]);
                    }
                }
                Console.Write("\r\n");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void PartTwo() {
            for (int row = 0; row < MatrixSize; row++) {
                for (int col = 0; col < MatrixSize; col++) {
                    if (_regex.IsMatch(Map[row, col].ToString())) {
                        //antenna found
                        FindAntennaPairsP2(row, col);
                        ;
                    }
                }
            }
            DrawHeatDistanceMap(UniqueMapP2);
        }


        private void FindAntennaPairsP2(int _startRow, int _startCol) {
            int iterateStartCol = _startCol + 1;
            for (int row = _startRow; row < MatrixSize; row++) {
                for (int col = iterateStartCol; col < MatrixSize; col++) {
                    if (Map[_startRow, _startCol] == Map[row, col]) {

                        var c = Map[row, col];
                        CalcDistanceAndMarkNodeP2(_startRow, _startCol, row, col);
                    }
                }
                iterateStartCol = 0;
            }
        }
        private void CalcDistanceAndMarkNodeP2(int Arow, int Acol, int Brow, int Bcol) {
            int dRow = Arow - Brow;
            int dCol = Acol - Bcol;
            int newRow = Arow;
            int newCol = Acol;
            while (true) {
                if (newRow >= 0 && newCol >= 0 && newRow < MatrixSize && newCol < MatrixSize) {//within range
                    if (UniqueMapP2[newRow, newCol] == 0) {
                        Result_PartTwo++;
                        UniqueMapP2[newRow, newCol] += 1;
                    }
                } else {
                    break;
                }
                newRow += dRow;
                newCol += dCol;
            }
            //reverse
            dRow = Brow - Arow;
            dCol = Bcol - Acol;
            newRow = Brow;
            newCol = Bcol;
            while (true) {
                if (newRow >= 0 && newCol >= 0 && newRow < MatrixSize && newCol < MatrixSize) {//within range
                    if (UniqueMapP2[newRow, newCol] == 0) {
                        Result_PartTwo++;
                        UniqueMapP2[newRow, newCol] += 1;
                    }
                } else {
                    break;
                }
                newRow += dRow;
                newCol += dCol;
            }
        }
    }
}
