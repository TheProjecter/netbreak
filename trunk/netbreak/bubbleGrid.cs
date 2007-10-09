//TODO:   
//  - Write Constructor to initialize grid **
//  - Adapt removeConnected() to array rep. -- This is going to need to check boundaries and then check neighbors
//  - Adapt CheckLocked and CheckWin for array

using System;
using System.Collections;

namespace bubblebreak {

	public class bubbleGrid {
		
		private int[][] grid;
		private int height;
		private int width;
		
		public bubbleGrid(int x, int y) {
			height = y;
			width = x;
			
			Random rand = new Random();
			for(int i=1; i<=x; i++) {
				for(int j=1; j<=y; j++) {
					grid[i][j] = rand.Next(4);
				}
			}
			
		}
		
		
		public bubbleNode Get(int x, int y) {
           checkBounds(x,y);
           return grid[x][y];
		}
	
		//method removes node and returns a reference to the removed node
		public int remove(int x, int y) {
			checkBounds(x,y);
			int removed = grid[x][y];
			for(int i=y; i<grid[x].Length-1; i++) {
				grid[x][i] = grid[x][i+1];
			}
			grid[x][grid[x].Length-1] = 0;
			return removed;
		}


        public void removeConnected(x,y) {
            checkBounds(x,y);
            
            //if deleted is not true, then there is not more than one of this color and nothing should be removed
            bool deleted = false;
            
            //check left
            if(grid[x-1][y] != null && startNode.Left.Color == startNode.Color) {
                removeConnected(startNode.Left);
                deleted = true;
            }

            //check right
            if (startNode.Right != null && startNode.Right.Color == startNode.Color) {
                removeConnected(startNode.Right);
                deleted = true;
            }

            //check up
            if (startNode.Up != null && startNode.Up.Color == startNode.Color) {
                removeConnected(startNode.Up);
                deleted = true;
            }

            //check down
            if (startNode.Down != null && startNode.Down.Color == startNode.Color) {
                removeConnected(startNode.Down);
                deleted = true;
            }
            if (deleted) {
                remove(startNode);
            }
        }

        public void checkBounds(int x, int y) {
            if(x<=0 || x>width || y<=0 || y>height[x]) {
                throw new ArgumentOutOfRangeException();
            }
        }

        public bool checkWin() {
            if (width == 0) {
                return true;
            } else {
                return false;
                }
        }

        public bool checkLocked(bubbleNode startNode) {

            if (startNode.Left != null && ((startNode.Left.Color == startNode.Color) || (checkLocked(startNode.Left)))) {
                return true;
            }
            else if (startNode.Right != null && ((startNode.Right.Color == startNode.Color) || (checkLocked(startNode.Right)))) {
                return true;
            }
            else if (startNode.Up != null && ((startNode.Up.Color == startNode.Color) || (checkLocked(startNode.Up)))) {
                return true;
            }
            else if (startNode.Down != null && ((startNode.Down.Color == startNode.Color) || (checkLocked(startNode.Down)))) {
                return true;
            } else {
                return false;
            }
        }

        public bool checkLocked() {
            return checkLocked(origin);
        }
	}
}
