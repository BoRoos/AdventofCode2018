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
            Part1();

            string[] data = File.ReadAllLines(@"c:\temp\input16part1.txt");
            Dictionary<int, Operation> opcodes = new Dictionary<int, Operation>();
            List<string> AllOpcodes = new List<string>() {
            "addr", "addi","mulr","muli","banr","bani","borr","bori","setr","seti","gtir","gtri","gtrr","eqir","eqri","eqrr"
            };
            int[] input, output;
            int totalops = 0;
            for(int i = 0; i<=data.Count()/4;i++)
            {
                Operation op;
                TestCommand tc = new TestCommand();
                string[] parts = data[i*4 + 0].Split(new char[] { '[', ',', ' ', ']' },StringSplitOptions.RemoveEmptyEntries);
                tc.input = new int[] { int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]) };
                
               
                parts = data[i*4 + 1].Split();
                int key = int.Parse(parts[0]);
                tc.Instruction = key;

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
                
                tc.A = int.Parse(parts[1]);
                tc.B = int.Parse(parts[2]);
                tc.C = int.Parse(parts[3]);
                
                parts = data[i*4 +2].Split(new char[] { '[', ',', ' ', ']' }, StringSplitOptions.RemoveEmptyEntries);
                tc.output = new int[] { int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]) };
                op.Commands.Add(tc);
                               
            }
            opcodes = opcodes.Values.OrderBy(o => o.Id).ToDictionary(k=>k.Id);
            while (AllOpcodes.Count() >0)
            {
                for(int i = 0; i<16; i++)

                {
                    var op = opcodes[i];
                    if(op.ValidOpcodes.Count() >1)
                    {
                        var listofcodes = op.ValidOpcodes.ToList();
                        foreach (string opcode in listofcodes)
                        {
                            foreach(var c in op.Commands)
                            {
                                int[] result = PerformInstruction(c.input, opcode, c.A, c.B, c.C);
                                if (!AllOpcodes.Contains(opcode) || result[0] != c.output[0] || result[1] != c.output[1] || result[2] != c.output[2] || result[3] != c.output[3])
                                {
                                    op.ValidOpcodes.Remove(opcode);
                                }
                            }
                        }
                        if (op.ValidOpcodes.Count() ==1)
                        {
                            op.Name = op.ValidOpcodes.First();
                            AllOpcodes.Remove(op.Name);
                        }
                    }
                    
                }

            }

            var Program = File.ReadAllLines(@"c:\temp\input16part2.txt");
            var register = new int[] { 0, 0, 0,0 };
            foreach(var row in Program)
            {
                var parts = row.Split();
                register = PerformInstruction(register, opcodes[int.Parse(parts[0])].Name, int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
            }
            Console.WriteLine(register[0]);

            Console.ReadKey();

        }
        static void Part1()
        {
            string[] data = File.ReadAllLines(@"c:\temp\input16part1.txt");
            Dictionary<int, Operation> opcodes = new Dictionary<int, Operation>();
            List<string> AllOpcodes = new List<string>() {
            "addr", "addi","mulr","muli","banr","bani","borr","bori","setr","seti","gtir","gtri","gtrr","eqir","eqri","eqrr"
            };
            int[] input, output;
            int totalops = 0;
            for (int i = 0; i <= data.Count() / 4; i++)
            {
                string[] parts = data[i * 4 + 0].Split(new char[] { '[', ',', ' ', ']' }, StringSplitOptions.RemoveEmptyEntries);
                input = new int[] { int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]) };


                Operation op;
                parts = data[i * 4 + 1].Split();
                int key = int.Parse(parts[0]);

                op = new Operation { Id = key }; //Part 1
                int inputA = int.Parse(parts[1]);
                int inputB = int.Parse(parts[2]);
                int inputC = int.Parse(parts[3]);

                parts = data[i * 4 + 2].Split(new char[] { '[', ',', ' ', ']' }, StringSplitOptions.RemoveEmptyEntries);
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


        }
        static int[] PerformInstruction(int[] registerin, string opcode, int a, int b, int c)
        {
            int[] registerout = new int[] { registerin[0], registerin[1], registerin[2], registerin[3] };
            switch (opcode)
            {
                case "addr":
                    registerout[c] = registerin[a] + registerin[b];
                    break;
                case "addi":
                    registerout[c] = registerin[a] + b;
                    break;
                case "mulr":
                    registerout[c] = registerin[a] * registerin[b];
                    break;
                case "muli":
                    registerout[c] = registerin[a] * b;
                    break;
                case "banr":
                    registerout[c] = registerin[a] & registerin[b];
                    break;
                case "bani":
                    registerout[c] = registerin[a] & b;
                    break;
                case "borr":
                    registerout[c] = registerin[a] | registerin[b];
                    break;
                case "bori":
                    registerout[c] = registerin[a] | b;
                    break;
                case "setr":
                    registerout[c] = registerin[a];
                    break;
                case "seti":
                    registerout[c] = a;
                    break;
                case "gtir":
                    registerout[c] = a > registerin[b] ? 1 : 0;
                    break;
                case "gtri":
                    registerout[c] = registerin[a] > b ? 1 : 0;
                    break;
                case "gtrr":
                    registerout[c] = registerin[a] > registerin[b] ? 1 : 0;
                    break;
                case "eqir":
                    registerout[c] = a == registerin[b] ? 1 : 0;
                    break;
                case "eqri":
                    registerout[c] = registerin[a] == b ? 1 : 0;
                    break;
                case "eqrr":
                    registerout[c] = registerin[a] == registerin[b] ? 1 : 0;
                    break;
                default:
                    break;
            }
            return registerout;
        }
    }
    class Operation
    {
        public int Id = 0;
        public string Name = "";
        public List<TestCommand> Commands= new List<TestCommand>();
        public List<string> ValidOpcodes = new List<string>() {
            "addr", "addi","mulr","muli","banr","bani","borr","bori","setr","seti","gtir","gtri","gtrr","eqir","eqri","eqrr"
            };
        
    }
    class TestCommand
    {
        public int[] input, output;
        public int Instruction;
        public int A, B, C;
    }
}
