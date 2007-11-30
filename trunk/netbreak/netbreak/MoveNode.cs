using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	class MoveNode : IComparable
	{
        private Group group;
        private double rank;
        private int ply;
        private Grid board;
        private MoveNode previousMove;

        public MoveNode()
        {
            group = new Group(0, 0, 0);
            rank = 0;
            ply = 0;
            board = null;
            previousMove = null;
        }
        
        public MoveNode(Group m, double rank, int ply, Grid board, MoveNode last)
        {
            this.group = m;
            this.rank = rank;
            this.ply = ply;
            this.board = board;
            previousMove = last;

        }

        public MoveNode(int x, int y, double rank)
        {
            group = new Group(x, y, 0);
            this.rank = rank;
            ply = 0;
            board = null;
            previousMove = null;
        }

        public Group Group
        {
            get { return group; }
            set { group = value; }
        }

        public double Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public int Ply
        {
            get { return ply; }
            set { ply = value; }
        }

        public Grid Board
        {
            get { return board; }
        }
        
        public MoveNode PreviousMove
        {
        	get { return previousMove; }
        	set { previousMove = value; }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            MoveNode m = (MoveNode)obj;
            double r = m.Rank;
            double result = rank - r;
            if (result > 0)
                return 1;
            if (result < 0)
                return -1;
            return 0;
        }

        #endregion
    }
}
