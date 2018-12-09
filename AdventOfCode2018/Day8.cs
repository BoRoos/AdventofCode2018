using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day8
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input8test.txt");

            
            Console.WriteLine(result);
            Console.ReadKey();

        }
 
    }
    class Node {
        public string header;
        public List<Node> Childs;
        public SortedList<string, Step> Followlist = new SortedList<string, Step>();
 
}
}
