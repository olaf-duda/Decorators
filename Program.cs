using System;
using nm = Lab1.NodesManipulator;
namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            INode[] nodes =
            {
               new Node("First"),
               new Node("Second"),
               new Node("Third"),
               new Node("Spare"),
               new Node("Green"),
               new Node("Blue"),
               new Node("Red"),
               new Node("Intersection"),
               new Node("Ring")
            };
			
			INode[] nodesDatabase =
            {
               new Node("First"),
               new Node("Second"),
               new Node("Third"),
               new Node("Spare"),
               new Node("Green"),
               new Node("Blue"),
               new Node("Red"),
               new Node("Intersection"),
               new Node("Ring")
            };

            foreach (var node in nodes)
            {
                TestPrintNode(node, 0 ,"");
            }

            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SmartNode\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[0], 1, "test");
                nodes[0] = nm.MakeNodeSmart(nodes[0]);
                TestPrintNode(nodes[0], 11, "test");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BrokenNode\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[1],1, "test");
                nodes[1] = nm.SetNodeToBroken(nodes[1], "testing");
                TestPrintNode(nodes[1],3, "test");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("EncryptionNode\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[2],1, "test");
                nodes[2] = nm.EncryptMessages(nodes[2], 2, 1);
                TestPrintNode(nodes[2],4, "test");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DecryptionNode\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[4],1, "test");
                nodes[4] = nm.DecryptMessages(nodes[4], 2);
                TestPrintNode(nodes[4],2, "vguv");
                Console.WriteLine("----------------------------------------------------------------------");
            }

            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[5],1, "test");
                nodes[5] = nm.DecryptMessages(nm.EncryptMessages(nm.EncryptMessages(nodes[5],  2,10), 3,10), 5);
                TestPrintNode(nodes[5], 2, "test");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("2\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[6],1, "test");
                nodes[6] = nm.SetNodeToBroken(nm.EncryptMessages(nm.DecryptMessages(nodes[6], 2),4, 3), "just broken");
                TestPrintNode(nodes[6], 3, "test");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[7],1, "test");
                nodes[7] = nm.EncryptMessages(nm.MakeNodeSmart(nodes[7]), 23, 5);
                TestPrintNode(nodes[7], 6, "");
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4\n");
                Console.ForegroundColor = ConsoleColor.White;
                TestPrintNode(nodes[8],1, "test");
                nodes[8] = nm.SetNodeToBroken(nm.DecryptMessages(nm.SetNodeToBroken(nodes[8], "very old node"), 5),"404");
                TestPrintNode(nodes[8], 4, "test");
                Console.WriteLine("----------------------------------------------------------------------");
            }
			
			//Part 2
			{
                for (int i = 0; i<nodesDatabase.Length; i++)
                {
                    nodesDatabase[i] = nm.DecryptMessages(nodesDatabase[i], 2);
                }
                for (int i = 0; i < nodesDatabase.Length; i++)
                {
                    nodesDatabase[i] = nm.SetNodeToBroken(nodesDatabase[i], "testing");
                }
                INode[] nodesSmartDatabase =
                {
                    new Node("First"),
                    new Node("Second"),
                    new Node("Third"),
                    new Node("Spare"),
                    new Node("Green"),
                    new Node("Blue"),
                    new Node("Red"),
                    new Node("Intersection"),
                    new Node("Ring")
                };
                for (int i = 0; i < nodesDatabase.Length; i++)
                {
                    nodesSmartDatabase[i] = nm.MakeNodeSmart(nodesSmartDatabase[i]);
                }
                for (int i = 0; i < nodesDatabase.Length; i++)
                {
                    if(nodesDatabase[i].GetName().StartsWith("Third") && nodesDatabase[i].GetCapacity() == 10)
                        TestPrintNode(nodesDatabase[i], 1, "test");
                }
                for (int i = 0; i < nodesSmartDatabase.Length; i++)
                {
                    if (nodesSmartDatabase[i].GetName().StartsWith("Third") && nodesSmartDatabase[i].GetCapacity() == 10)
                        TestPrintNode(nodesSmartDatabase[i], 1, "test");
                }
            }
        }

        private static void TestPrintNode(INode node, int tries, string message)
        {
            Console.WriteLine($"Name: {node.GetName()}");
            Console.WriteLine($"Capacity: {node.GetCapacity()}");
            Console.WriteLine($"Type: {node.GetNodeType()}");
            Console.WriteLine($"Capacity left: {node.GetCapacityLeft()}");
            if(tries >0)
                Console.WriteLine($"First try: {node.ProcessMessage(message)}");
            if (tries > 1)
            {
                for (int i = 0; i < tries - 2; i++)
                    node.ProcessMessage(message);
                Console.WriteLine($"Last try: {node.ProcessMessage(message)}");
            }
            Console.WriteLine("\n");
        }
        
    }
}