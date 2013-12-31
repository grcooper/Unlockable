using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Unlockable
{
    public class dataManagement
    {
        
        public List<string> dataString = new List<string>();
        public List<int> highScores = new List<int>();
        public int score;

        public List<int> ReadData()
        {
            string line;
            StreamReader sr = new StreamReader(@"C:\\Highscores.txt");

            for (int y = 0; y < 5; y++)
            {
                highScores.Add(-1);
            }

            try
            {
                line = sr.ReadLine();

                for (int q = 0; q < highScores.Count; q++)
                {
                    highScores[q] = Convert.ToInt32(line);
                    line = sr.ReadLine();
                }
            }
            catch
            {
                for (int i = 0; i < 5; i++)
                {
                    highScores.Add(0);
                }
            }

            sr.Close();
            return highScores;
        }
                
        public List<int> WriteData(int s)
        {
                highScores.Add(s);
                selection(highScores, highScores.Count, 0);
                highScores.RemoveAt(5);

                StreamWriter sw = new StreamWriter(@"C:\\Highscores.txt");

                for (int l = 0; l < 5; l++)
                {
                    sw.WriteLine(Convert.ToString(highScores[l]));
                }

                sw.Close();

                return highScores;
         }

        public void DeleteData()
        {
            StreamWriter sw = new StreamWriter(@"C:\\Highscores.txt");

            for (int h = 0; h < 5; h++)
            {
                sw.WriteLine("0");
            }
            sw.Close();
        }

        public void DeletePlayerData()
        {
            StreamWriter sw = new StreamWriter(@"C:\\PlayerData.txt");

            for (int h = 0; h < 5; h++)
            {
                sw.WriteLine("-1");
            }
            sw.Close();
        }

        public List<string> ReadPlayerData()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader(@"C:\\PlayerData.txt");
                for(int p = 0; p < 7; p++)
                {
                    dataString.Add("0");
                }
                try
                {
                    line = sr.ReadLine();

                    for (int q = 0; q < dataString.Count; q++)
                    {
                        dataString[q] = line;
                        line = sr.ReadLine();
                    }
                }
                catch
                {
                    for (int i = 0; i < 7; i++)
                    {
                        dataString.Add("-1");
                    }
                }

                sr.Close();
            }
            catch
            {
                return null;
            }

            //for (int u = 0; u < highScores.Count; u++)
            //{
            //    dataString.Add(Convert.ToString(highScores[u]));
            //}
            return dataString;
        }

        public List<string> WritePlayerData(int score, int level, int a1, int a2, int a3, int a4, int a5)
        {
            dataString.Clear();

            dataString.Add(Convert.ToString(score));
            dataString.Add(Convert.ToString(level));
            dataString.Add(Convert.ToString(a1));
            dataString.Add(Convert.ToString(a2));
            dataString.Add(Convert.ToString(a3));
            dataString.Add(Convert.ToString(a4));
            dataString.Add(Convert.ToString(a5));

            StreamWriter sw = new StreamWriter(@"C:\\PlayerData.txt");
            
            try
            {
                for (int l = 0; l < 7; l++)
                {
                    sw.WriteLine(dataString[l]);
                }
            }
            catch
            {
                for (int l = 0; l < 7; l++)
                {
                    sw.WriteLine("-1");
                }
            }

            sw.Close();

            return dataString;
        }


        static List<int> selection(List<int> templist, int s, int o)
        {
            int temp;
            if (o == 1)
            {
                for (int i = 0; i < s; i++)
                {
                    for (int j = i + 1; j < s; j++)
                    {
                        if (templist[j] < templist[i])
                        {
                            temp = templist[i];
                            templist[i] = templist[j];
                            templist[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < s; i++)
                {
                    for (int j = i + 1; j < s; j++)
                    {
                        if (templist[j] > templist[i])
                        {
                            temp = templist[i];
                            templist[i] = templist[j];
                            templist[j] = temp;
                        }
                    }
                }

            }
            return templist;
        }
    }
}
