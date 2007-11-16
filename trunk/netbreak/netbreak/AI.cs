using System;
using System.Collections.Generic;


//my initial concept for the AI from my intelligence report was to use a uniform cost search with iterative deepening
// the cost would be based on a function which takes into account number of nodes, number of singletons, size of large groups etc.
// This may be unfeasible, so I'm going to talk to the prof. and make sure its ok,and try to get a basic idea of how to implement it.
namespace Netbreak
{
	
	
	class AI
	{
        private int dimensions;

        public AI(int dim)
        {
            this.dimensions = dim;
        }

        public int[] getMove(string[][] grid)
        {
            string[][] nextGrid;
            int[] point = new int[2];

            Tree tree = new Tree(evaluateBoard(grid));
            SortedList<int, int> queue = new SortedList<int,int>();

            nextGrid = makeMove();
        }

        public int evaluateBoard(string[][] grid)
        {
            //do some crazy heuristic evaluation
            return 0;
        }

        public int countRemaining(string[][] grid)
        {
            //count how many remaining nodes we have
            return 0;
        }
	}
}

