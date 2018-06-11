using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ProjectForAssignment
{
    class Program
    {

        public static bool spaceControl(List<string> Mylists, int columnCounters, int LineCounters)  // Control spaces about .csv
        {
            if (Mylists[LineCounters][columnCounters].Equals(' ') || Mylists[LineCounters][columnCounters].Equals('\t') || Mylists[LineCounters][columnCounters].Equals('\r') || Mylists[LineCounters][columnCounters].Equals('\n') || Mylists[LineCounters][columnCounters].Equals('\x0b'))
            {
                return true;
            }
            return false;
        }
        static void Main(string[] args)
        {

            using (var reader = new StreamReader(@"C:\Users\gencf\OneDrive\Masaüstü\exhibitA-input\exhibitA-input.csv"))   // Input Location on  our computer
            {
                // Our list area
                List<string> MyList = new List<string>();
                List<string> SONG_ID = new List<string>();
                List<string> PLAY_ID = new List<string>();
                List<string> CLIENT_ID = new List<string>();
                List<string> PLAY_TS = new List<string>();
                List<string> Control = new List<string>();
                List<int> distinctsongsplayed = new List<int>();

                while (!reader.EndOfStream)  //Read For .csv
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    MyList.Add(values[0]);
                }
                int lineCounter = 0;   // Line Counter for .csv
                do
                {
                    String ListOfID = "";
                    int columnCounter = 0; // column Counter for .csv
                    int space = 0;
                    do//Control play id for  column 
                    {
                        if (spaceControl(MyList, columnCounter, lineCounter).Equals(true))
                        {
                            space++;
                        }
                        else
                        {
                            ListOfID += MyList[lineCounter][columnCounter];
                        }
                        columnCounter++;
                    } while (space.Equals(0));

                    PLAY_ID.Add(ListOfID); //Adding list
                    ListOfID = "";

                    do // Control song id for column
                    {
                        if (spaceControl(MyList, columnCounter, lineCounter).Equals(true))
                        {
                            space++;
                        }
                        else
                        {
                            ListOfID += MyList[lineCounter][columnCounter];
                        }
                        columnCounter++;
                    } while (space.Equals(1));

                    SONG_ID.Add(ListOfID);  //Adding  list
                    ListOfID = "";

                    do // Control client id for column
                    {
                        if (spaceControl(MyList, columnCounter, lineCounter).Equals(true))
                        {
                            space++;
                        }
                        else
                        {
                            ListOfID += MyList[lineCounter][columnCounter];
                        }
                        columnCounter++;
                    } while (space.Equals(2));

                    CLIENT_ID.Add(ListOfID); //Adding  list
                    ListOfID = "";

                    do  //Control Date And Clock for column
                    {
                        if (columnCounter.Equals(MyList[lineCounter].Length))
                        {
                            space = 5;
                        }

                        else if (spaceControl(MyList, columnCounter, lineCounter).Equals(true))
                        {
                            ListOfID += MyList[lineCounter][columnCounter];
                            space++;
                        }
                        else
                        {
                            ListOfID += MyList[lineCounter][columnCounter];
                        }
                        columnCounter++;
                    } while (space.Equals(3) || space.Equals(4));
                    PLAY_TS.Add(ListOfID);
                    lineCounter++;
                } while (lineCounter < MyList.Count);
                int counter = 0;
                for (int i = 0; i < CLIENT_ID.Count; i++)  // search values on .csv 
                {
                    List<string> output = new List<string>(); // List for output

                    if (!Control.Contains(CLIENT_ID[i]))
                    {
                        for (int j = 0; j < CLIENT_ID.Count; j++)
                        {
                            if (CLIENT_ID[i].Equals(CLIENT_ID[j]))
                            {
                                if (PLAY_TS[j].Contains("10/08/2016"))
                                {
                                    output.Add(SONG_ID[j]);
                                }
                            }
                        }
                    }
                    Control.Add(CLIENT_ID[i]);
                    output = output.Distinct().ToList();
                    distinctsongsplayed.Add(output.Count);
                    if (output.Count.Equals(346))
                    {
                        counter++;
                    }
                }
                Console.WriteLine(counter + " users played 346 distinct songs.");
                Console.Write(distinctsongsplayed.Max() + " is the maximum number of distinct songs played.");
            }
            Console.ReadKey();
        }
    }
}
