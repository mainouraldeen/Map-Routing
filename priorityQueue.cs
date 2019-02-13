using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo1
{
    class priorityQueue
    {
        List<Node> queue;
        public int siz;

        public priorityQueue()
        {
            queue = new List<Node>();
            siz = 0;
        }
        public void Enqueue(Node item)//
        {
            siz++;
            queue.Add(item);
            int newItemIndex = siz - 1;
            heapifyUp(newItemIndex);
        }

        public void heapifyUp(int newItemIndex)//
        {
            while (newItemIndex > 0 && queue[newItemIndex].weight < queue[ParentIndex(newItemIndex)].weight)
            {
                int temppqindex = queue[newItemIndex].indexInQueue;
                queue[newItemIndex].indexInQueue = queue[ParentIndex(newItemIndex)].indexInQueue;
                queue[ParentIndex(newItemIndex)].indexInQueue = temppqindex;

                Node temp = queue[newItemIndex];
                queue[newItemIndex] = queue[ParentIndex(newItemIndex)];
                queue[ParentIndex(newItemIndex)] = temp;
                newItemIndex = ParentIndex(newItemIndex);

            }
        }
        public bool IsEmpty()//
        {
            if (siz == 0)
                return true;
            else
                return false;
        }

        public Node GetMin()//
        {
            return queue[0];
        }

        public int LeftIndex(int index)//
        {
            return (index * 2) + 1;
        }
        public int RightIndex(int index)//
        {
            return (index * 2) + 2;
        }
        public int ParentIndex(int index)//
        {
            return (index - 1) / 2;
        }

        public void Dequeue()//
        {
            Node root = queue[0];
            queue[0] = queue[siz - 1];
            queue[0].indexInQueue = 0;
            siz--;
            queue.RemoveAt(siz);
            HeapifyDown(0);
        }
        public void HeapifyDown(int index)
        {
            int right = RightIndex(index);
            int left = LeftIndex(index);
            int smallest = index;
            //int temppqindex = queue[index].indexInQueue;

            if (left < siz && queue[left].weight < queue[smallest].weight)
            {
                smallest = left;
                // temppqindex = queue[left].indexInQueue;
            }
            if (right < siz && queue[right].weight < queue[smallest].weight)
            {
                smallest = right;
                // temppqindex = queue[right].indexInQueue;
            }
            if (smallest != index)// d5l f if mn dol
            {
                int temppqindex = queue[smallest].indexInQueue;
                queue[smallest].indexInQueue = queue[index].indexInQueue;
                queue[index].indexInQueue = temppqindex;

                Node temp = queue[smallest];
                queue[smallest] = queue[index];
                queue[index] = temp;

                HeapifyDown(smallest);

            }
        }
    }

}
