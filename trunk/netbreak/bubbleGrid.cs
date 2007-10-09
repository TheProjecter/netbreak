using System;
using System.Collections;

namespace bubblebreak {

	public class bubbleGrid {
		
		private bubbleNode origin;
		private int oheight;
		private int[] height;
		private int width;
		
		public bubbleGrid(int x, int y) {
			//won't use height[0] in order for this to work properly later on
            height = new int[x+1];
			width = x;
			oheight = y;
			for(int b=1; b<=x; b++) {
				height[b]=y;
			}
			
			Queue nodeQ = new Queue();
			
			for(int i=0; i<y; i++) {
				bubbleNode lastNode = new bubbleNode();
				bubbleNode cFirst = lastNode;
				for(int j=0; j<x; j++) {
					lastNode.Right = new bubbleNode();
					lastNode.Right.Left = lastNode;
					lastNode = lastNode.Right;
					Console.WriteLine(lastNode.Color);
				}
				nodeQ.Enqueue(cFirst);
			}
			 
			 
			//The only fix I could come up with was checking the height of both elements, and then setting the larger height to
			//have the index node in the top left, and the smaller the bottom left.  If the height =1, this doesn't matter,
			//and if they are the same height we choose arbitarily... probably just using the first one as top left...very techincal choice there :)
			while(nodeQ.Count>1) {
				bubbleNode next = (bubbleNode) nodeQ.Dequeue();
                bubbleNode last = (bubbleNode) nodeQ.Dequeue();
               	int nh = getHeight(next);
               	int lh = getHeight(last);
               	bubbleNode first = null;
               	if(lh==1 || nh==1) {
               		//don't do anything if last has a height of 1
               		first = next;

               	} else if(nh >= lh) {
                    first = last;
                    while(last.Down != null) {
               			last = last.Down;
               		}
               		bubbleNode temp = next;
               		next = last;
               		last = temp;
               	} else if(lh>nh) {
                    first = next;
               		while(next.Down != null) {
               			next = next.Down;
               		}       
               	}
				for(int k=0; k<x; k++) {
					last.Up = next;
					next.Down = last;
					last = last.Right;
					next = next.Right;
				}
				nodeQ.Enqueue(first);
			}
			
            origin = (bubbleNode) nodeQ.Dequeue();
            
            while (origin.Down != null) {
                origin = origin.Down;
            }
		}
		
		public int getHeight(bubbleNode n) {
			int height = 1;
			while(n.Down!= null) {
				height++;
				n = n.Down;
			}
			return height;
		}
		
		public bubbleNode Get(int x, int y) {
            checkBounds(x,y);
            bubbleNode lastNode = origin;
			
			//go to the right x nodes
			for(int i=1; i<x && i>1; i++) {
				lastNode = lastNode.Right;
			}

			//go down y nodes
			for (int j=1; j<y && j>1; j++) {
				lastNode = lastNode.Up;
			}
			return lastNode;
		}
	
		//method removes node and returns a reference to the removed node
		public bubbleNode remove(int x, int y) {

			bubbleNode removedNode = Get(x, y);

			bubbleNode u = removedNode.Up;
			bubbleNode d = removedNode.Down;
			bubbleNode r = removedNode.Right;
			bubbleNode l = removedNode.Left;
			u.Down = d;
			d.Up = u;
			r.Left = l;
			l.Right = r;
			
			//adjust the height property of this column.  if it reaches 0, then we elminated
			//one of the columns and width should be adjusted
			if(--height[x] == 0) {
				width--;
			}
			return removedNode;
		}

        public void remove(bubbleNode removedNode) {
            int y = 0;
            int x = 0;
            bubbleNode sNode = removedNode;

            while (sNode.Down != null) {
                y++;
                sNode = sNode.Down;
            }
            while (sNode.Left != null) {
                x++;
                sNode = sNode.Left;
            }
            remove(x, y);
        }

        public void displayGrid() {
        	for(int i=1; i<=width; i++){
        		for(int j=oheight; j>0; j--) {
        			if(Get(i,j) != null) {
        				Console.Write(Get(i,j).Color);
        			} else {
        				Console.Write(" ");
        			}
        		}
        		Console.WriteLine();
        	}
        }

        public void removeConnected(bubbleNode startNode) {
            //if deleted is not true, then there is not more than one of this color and nothing should be removed
            bool deleted = false;
            //check left
            if(startNode.Left != null && startNode.Left.Color == startNode.Color) {
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

		//TODO: fix the displayGrid method.  I've fixed origin so it points to the bottom left node
        //      now just cycle through each node and display its Color.
	}
}
