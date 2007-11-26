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
            Stack<MoveNode> moves = new Stack<MoveNode>();
            PriorityQueue<MoveNode> q = new PriorityQueue<MoveNode>();
            q.initialize(expandNodes(board, 1));
            moves.Push(q.Dequeue());
            Console.Write("");

            while ((moves.Peek().Ply < depth) && !(q.isEmpty))
            {
                MoveNode move = q.Dequeue();
                moves.Push(move);
                MoveNode[] expnd = expandNodes(move.Board, move.Ply + 1);
                if(expnd.Length > 0) 
                    q.EnqueueArray(expnd);
            }

            while (moves.Peek().Ply > 1)
            {
                moves.Pop();
            }
            //this will be the best move to make after expanding down to depth plys.
            MoveNode finalMove = moves.Pop();
            int[] result = { finalMove.Group.X, finalMove.Group.Y };

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

        public MoveNode[] expandNodes(Grid board, int ply)
        {
            List<MoveNode> nodes = new List<MoveNode>();
            List<Group> moves = board.calculateGroups();

            foreach(Group move in moves)
            {
                Grid b = board.copy();
                if (b.checkMove(move.X, move.Y))
                {
                    b.removeGroup(move.X, move.Y);
                    nodes.Add(new MoveNode(move, rankGrid(b), ply, b));
                }
            }
            return nodes.ToArray();
        }
    }
}