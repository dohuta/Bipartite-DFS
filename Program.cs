using MyAdjacencyList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;
/*
 * TODO check a graph is bipartite or not
 * --------------------------------------------------------------------------
 * Step 1: Take randomly vertex v thus set X:={v} and set Y:=null;
 * Step 2: repeat:
 *          Y:= Y ∪ neighbors-of(X);
 *          X:= X ∪ neighbors-of(Y);
 *         until (X ∩ Y ≠ null) or (X max and Y max - traversed all elements)
 * Step 3: If X ∩ Y ≠ null then the graph is not bipartite
 *         else the graph is bipartite
 *          X is the left set
 *          Y is the right set
 * --------------------------------------------------------------------------
 * 
 * In my work, I use an boolean array to sign the traverse vertex
 * and an int array to sign which side that vertex is (0 for right, 1 for left)
 * 
 */

namespace CS_B03
{
    class Program
    {
        static AdjacencyList ReadAdjacencyList(string filepath)
        {
            using (var sr = new StreamReader(filepath))
            {
                // Allocations
                var list = new AdjacencyList();
                var s1 = sr.ReadLine().Split(' ');
                list.Vertices = int.Parse(s1[0]);
                list.List = new LinkedList<int>[list.Vertices];


                // Read matrix
                for (int i = 0; i < list.Vertices; i++)
                {
                    string[] s = sr.ReadLine().Split(' ');

                    var tmp = new LinkedList<int>();
                    foreach (string e in s)
                    {
                        // catch the error if line content is not integers
                        if (int.TryParse(e, out int tmp2) == false)
                            if (tmp2 == 0)
                                continue;
                        tmp.AddLast(--tmp2);
                    }

                    list.List[i] = tmp;
                }

                return list;
            }
        }

        static bool CheckBipartite(AdjacencyList list, int source, ref bool[] checkpoint, ref int[] flag)
        {
            foreach (var item in list.List[source])
            {
                if (!checkpoint[item])
                {
                    checkpoint[item] = true;
                    if (flag[source] == 0)
                        flag[item] = 1;
                    else
                        flag[item] = 0;
                    if (!CheckBipartite(list, item, ref checkpoint, ref flag))
                        return false;
                }

                // If the vertex item is traversed 
                else if (flag[source] == flag[item])
                    return false;
            }

            // In the end, if there is nothing happened, return true
            return true;
        }

        static void ScanGraph(AdjacencyList list)
        {
            var checkpoint = new bool[list.Vertices];
            var flag = new int[list.Vertices];

            checkpoint[0] = true;
            flag[0] = 0;
            
            if(CheckBipartite(list, 0, ref checkpoint, ref flag))
            {
                WriteLine("Do thi phan doi.");
                WriteResult(true);
            }
            else
            {
                WriteLine("Do thi khong phan doi.");
                WriteResult(false);
            }
        }

        static void WriteResult(bool result)
        {
            using(var sw = new StreamWriter("..//..//dothiphandoi.out"))
            {
                if (result)
                    sw.Write("Do thi phan doi.");
                else
                    sw.Write("Do thi khong phan doi.");
            }
        }

        static void Main(string[] args)
        {
            // dothiphandoi.inp ; dothiphandoi1.inp is bipartite graph
            // dothiphandoi2.inp ; dothiphandoi3.inp is not bipartite graph
            var list = ReadAdjacencyList("..//..//dothiphandoi3.inp");
            WriteLine(list);

            ScanGraph(list);
        }
    }
}
