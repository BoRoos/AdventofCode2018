using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day16
    {
        public static void Execute()
        {
            string[] data = File.ReadAllLines(@"c:\temp\input16part1.txt");
            Dictionary<int, Operation> opcodes = new Dictionary<int, Operation>();
            List<string> AllOpcodes = new List<string>() {
            "addr", "addi","mulr","muli","banr","bani","borr","bori","setr","seti","gtir","gtri","gtrr","eqir","eqri","eqrr"
            };
            int[] input, output;
            int totalops = 0;
            for(int i = 0; i<=data.Count()/4;i++)
            {
                string[] parts = data[i*4 + 0].Split(new char[] { '[', ',', ' ', ']' },StringSplitOptions.RemoveEmptyEntries);
                input = new int[] { int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]) };


                Operation op;
                parts = data[i*4 + 1].Split();
                int key = int.Parse(parts[0]);

                if (opcodes.ContainsKey(key))
                {
                    op = opcodes[key];
                }
                else
                {
                    op = new Operation
                    {
                        Id = key
                    };
                    opcodes.Add(key, op);
                }
                //op = new Operation { Id = key }; //Part 1
                int inputA = int.Parse(parts[1]);
                int inputB = int.Parse(parts[2]);
                int inputC = int.Parse(parts[3]);
                
                parts = data[i*4 +2].Split(new char[] { '[', ',', ' ', ']' }, StringSplitOptions.RemoveEmptyEntries);
                output = new int[] { int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]) };
                foreach (string opcode in AllOpcodes)
                {
                    if (op.ValidOpcodes.Contains(opcode))
                    {
                        int[] result = PerformInstruction(input, opcode, inputA, inputB, inputC);
                        if (result[0] != output[0] || result[1] != output[1] || result[2] != output[2] || result[3] != output[3])
                        {
                            op.ValidOpcodes.Remove(opcode);
                        }
                    }

                }
                if (op.ValidOpcodes.Count() >= 3) totalops++;
            }

            Console.WriteLine(totalops);

            // PArt2;
            foreach(var op in opcodes.OrderBy(o => o.Key))
            {
                Console.Write(op.Key + " ");
                foreach(var s in op.Value.ValidOpcodes)
                {
                    Console.Write( s + ", ");
                }
                Console.WriteLine();
            }


            Console.ReadKey();

        }
        static int[] PerformInstruction(int[] registerin, string opcode, int a, int b, int c)
        {
            int[] registerout = new int[] { registerin[0], registerin[1], registerin[2], registerin[3] };
            int aVal = a;
            int bVal = b;
            int outval = 0;
            switch (opcode)
            {
                case "addr":
                case "mulr":
                case "banr":
                case "borr":
                case "gtrr":
                case "eqrr":
                    aVal = registerin[a];
                    bVal = registerin[b];
                    break;
                case "addi":
                case "bani":
                case "bori":
                case "setr":
                case "gtri":
                case "eqri":
                    aVal = registerin[a];
                    break;
                case "gtir":
                case "eqir":
                    bVal = registerin[b];
                    break;
                default:
                    break;
            }

            switch (opcode.Substring(0, 3))
            {
                case "add":
                    outval = aVal + bVal;
                    break;
                case "mul":
                    outval = aVal * bVal;
                    break;
                case "ban":
                    outval = aVal & bVal;
                    break;
                case "bor":
                    outval = aVal | bVal;
                    break;
                case "set":
                    outval = aVal;
                    break;
                case "gti":
                case "gtr":
                    outval = (aVal > bVal ? 1 : 0);
                    break;
                case "eqi":
                case "eqr":
                    outval = (aVal == bVal ? 1 : 0);
                    break;
                    
                default:
                    break;
            }
            registerout[c] = outval;
            return registerout;
        }
    }
    class Operation
    {
        public int Id = 0;
        public List<string> ValidOpcodes = new List<string>() {
            "addr", "addi","mulr","muli","banr","bani","borr","bori","setr","seti","gtir","gtri","gtrr","eqir","eqri","eqrr"
            };
        
    }
}
