using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class Tree<T>
        where T: IComparable
	{
        private TreeNode<T> root;

        public Tree()
        {
            root = null;
        }
        
        public void put(TreeNode<T> Node)
        {
            if (root == null)
            {
                root = Node;
            }
            else
            {
                Node.S = 1;
                root = meld(root, Node);
            }
        }
        
        public T removeMax()
        {
        	T result = root.Value;
        	Root = meld(root.Left, root.Right);
        	return result;
        }
        
        public TreeNode<T> meld(TreeNode<T> x, TreeNode<T> y)
        {
		  	if(x == null)
		    	return y;
		  	if(y == null) 
		    	return x;
		    if(x.Value.CompareTo(y.Value) > 0)
		    {
		    	TreeNode<T> temp = x;
		    	x = y;
		    	y = temp;
		    }
		    
		    x.Right = meld(x.Right, y);
		    
		    if(x.Left == null)
		    {
		    	x.Left = x.Right;
		    	x.Right = null;
		    	x.S =1;
		    } 
		    else
		    {
		    	if (x.Left.S < x.Right.S)
		   		{
		  		TreeNode<T> temp = x.Left;
		    	x.Left = x.Right;
		    	x.Right = temp;
		    	
		  		}
		  		x.S = x.Right.S + 1;
		  	}
		    return x;
        }

		public void initialize(T[] a)
		{
			Queue<TreeNode<T>> nodeQ = new Queue<TreeNode<T>>();
			for(int i=0; i< a.Length; i++)
			{
				nodeQ.Enqueue(new TreeNode<T>(a[i]));
			}
			
			while( nodeQ.Count != 1)
			{
				nodeQ.Enqueue(meld(nodeQ.Dequeue(), nodeQ.Dequeue()));
			}
			root = nodeQ.Dequeue();
		}
		
      	public TreeNode<T> Root
      	{
      		get { return root; }
      		set { value = root; }
      	}      	
	}
}
