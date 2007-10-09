using System;
using System.Collections.Generic;

namespace bubblebreak {
	
	public class bubbleNode {
		
		private bubbleNode left;
		private bubbleNode right;
		private bubbleNode up;
		private bubbleNode down;
		private int color;
		
		public bubbleNode() {
			Random rand = new Random();
            left = null;
			right = null;
			up = null;
			down = null;
            color = rand.Next(4);
		}
	
		public bubbleNode(bubbleNode l, bubbleNode r, bubbleNode u, bubbleNode d, int c) {
			left = l;
			right = r;
			up = u;
			down = d;
			color = c;
		}

        public int Color {
            get { return color;}
            set { color = value;}
        }

        public bubbleNode Left {
            get {return left;}
            set { left = value; }
        }

        public bubbleNode Right {
            get {return right;}
            set { right = value; }
        }

        public bubbleNode Up {
            get {return up;}
            set { up = value; }
        }

        public bubbleNode Down {
            get {return down;}
            set { down = value; }
        }

	}
}