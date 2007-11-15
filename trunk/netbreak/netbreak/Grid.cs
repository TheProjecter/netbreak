using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//TODO: add checks on data entry so that empty or incomplete entries do not crash the game
//The grid is represented in a matrix with x and y +2.  The matrix sits within a border of "E" cells.  This allows checkMove to work,
//without having to write a long extensive check for the left, right, up, and down cells (to check its not out of bounds)
// (so basically it probably saves 5-10 lines of code in a few methods, which is worth it to me :) )
//Convieniently, by doing this, a user defined coordinate like (1,1) for the bottom left corner, is also the matrix index

namespace Netbreak
{
    class Grid
    {
        private string[,] grid;
        private int x;
        private MoveLogger logger;
        private int currentRemoved;
        private int points;

        public Grid(int x)
        {
            this.x = x;
         	logger = new MoveLogger();
	        
	        grid = new string[x+2, x+2];
	      
	     	Random rangen = new Random();
	      	string color = "";

            for (int i = 0; i < x + 2; i++)
            {
                for (int j = 0; j < x + 2; j++)
                {
                    if (i == 0 || j == 0 || (i == x + 1) || (j == x + 1))
                    {
                        grid[i, j] = "E";
                    }
                    else
                    {
                        switch (rangen.Next(4))
                        {
                            case 0: color = "R";
                                break;
                            case 1: color = "G";
                                break;
                            case 2: color = "Y";
                                break;
                            case 3: color = "B";
                                break;
                        }
                        grid[i, j] = color;
                    }
                }
            }
  		}
            
        public Grid(string filename) 
        {
        
        	logger = new MoveLogger();
        	
        	StreamReader read = new StreamReader(filename);
        	string file = read.ReadToEnd();
        	string[] lines = file.Split('\n');
        	this.x = Int32.Parse(lines[0]);
        	grid = new string[x+2, x+2];
        	
        	for(int i=0; i<=x+1; i++) 
        	{
        		for(int j=0; j<=x+1; j++) 
        		{
        			if (i == 0 || j == 0 || (i == x + 1) || (j == x + 1))
	               	{
	            		grid[i, j] = "E";
	               	} else 
	               	{
	               		grid[j,(x+1)-i] = lines[i].Substring(j-1,1);
	               	}
        		}
        	}
        }

        public bool checkWin()
        {
            return (grid[1,1] == "E");
        }

        public bool checkLocked()
        {
            for (int i = 1; i <= x; i++)
            {
                for (int j = 1; j <= x; j++)
                {
                    if (checkMove(i, j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool checkBounds(int x, int y)
        {
            return !((x < 0 || x > this.x) || (y < 0 || y >this.x) || (grid[x, y] == "E"));
        }

        public bool checkMove(int x, int y)
        {
            return checkBounds(x,y) && ((grid[x, y] == grid[x - 1, y]) || (grid[x, y] == grid[x + 1, y]) || (grid[x, y] == grid[x, y - 1]) || (grid[x, y] == grid[x, y + 1]));
        }

        public void removeGroup(int x, int y)
        {
            if (checkBounds(x, y))
            {
                string color = grid[x, y];
                grid[x, y] = "E";
                currentRemoved++;

                if (grid[x + 1, y] == color)
                {
                    removeGroup(x + 1, y);
                }

                if (grid[x - 1, y] == color)
                {
                    removeGroup(x - 1, y);
                }

                if (grid[x, y - 1] == color)
                {
                    removeGroup(x, y - 1);
                }

                if (grid[x, y + 1] == color)
                {
                    removeGroup(x, y + 1);
                }
            }
        }

        public void compressGrid()
        {
            Queue<string> bubbleQ = new Queue<string>();

            //first pass moves all bubbles down
            for (int i = 1; i <= x; i++)
            {
                for (int j = 1; j <= x; j++)
                {
                    if (grid[i, j] != "E")
                    {
                        bubbleQ.Enqueue(grid[i, j]);
                    }
                }
                for (int j = 1; j <= x; j++)
                {
                    if (bubbleQ.Count > 0)
                    {
                        grid[i, j] = bubbleQ.Dequeue();
                    }
                    else
                    {
                        grid[i, j] = "E";
                    }
                }
            }

            //second pass moves columns over 

            for (int i = 1; i <= x; i++)
            {
                if (grid[i, 1] == "E")
                {
                    int replace = i;
                    for (int k = replace; k <= x; k++)
                    {
                        if (grid[k, 1] != "E")
                        {
                            for (int j = 1; j <= x; j++)
                            {
                                grid[replace, j] = grid[k, j];
                                grid[k, j] = "E";
                            }
                       	k = x+1;
                        }
                    }
                }
            }
        }

        public void displayGrid()
        {
            for (int i = x; i > 0; i--)
            {
                for (int j = 1; j<=x; j++)
                {
                    if (grid[j, i] == "E")
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(grid[j, i]);
                    }
                }
                Console.WriteLine();
            }
        }

        public double calculatePoints()
        {
            double result = Math.Pow((currentRemoved - 1), 2);
            points += (int) result;
            currentRemoved = 0;
            return result;
        }
        

        public MoveLogger Logger
        {
            get { return logger; }
        }

        public int Points
        {
            get { return points; }
        }

    }//end class
}//end namespace