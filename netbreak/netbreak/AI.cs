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
            PriorityQueue<Group> moves = board.calculateGroupsQueue();
            int largest = moves.Dequeue().Bubbles;
            int groups = 0;
            int singles = 0;
            int remaining = 0;

            while (!(moves.isEmpty) && (moves.Peek().Bubbles > 0))
            {
                groups++;
                remaining += moves.Dequeue().Bubbles;
            }

            singles = moves.Count;
            remaining += singles;

           // rank = (remaining * .25) + (largest * .35) - (singles * .50);
       
         // Experimental:
         // rank = ((remaining / (board.X * board.X)) * .25) + ((largest / remaining) * .35) - ((singles / remaining) * .4);
         
         // rank = (100 + (largest * .5)) - ((singles * .15) + (remaining * .35));
         //locked:
         //rank = ((board.X * board.X)) - singles;
         
         if(board.checkWin())
         	rank = ((board.X * board.X)/(remaining+1) * 100) *100;
         else if (board.checkLocked())
         	rank  = -10000000;
         else
        	rank = ((board.X * board.X)/(remaining+1) * 100) - (singles/(groups+1));
       
       		if(rank == ( ((board.X * board.X)/(remaining+1) * 100) *100))
       			Console.WriteLine("found win condition");
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