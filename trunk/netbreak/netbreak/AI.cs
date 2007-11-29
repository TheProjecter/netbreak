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
            	//Console.WriteLine(current.Ply);
                current = q.Dequeue();
                MoveNode[] expnd = expandNodes(current.Board, current, (current.Ply + 1));
                if(expnd.Length > 0) 
                    q.EnqueueArray(expnd);
            }

			
            while(current.Ply > 1)
            {
                current = current.PreviousMove;
            }
            //this will be the best move to make after expanding down to depth plys.
            int[] result = { current.Group.X, current.Group.Y };

            return result;
        }

        public double rankGrid(Grid board)
        {
            double rank = 0;
            PriorityQueue<Group> moves = board.calculateGroupsQueue();
            Group largest = moves.Dequeue();
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

            rank = (remaining * .25) + (largest.Bubbles*.35) - (singles*.15);

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
                    //if the board is a win situation, give it an arbitrarily high score
                    if(b.checkWin())
                        nodes.Add(new MoveNode(move, 100000, ply, b, last));

                    nodes.Add(new MoveNode(move, rankGrid(b), ply, b, last));
                }
            }
            return nodes.ToArray();
        }
    }
}