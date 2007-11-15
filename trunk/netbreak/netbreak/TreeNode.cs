using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class TreeNode
	{
        private int value;
        private List<TreeNode> children;

        public TreeNode(int val)
        {
            this.value = val;
            children = new List<TreeNode>();
        }

        public int Value
        {
            get { return value; }
        }

        public List<TreeNode> Children
        {
            get { return children; }
            set { children = value; }
        }
	}
}
