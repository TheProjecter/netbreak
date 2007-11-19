using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class TreeNode
	{
        private int value;
        private int s;
        private TreeNode left;
        private TreeNode right;

        public TreeNode(int val)
        {
            this.value = val;
            Left = null;
            Right = null;
            s = null;
        }

        public int Value
        {
            get { return value; }
        }

        public TreeNode Left
        {
            get { return left; }
            set { left = value; }
        }
        
      	public TreeNode Right
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
