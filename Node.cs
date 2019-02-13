using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo1
{
    public class Node
    {
        public double x_coord, y_coord, weight = double.MaxValue;
        public int id, parent, indexInQueue;
        public string color = "w";
        public Dictionary<int, Tuple<double, double>> neighbours = new Dictionary<int, Tuple<double, double>>();
        public Node(int id, double x, double y)
        {
            this.x_coord = x;
            this.y_coord = y;
            this.id = id;
        }

    }
}
