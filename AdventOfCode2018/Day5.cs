using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day5
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input5.txt");
            var charlist = data[0].ToCharArray().ToList();
            var runagain = false;
            //do
            //{
            //    runagain = false;
            //    for (var i = 0; i < charlist.Count - 1; i++)
            //    {
            //        if (Char.ToLower(charlist[i]) == Char.ToLower(charlist[i + 1]) && (int)charlist[i] != (int)charlist[i + 1])
            //        {
            //            charlist.RemoveAt(i);
            //            charlist.RemoveAt(i);
            //            runagain = true;
            //            break;
            //        }
            //    }

            //} while (runagain);

            Console.WriteLine(charlist.Count);
            var filterlist = "abcdefghijklmnopqrstuvwxyz";
            var currentcount = 100000;
            var currentlist = new List<string>();
            foreach (var letter in filterlist)
            {
                var d = data[0].Replace(letter.ToString(), "").Replace(Char.ToUpper(letter).ToString(), "");
                charlist = d.ToCharArray().ToList();
                runagain = false;
                do
                {
                    runagain = false;
                    for (var i = 0; i < charlist.Count - 1; i++)
                    {
                        if (Char.ToLower(charlist[i]) == Char.ToLower(charlist[i + 1]) && (int)charlist[i] != (int)charlist[i + 1])
                        {

                            charlist.RemoveAt(i);
                            charlist.RemoveAt(i);
                            runagain = true;
                            break;
                        }
                    }

                } while (runagain);


                if (charlist.Count < currentcount)
                {
                    currentcount = charlist.Count;
                    currentlist = charlist.Select(c => c.ToString()).ToList();
                }
            }

            Console.WriteLine(currentlist.Count);

            Console.ReadKey();
        }

    }
}
