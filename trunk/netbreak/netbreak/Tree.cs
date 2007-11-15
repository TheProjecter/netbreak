using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class Tree
	{
        private TreeNode root;
        private TreeNode currentNode;

        public Tree(int rootVal)
        {
            root = new TreeNode(rootVal);
            currentNode = root;
        }
	}
}
