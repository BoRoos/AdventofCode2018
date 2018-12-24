using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    class Day14
    {
        public static void Execute()
        {
            int recipies = 509671;
            
            LinkedList<int> scores = new LinkedList<int>();
            var elf1 = scores.AddLast(3);
            var elf2 = scores.AddLast(7);

            while (scores.Count() < recipies + 10)
            {

                var newscore = elf1.Value + elf2.Value;
                if (newscore >= 10) scores.AddLast(1);
                scores.AddLast(newscore % 10);
                var elf1moves = elf1.Value + 1;
                var elf2moves = elf2.Value + 1;
                for (var i = 0; i < elf1moves; i++) elf1 = elf1.Next ?? scores.First;
                for (var i = 0; i < elf2moves; i++) elf2 = elf2.Next ?? scores.First;
 
            }
            var start = scores.First;
            for(var j=0; j<recipies+10;j++)
            {
                if (j>=recipies) Console.Write(start.Value + " ");
                start = start.Next;
            }
            
            Console.WriteLine();

            //part2
            scores = new LinkedList<int>();
            elf1 = scores.AddLast(3);
            elf2 = scores.AddLast(7);
            var pattern = new List<int> { 5, 0, 9, 6, 7, 1 };
            var counter = 0;
            bool found = false;
            var c = scores.First;
            while (!found)
            {
                var newscore = elf1.Value + elf2.Value;
                if (newscore >= 10) scores.AddLast(1);
                scores.AddLast(newscore % 10);
                var elf1moves = elf1.Value + 1;
                var elf2moves = elf2.Value + 1;
                for (var i = 0; i < elf1moves; i++) elf1 = elf1.Next ?? scores.First;
                for (var i = 0; i < elf2moves; i++) elf2 = elf2.Next ?? scores.First;
                if (scores.Count() >= pattern.Count)
                {
                    if (c.Value == pattern[0] && c.Next.Value == pattern[1] && c.Next.Next.Value == pattern[2] && c.Next.Next.Next.Value == pattern[3] && c.Next.Next.Next.Next.Value == pattern[4] && c.Next.Next.Next.Next.Next.Value == pattern[5])
                    {
                        found = true;
                    }
                    else
                    {
                        counter++;
                        c = c.Next;
                    }
                        
                }
                
            }
            Console.WriteLine(counter);
            Console.ReadKey();

        }
    }
  
}
