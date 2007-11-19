using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class Tree
	{
        private TreeNode root;

        public Tree()
        {
            root = null;
        }
        
        public void put(TreeNode Node)
        {
        	Node.S = 1;
        	meld(root, Node);
        }
        
        public int removeMax()
        {
        	int result = root.Value;
        	Root = meld(root.Left, root.Right);
        	return result;
        }
        
        public TreeNode meld(TreeNode x, TreeNode y)
        {
		  	if(x == null)
		    	return y;
		  	if(y == null) 
		    	return x;
		    	
		    if(x.Value > y.Value)
		    {
		    	TreeNode temp = x;
		    	x = y;
		    	y = temp;
		    }
		    
		    x.Right = meld(x.Right, y);
		    
		    if(x.Left == null)
		    {
		    	x.Left = x.Right;
		    	x.Right == null;
		    	x.S =1;
		    } 
		    else
		    {
		    	if (x.Left.S < x.Right.S)
		   		{
		  		TreeNode temp = x.Left;
		    	x.Left = x.Right;
		    	x.Right = temp;
		    	
		  		}
		  		x.S = x.Right.S + 1;
		  	}
		    return x;
        }

		public void initialize(int[] a)
		{
			Queue<TreeNode> nodeQ = new Queue<TreeNode>();
			for(int i=0; i< a.length; i++)
			{
				nodeQ.Enqueue(new TreeNode(a[i]));
			}
			
			while( nodeQ.Count != 1)
			{
				nodeQ.Enqueue(meld(nodeQ.Dequeue(), nodeQ.Dequeue()));
			}
			root = nodeQ.Dequeue();
		}
		
      	public TreeNode Root
      	{
      		get (return root; }
      		set { value = root; }
      	}      	
	}
}
