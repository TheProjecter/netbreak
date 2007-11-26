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
            int ply = 0;
            Stack<MoveNode> moves = new Stack<MoveNode>();
            PriorityQueue<MoveNode> q = new PriorityQueue<MoveNode>();
            q.initialize(expandNodes(board, ++ply));
            //moves.Push(board);

            while (moves.Peek().Ply < depth)
            {
                MoveNode move = q.Dequeue();
                moves.Push(move);
                q.EnqueueArray(expandNodes(move.Board, ++ply));
            }

            while (moves.Peek().Ply > 1)
            {
                moves.Pop();
            }
            //this will be the best move to make after expanding down to depth plys.
            MoveNode finalMove = q.Dequeue();
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

            while (moves.Peek().Bubbles > 0)
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
                b.removeGroup(move.X, move.Y);
                nodes.Add(new MoveNode(move, rankGrid(b), ply, b));
            }
            return nodes.ToArray();
        }
    }
}