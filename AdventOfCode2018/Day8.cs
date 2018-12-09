using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day8
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input8.txt");
            var parts = data[0].Split(' ').ToList();
            var counter = 1;
            var Nodes = new List<Node>();
            Node current = null; ;
            while (parts.Count() > 0)
            {
                if (current is null || current.Children > current.ListChildren.Count())
                {
                    var wrk = new Node
                    {
                        NodeNumber = counter++,
                        Children = int.Parse(parts[0]),
                        Entries = int.Parse(parts[1]),
                        parent = current
                    };
                    Nodes.Add(wrk);

                    current = wrk;
                    parts.RemoveAt(0);
                    parts.RemoveAt(0);
                }
                else
                {
                    for (var i = 0; i< current.Entries; i++) {
                        current.ListEntries.Add(int.Parse(parts[0]));
                        parts.RemoveAt(0);
                    }
                    if (current.parent != null)
                    {
                        current.parent.ListChildren.Add(current);
                        current = current.parent;
                    }
                }
                                
            }

            var total = Nodes.Sum(n => n.ListEntries.Sum());

            Console.WriteLine(total);
            var cur = Nodes[0];
            while (cur.Calculated == false)
            {
                if(cur.Children ==0)
                {
                    cur.Value = cur.ListEntries.Sum();
                    cur.Calculated =true;
                    if (cur.parent != null) cur = cur.parent;
                }
                else
                {
                    cur.Value = 0;
                    var NodeisDone = true;
                    foreach(var c in cur.ListEntries)
                    {
                        if (c <= cur.Children)
                        {
                            if (cur.ListChildren[c-1].Calculated)
                            {
                                cur.Value += cur.ListChildren[c-1].Value;
                            }
                            else
                            {
                                cur = cur.ListChildren[c-1];
                                NodeisDone = false;
                                break;
                            }
                        }
                    }
                    if (NodeisDone)
                    {
                        cur.Calculated = true;
                        if (cur.parent != null) cur = cur.parent;
                    }

                }
            }

            Console.WriteLine(cur.Value);
            Console.ReadKey();

        }

    }
    class Node
    {
        public int NodeNumber;
        public Node parent;
        public int Children;
        public int Entries;
        public List<Node> ListChildren = new List<Node>();
        public List<int> ListEntries = new List<int>();
        public int Value;
        public Boolean Calculated = false;
    }
}
