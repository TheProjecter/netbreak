using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class TreeNode<T>
        where T: IComparable
	{
        private T value;
        private int s;
        private TreeNode<T> left;
        private TreeNode<T> right;

        public TreeNode(T val)
        {
            this.value = val;
            Left = null;
            Right = null;
            s = 0;
        }

        public T Value
        {
            get { return value; }
        }

        public TreeNode<T> Left
        {
            get { return left; }
            set { left = value; }
        }
        
      	public TreeNode<T> Right
        {
            get { return right; }
            set { right = value; }
        }
        
        public int S
        {
        	get { return s; }
        	set { s = value; }
		}
	}
}
