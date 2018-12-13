using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    class Day13
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\inputtest13.txt");
            var railway = new char[150, 150];
            var carts = new List<Cart>();
            var cartfilter = "<>^v";
            int i, j;
            i = j = 0;
            foreach (var row in data)
            {
                foreach (var part in row.ToCharArray())
                {
                    var track = part;
                    if (cartfilter.Contains(part))
                    {
                        var wrk = new Cart
                        {
                            x = i,
                            y = j,
                            direction = part
                        };



                    }

                    railway[i, j] = track;

                    j++;
                }
                i++;
            }



            Console.WriteLine("");

            Console.ReadKey();

        }
    }
        class Cart {
            public int x;
            public int y;
            public char direction;
            public char nextturn = 'L';
        }

}
