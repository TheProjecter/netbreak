using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class PriorityQueue<T>
        where T: IComparable
	{
        Tree<T> tree;

        public PriorityQueue()
        {
            tree = new Tree<T>();
        }

        public void Enqueue(T item)
        {
            tree.put(new TreeNode<T>(item));
        }

        public T Dequeue()
        {
            return tree.removeMax();
        }

        public bool isEmpty
        {
            get { return (tree.Root == null) ? true : false; }
        }
	}
}
