namespace _2024_day_09 {

    public class Space {
        public bool Free;
        public int ID;
    }
    internal class Solution {
        public long Result_PartOne;
        public long Result_PartTwo;
        List<int> Files = new List<int>();
        List<int> FreeSpace = new List<int>();



        Dictionary<int, int> IDLocation = new Dictionary<int, int>();//
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
                    bool even = false;
                    foreach (char c in lineOfText) {
                        if (even) {
                            FreeSpace.Add(Convert.ToInt32(c.ToString()));
                            even = false;
                        } else {
                            Files.Add(Convert.ToInt32(c.ToString()));
                            even = true;
                        }
                    }
                }
                Row++;
            }

        }
        List<Space> P1FileSystem = new List<Space>();

        public void PartOne() {

            //create filesystem
            for (int ID = 0; ID < Files.Count; ID++) {
                //add files
                for (int index = 0; index < Files[ID]; index++) {
                    Space newSpace = new Space();
                    newSpace.ID = ID;
                    newSpace.Free = false;
                    P1FileSystem.Add(newSpace);
                }
                //add freespace
                if (ID < FreeSpace.Count()) {
                    for (int index = 0; index < FreeSpace[ID]; index++) {
                        Space newSpace = new Space();
                        newSpace.ID = -1;
                        newSpace.Free = true;
                        P1FileSystem.Add(newSpace);
                    }
                }
            }
            //Visualize();
            //reorder
            Reorder();
            //Visualize();
            Result_PartOne = CalcChecksum();
        }
        private long CalcChecksum() {
            long retVal = 0;
            for (int i = 0; i < P1FileSystem.Count(); i++) {
                if (P1FileSystem[i].Free) {
                    break;
                }
                retVal += (i * P1FileSystem[i].ID);
                ;
            }

            return retVal;
        }
        private void Visualize(ref List<Space> FileSystem) {
            Console.WriteLine("");
            foreach (Space sp in FileSystem) {
                if (sp.Free) {
                    Console.Write(".");
                } else {
                    Console.Write(sp.ID.ToString());
                }
            }
            Console.Write("\r\n");
        }
        private void Reorder() {
            int Head = 0;
            int Tail = P1FileSystem.Count() - 1;
            Head = MoveHead(Head, Tail);
            Tail = MoveTail(Head, Tail);
            while (Head > 0 && Tail > 0) {
                Swap(P1FileSystem, Head, Tail);
                Head = MoveHead(Head, Tail);
                Tail = MoveTail(Head, Tail);
            }
            ;
        }
        public static void Swap<T>(IList<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        private int MoveHead(int Head, int Tail) {
            int retval = -1;
            for (int i = Head; i < Tail; i++) {
                if (P1FileSystem[i].Free) {
                    retval = i;
                    break;
                }
            }
            return retval;
        }

        private int MoveTail(int Head, int Tail) {
            int retval = -1;
            for (int i = Tail; i > Head; i--) {
                if (!P1FileSystem[i].Free) {
                    retval = i;
                    break;
                }
            }
            return retval;
        }


        List<Space> P2FileSystem = new List<Space>();

        public void PartTwo() {

            //create filesystem
            for (int ID = 0; ID < Files.Count; ID++) {
                //add files
                for (int index = 0; index < Files[ID]; index++) {
                    Space newSpace = new Space();
                    newSpace.ID = ID;
                    newSpace.Free = false;
                    P2FileSystem.Add(newSpace);
                }
                //add freespace
                if (ID < FreeSpace.Count()) {
                    for (int index = 0; index < FreeSpace[ID]; index++) {
                        Space newSpace = new Space();
                        newSpace.ID = -1;
                        newSpace.Free = true;
                        P2FileSystem.Add(newSpace);
                    }
                }
            }

            //Visualize(ref P2FileSystem);
            //reorder
            ReorderP2();

            Result_PartTwo = CalcChecksumP2();

        }

        List<int> IDs = new List<int>();
        private void ReorderP2() {
            int Head = 0;
            int HeadLength = 0;
            int Tail = P2FileSystem.Count() - 1;
            int TailLength = 0;
            ;
            while (Head >= 0 && Tail >= 0) {
                bool noSpaceForFile = false;
                Tail = MoveTailP2(Head, Tail, ref TailLength);
                Head = 0;
                Head = MoveHeadP2(Head, Tail, ref HeadLength);
                ;
                while (HeadLength < TailLength) {
                    Head += HeadLength;
                    Head = MoveHeadP2(Head, Tail, ref HeadLength);
                    if(Head> Tail || Head == -1) {
                        //move tail
                        Head = 0;
                        Tail -= TailLength;
                        noSpaceForFile = true;
                        break;
                    }
                }
                if (noSpaceForFile) {
                    continue;
                }
                if (Head > 0 && Tail > 0) {
                    if (HeadLength >= TailLength) { //enough space -> swap
                        if (!IDs.Any(x => x == P2FileSystem[Tail].ID)) {
                            IDs.Add(P2FileSystem[Tail].ID);
                            SwapWholeFile(Head, Tail, TailLength);
                        }

                    }
                }
                //Visualize(ref P2FileSystem);
            }

        }
        private long CalcChecksumP2() {
            long retVal = 0;
            for (int i = 0; i < P2FileSystem.Count(); i++) {
                if (P2FileSystem[i].Free) {

                } else {
                    retVal += (i * P2FileSystem[i].ID);
                    ;
                }
            }

            return retVal;
        }

        private void SwapWholeFile(int Head, int Tail,int TailLength) {
                for(int i = 0; i < TailLength; i++) {//for the file length
                    Swap(P2FileSystem, Head++, Tail--);
                }
        }
        private int MoveHeadP2(int Head, int Tail,ref int HeadLength) {
            if (Head < 0) {
                return -1;
            }
            int retval = -1;
            HeadLength = 0;
            for (int i = Head; i < Tail; i++) {
                if (P2FileSystem[i].Free) {
                    retval = i;
                    break;
                }
            }
            if (retval > 0) {
                for (int i = retval; i < Tail; i++) {
                    if (!P2FileSystem[i].Free) {
                        break;
                    }
                    HeadLength++;
                }
            }
            return retval;
        }

        private int MoveTailP2(int Head, int Tail, ref int TailLegth) {
            int retval = -1;
            TailLegth = 0;
            for (int i = Tail; i > Head; i--) {
                if (!P2FileSystem[i].Free) {
                    retval = i;
                    break;
                }
            }
            if (retval > 0) {
                for (int i = retval; i > Head; i--) {
                    if (P2FileSystem[i].Free) {
                        break;
                    }
                    if (P2FileSystem[i].ID != P2FileSystem[retval].ID) {
                        break;
                    }
                    TailLegth++;
                }
            }
            return retval;
        }

    }
}
