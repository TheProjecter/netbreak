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

        public T Peek()
        {
            return tree.Root.Value;
        }

        public void EnqueueArray(T[] a)
        {
            Tree<T> t = new Tree<T>();
            t.initialize(a);
            tree.Count = tree.Count + a.Length;
            tree.Root = tree.meld(tree.Root, t.Root);
            
        }

        public bool isEmpty
        {
            get { return (tree.Root == null) ? true : false; }
        }

        public void initialize(T[] a)
        {
            tree.initialize(a);
        }

        public int Count
        {
            get { return tree.Count; }
        }
	}
}
