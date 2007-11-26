using System;
using System.Collections.Generic;
using System.Text;

namespace Netbreak
{
	struct Group : IComparable
	{
        private int x;
        private int y;
        private int bubbles;

        public Group(int x, int y, int count)
        {
            this.x = x;
            this.y = y;
            bubbles = count;
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Bubbles
        {
            get { return bubbles;}
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            int r = (int)obj;
            double result = bubbles - r;
            if (result > 0)
                return 1;
            if (result < 0)
                return -1;
            return 0;
        }

        #endregion
    }
}
