using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
    class AI
    {

        public void makeNextMove(string[,] grid)
        {
            //make new node class for nodes expanded by board tree. this should thus hold the move, and score
            // moveNode(x,y, rank)
            PriorityQueue<int> q = new PriorityQueue<int>();


        }

        public int rankBoard()
        {
            //here goes my ranking algorithm!!
            return 0;
        }
    }
}