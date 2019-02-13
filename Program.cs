using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace algo1
{
    class Program
    {
        public static int maxID = 0;//to set source ID
        public const double manVelocity = 5;
        public static double walkingFromSource = 0, walkingToDistination = 0;
        public static List<int> shortestPath = new List<int>();
        public static List<string> allLines = new List<string>();
        public static Dictionary<int, Node> nodeList = new Dictionary<int, Node>();
        public static List<Tuple<int, double>> nearestToSource = new List<Tuple<int, double>>();
        public static List<Tuple<int, double>> nearestToDestination = new List<Tuple<int, double>>();


        //edited
        public static void add_Source_Destination(Node source, Node destination, int originalNodesSize)
        {
            nodeList[maxID + 1] = destination;
            nodeList[maxID + 2] = source;

            for (int i = 0; i < nearestToDestination.Count; i++)
            {
                // bt7ot el neighbours fel destination
                Tuple<double, double> tuple = new Tuple<double, double>((nearestToDestination[i].Item2), manVelocity);
                nodeList[originalNodesSize].neighbours.Add(nearestToDestination[i].Item1, tuple);
                // bn7ot id el destination f kol l neighbours 3shan hya undirected gragh
                // nodeCount becouse destination howa el element el abl el a5eer
                nodeList[nearestToDestination[i].Item1].neighbours.Add(originalNodesSize, tuple);

            }

            for (int i = 0; i < nearestToSource.Count; i++)
            {
                // bt7ot el neighbours fel source
                Tuple<double, double> tuple = new Tuple<double, double>((nearestToSource[i].Item2), manVelocity);
                // bn7ot id el source f kol l neighbours 3shan hya undirected gragh
                // nodeCount+1 becouse source howa el element el a5eer
                nodeList[originalNodesSize + 1].neighbours.Add(nearestToSource[i].Item1, tuple);
                nodeList[nearestToSource[i].Item1].neighbours.Add(originalNodesSize + 1, tuple);

            }

        }
        public static void dijkstra(priorityQueue pq)
        {
            while (!pq.IsEmpty())//v homa 3shan 3mleen el tre2a el tanya enhom msh by7oto kol el nodes
            {
                Node minNode = pq.GetMin();
                if (minNode.color != "b")//theta (e' log v')
                {
                    //element: shayl id-->.key, w TUPLE-->.value(.value.Item1, .value.Item2)
                    foreach (KeyValuePair<int, Tuple<double, double>> element in minNode.neighbours)//e
                    {
                        nodeList[element.Key].color = "g";
                        double time = element.Value.Item1 / element.Value.Item2;
                        //relaxation
                        if (nodeList[element.Key].weight > time + (minNode.weight))
                        {
                            nodeList[element.Key].weight = time + (minNode.weight);
                            //set neighbours parent to minNode ID
                            nodeList[element.Key].parent = minNode.id;
                            pq.heapifyUp(nodeList[element.Key].indexInQueue);//log v
                        }
                    }
                    minNode.color = "b";
                    //speeding up Dijkstra: law el minNode hia el distination, w 5lst kol el neighbours bto3ha(black)
                    // f ana msh m7taga akml ba2y el nodes                  
                    if (minNode.id == maxID + 1)//destination
                    {
                        break;
                    }
                }
                pq.Dequeue();//theta of v
            }
        }
        public static double getTotalDistance()
        {
            // currentID: initially the distination
            int parentID, currentID = maxID + 1;

            double totalDistance = 0;
            while (true)
            {
                parentID = nodeList[currentID].parent;
                if (parentID == -1)// en dh el source malosh parent
                {
                    break;
                }
                //bmshy 3la el neighbours w adwr 3la el parent 3shan a5od el masafa ely benhom
                foreach (KeyValuePair<int, Tuple<double, double>> element in nodeList[currentID].neighbours)
                {
                    if (nodeList[parentID].parent == -1)
                    {
                        walkingFromSource = element.Value.Item1;
                    }
                    if (nodeList[currentID].id == maxID + 1)
                    {
                        walkingToDistination = element.Value.Item1;
                    }
                    if (element.Key == parentID)
                    {
                        //shortest path
                        /* if(currentID!=maxID+1)
                         shortestPath.Add(currentID);*/
                        //da or da
                        if (parentID != maxID + 2)
                            shortestPath.Add(parentID);

                        totalDistance += element.Value.Item1;
                        break;
                    }
                }

                currentID = parentID;
            }
            //a5r wa7d byt7at b index ana msh 3ayzah fa ha overwrite 3aleh el destination
            // shortestPath[shortestPath.Count - 1] = maxID + 2; //el list w hya bt add btzawd 4 amakn 4 amakn fna msh 3arfa hwa anhy

            getVehicleDistance(totalDistance);
            return totalDistance;
        }


        public static double getVehicleDistance(double totalDistance)
        {
            return totalDistance - (walkingFromSource + walkingToDistination);
        }

        public static string[] readFile(StreamReader mapFile)
        {
            string wholeFile = mapFile.ReadToEnd();
            mapFile.Close();
            string[] Lines = wholeFile.Split('\n');
            return Lines;
        }
        //edited
        public static void constructGraph(string[] lines)
        {
            int originalNodesSize = int.Parse(lines[0]);
            // create nodes without distenation and source
            for (int i = 1; i <= originalNodesSize; i++)
            {
                string[] line = lines[i].Split(' ');
                Node newNode = new Node(int.Parse(line[0]), double.Parse(line[1]), double.Parse(line[2]));
                //int.Parse(line[0])-->el id bta3 el node b7otoh key f-el dic
                nodeList[int.Parse(line[0])] = newNode;
                maxID = Math.Max(maxID, int.Parse(line[0]));//what this line do ? da 3shan olna msh mtrtb wkda bs lw kda da hykon esm shr3 :/
            }
            // by2ra mn b3d el original size b 1
            int edgesCount = int.Parse(lines[originalNodesSize + 1]);
            int index = originalNodesSize + 2;

            // read edges
            while (edgesCount > 0)
            {
                string[] line = lines[index].Split(' ');
                int firstId = int.Parse(line[0]);
                int secondId = int.Parse(line[1]);
                double distance = double.Parse(line[2]);
                double velocity = double.Parse(line[3]);
                Tuple<double, double> dis_vel = new Tuple<double, double>(distance, velocity);

                nodeList[firstId].neighbours.Add(secondId, dis_vel);
                nodeList[secondId].neighbours.Add(firstId, dis_vel);
                ++index;
                --edgesCount;
            }
        }

        public static void remove_Source_Destination()
        {
            Node sourceNode = nodeList[maxID + 2];
            Node destinationNode = nodeList[maxID + 1];
            //nodeList.RemoveAt(nodeList.Count - 1);
            nodeList.Remove(sourceNode.id);
            nodeList.Remove(destinationNode.id);
            foreach (KeyValuePair<int, Tuple<double, double>> element in sourceNode.neighbours)
            {
                // bd5ol fel neighbours bta3 l source w amsa7 id el source mnhom
                nodeList[element.Key].neighbours.Remove(sourceNode.id);
            }
            foreach (KeyValuePair<int, Tuple<double, double>> element in destinationNode.neighbours)
            {
                // bd5ol fel neighbours bta3 l destination w amsa7 id el destination mnhom
                nodeList[element.Key].neighbours.Remove(destinationNode.id);
            }

            foreach (KeyValuePair<int, Node> element in nodeList)
            {
                nodeList[element.Key].weight = double.MaxValue;
                nodeList[element.Key].color = "w";
            }
        }
        public static void nodesWithinRadius(int originalNodesSize, double source_X, double source_Y, double destination_X, double destination_Y, double radius)
        {
            // searching for valid nodes in the radius range , nearest to the source
            for (int j = 0; j < originalNodesSize; j++)
            {
                double subResX = nodeList[j].x_coord - source_X;
                double subResY = nodeList[j].y_coord - source_Y;
                subResX *= subResX;
                subResY *= subResY;
                double distance = Math.Sqrt(subResX + subResY);
                if (distance <= radius)
                {
                    Tuple<int, double> T = new Tuple<int, double>(nodeList[j].id, distance);
                    nearestToSource.Add(T);
                }

                // searching for valid nodes in the radius range , nearest to the destination
                subResX = nodeList[j].x_coord - destination_X;
                subResY = nodeList[j].y_coord - destination_Y;
                subResX *= subResX;
                subResY *= subResY;
                distance = Math.Sqrt(subResX + subResY);
                if (distance <= radius)
                {
                    Tuple<int, double> T = new Tuple<int, double>(nodeList[j].id, distance);
                    nearestToDestination.Add(T);
                }
            }
        }

        static void Main(string[] args)
        {
            Stopwatch totalExecutionTime = Stopwatch.StartNew();

            StreamReader mapFile;
            mapFile = new StreamReader("map1.txt");
            Console.SetIn(mapFile);
            //call function readFile to read nodes and edges
            string[] mapLines = readFile(mapFile);
            int originalNodesSize = int.Parse(mapLines[0]);

            mapFile = new StreamReader("queries1.txt");
            Console.SetIn(mapFile);
            // call function readFile to read queries
            string[] queryLines = readFile(mapFile);
            int queryCount = int.Parse(queryLines[0]);


            constructGraph(mapLines);


            double source_X = 0, source_Y = 0, destination_X = 0, destination_Y = 0, radius = 0;

            Stopwatch queryExecutionTime = Stopwatch.StartNew();

            for (int i = 1; i <= queryCount; i++)
            {
                string[] line = queryLines[i].Split(' ');
                source_X = double.Parse(line[0]);
                source_Y = double.Parse(line[1]);
                destination_X = double.Parse(line[2]);
                destination_Y = double.Parse(line[3]);
                radius = double.Parse(line[4]);
                radius /= 1000;

                nodesWithinRadius(originalNodesSize, source_X, source_Y, destination_X, destination_Y, radius);

                //adding destination node and source node to the graph
                Node destinationNode = new Node(maxID + 1, destination_X, destination_Y);
                Node sourceNode = new Node(maxID + 2, source_X, source_Y);
                sourceNode.weight = 0;
                sourceNode.parent = -1;
                add_Source_Destination(sourceNode, destinationNode, originalNodesSize);

                // construct el min-heap
                priorityQueue pq = new priorityQueue();
                foreach (KeyValuePair<int, Node> element in nodeList)
                {
                    element.Value.indexInQueue = pq.siz;
                    //bmshy 3la el nodeList w ba5od id el dictionary w ab3t el value bta3toh (Node)
                    pq.Enqueue(nodeList[element.Key]);
                }

                //get min time
                dijkstra(pq);
                double totalDistance = getTotalDistance();
                //shortest path
                string shortestPathStr = "";
                for (int p = shortestPath.Count - 1; p >= 0; p--)
                {
                    if (p != 0)
                        shortestPathStr += shortestPath[p].ToString() + " ";
                    else
                        shortestPathStr += shortestPath[p];
                }


                //Console.WriteLine("shortestpath " + shortestPathStr);
                double totalTime = nodeList[maxID + 1].weight;
                totalTime *= 60;
                //Console.WriteLine("Time = " + totalTime.ToString("F2") + " mins");
                //Console.WriteLine("Distance = " + totalDistance.ToString("F2") + " km");
                //Console.WriteLine("Walking Distance = " + (walkingFromSource + walkingToDistination).ToString("F2") + " km");
                //Console.WriteLine("Vehicle Distance = " + getVehicleDistance(totalDistance).ToString("F2") + " km");
                //Console.WriteLine();
                //Console.WriteLine("Executoin Time: " + queryExecutionTime.ElapsedMilliseconds + " ms");
                //Console.WriteLine();

                //writing in file

                allLines.Add(shortestPathStr.ToString());
                allLines.Add(totalTime.ToString("F2") + " mins");
                allLines.Add(totalDistance.ToString("F2") + " km");
                allLines.Add((walkingFromSource + walkingToDistination).ToString("F2") + " km");
                allLines.Add(getVehicleDistance(totalDistance).ToString("F2") + " km");
                allLines.Add("\n");

                //msh 3ayz kda 3ayz ytal3 el total time l kol el queiries
               // allLines.Add(queryExecutionTime.ElapsedMilliseconds.ToString() + " ms");
                //if (i != queryCount)
                //    allLines.Add("\n");



                //remove source and destination from the graph
                remove_Source_Destination();
                nearestToDestination.Clear();//O(S)
                nearestToSource.Clear();//O(D)
                shortestPath.Clear();
            }//num of queries
            queryExecutionTime.Stop();//homa ele alo kda :'D
            allLines.Add(queryExecutionTime.ElapsedMilliseconds.ToString() + " ms");
           // allLines.Add("\n");

            totalExecutionTime.Stop();
            allLines.Add("\n");

            //Console.WriteLine(totalExecutionTime.ElapsedMilliseconds);
            allLines.Add(totalExecutionTime.ElapsedMilliseconds.ToString()+" ms");
            System.IO.File.WriteAllLines("C:\\Users\\Mai Nour Al-Deen\\Desktop\\map1output.txt", allLines);
        }

    }

}
