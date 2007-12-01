using System;
using System.Collections.Generic;
using System.Text;
namespace Netbreak
{
    class AI
    {
        private int depth;

        public AI(int depth)
        {
            this.depth = depth;
        }


		public MoveNode solveTree(Grid board)
		{
				MoveNode result = null;
			    Stack<MoveNode> s = new Stack<MoveNode>();
        		MoveNode[] expnd = expandNodes(board, null, 1);
        		for(int i=0; i<expnd.Length; i++)
        		{
        			s.Push(expnd[i]);
        		}        	
        		
        		MoveNode current = s.Pop();
        		while(!(current.Board.checkWin()) && (s.Count > 0))
        		{
        			expnd = expandNodes(current.Board, current, current.Ply+1); 
	        		for(int j=0; j<expnd.Length; j++)
	        		{
	        			s.Push(expnd[j]);
	        		}
	        		current = s.Pop();
        		}
        		
        		if(current.Board.checkWin())
        			result = current;
        		
        		return result;
		}
		
        public int[] makeNextMove(Grid board)
        {
	   		PriorityQueue<MoveNode> q = new PriorityQueue<MoveNode>();
	      	q.initialize(expandNodes(board, null, 1));
	       	MoveNode current = q.Dequeue();

	      	while ((current.Ply < depth) && !(q.isEmpty))
	      	{
	       		MoveNode next = q.Dequeue();
	           	if(next.Rank> current.Rank)
	           	{
	            	current = next;
	            	MoveNode[] expnd = expandNodes(current.Board, current, (current.Ply + 1));
	                if(expnd.Length > 0) 
	                    q.EnqueueArray(expnd);
	            }
	     	}

	      	while(current.Ply > 1)
	      	{
	       		current = current.PreviousMove;
	      	}
	     	//this will be the best move to make after expanding down to depth plys.
	      	int[] result = { current.Group.X, current.Group.Y };
	      	Console.WriteLine("rank: " + current.Rank);
	       	
	       	return result;
        }

        public double rankGrid(Grid board)
        {
            double rank = 0;
            int largest = 0;
            
            PriorityQueue<Group> moves = board.calculateGroupsQueue();
            if(!moves.isEmpty)
            	largest = moves.Dequeue().Bubbles;
            	
            int groups = 0;
            int singles = 0;
            int remaining = 0;

            while (!(moves.isEmpty) && (moves.Peek().Bubbles > 1))
            {
                groups++;
                remaining += moves.Dequeue().Bubbles;
            }

            singles = moves.Count;
            remaining += singles;

         if(board.checkWin())
         	rank = (board.X * board.X) *1000;
         else if (board.checkLocked())
         	rank  = 0 - singles;
         else
         	rank = (remaining - singles) + largest;
        	
      	return rank;

        }

        public MoveNode[] expandNodes(Grid board, MoveNode last, int ply)
        {
            List<MoveNode> nodes = new List<MoveNode>();
            List<Group> moves = board.calculateGroups();

            foreach(Group move in moves)
            {
                Grid b = board.copy();
                if (b.checkMove(move.X, move.Y))
                {
                    b.removeGroup(move.X, move.Y);
                    b.compressGrid();
					nodes.Add(new MoveNode(move, rankGrid(b), ply, b, last));
                }
            }
            return nodes.ToArray();
        }
    }
}