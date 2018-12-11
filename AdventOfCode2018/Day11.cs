using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day11
    {
        public static void Execute()
        { //9810
            var serialnumber = 9810;
            var loopsize = 300;
            var grid = new int[loopsize+1, loopsize+1];
            for (var x =1; x<loopsize; x++ )
            {
                for (var y = 1; y < loopsize; y++)
                {
                    var rack = x + 10;
                    var powerlevel = rack * y;
                    var fuel = powerlevel + serialnumber;
                    fuel *= rack;
                    grid[x, y] = fuel / 100%10 -5;

                }
            }

            var currentfuel = 0;
            var currentx = 0;
            var currenty = 0;
            var currentsquare = 0;
            for (var square = 1; square < 300; square++)
            {
                for (var x = 1; x < loopsize - square; x++)
                {
                    for (var y = 1; y < loopsize - square; y++)
                    {
                        var fuel = 0;


                        for (var i = 0; i < square; i++)
                        {
                            for (var j = 0; j < square; j++)
                            {
                                fuel += grid[x + i, y + j];
                            }
                        }
                        if (fuel > currentfuel)
                        {
                            currentfuel = fuel;
                            currentx = x;
                            currenty = y;
                            currentsquare = square;
                            Console.WriteLine(currentx + "," + currenty + "," + currentsquare + " " + currentfuel);
                        }

                    }
                }
            }


            Console.WriteLine(currentx + "," + currenty +"," +currentsquare + " " + currentfuel );
            Console.ReadKey();

        }


    }


}
