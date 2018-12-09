using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    class Day9
    {
        public static void Execute()
        {
            int players = 404;
            int lastmarble = 71852*100;
            long[] scores = new long[players];
            LinkedList<int> circle = new LinkedList<int>();
            var current = circle.AddFirst(0);
            for (var move = 1; move <= lastmarble; move++)
            {
                if (move % 23 == 0)
                {
                    for (var i = 0; i < 7; i++)
                    {
                        current = current.Previous ?? circle.Last;
                    }
                    scores[move % players] += move;
                    scores[move % players] += current.Value;
                    var remove = current;
                    current = remove.Next??circle.First;
                    circle.Remove(remove);
                }
                else
                {
                    current = circle.AddAfter(current.Next ?? circle.First, move);
                }
            }


            Console.WriteLine(scores.Max());
            Console.ReadKey();

        }


    }

}
