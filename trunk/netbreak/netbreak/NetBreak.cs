using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Netbreak
{
    class NetBreak
    {
    	public static void newGame(Grid game)
    	{
            bool GameLoop = true;

            while (GameLoop)
            {
                game.displayGrid();
                Console.Write("Enter Move ( as 'x y' ): ");
                string move = Console.ReadLine();
                string[] coord = move.Split(' ');
                Int32 val;
                if((coord.Length==2) && (Int32.TryParse(coord[0], out val) && Int32.TryParse(coord[1], out val)) && game.checkMove(Int32.Parse(coord[0]), Int32.Parse(coord[1]))) {
                
                    game.removeGroup(Int32.Parse(coord[0]), Int32.Parse(coord[1]));
                    game.Logger.addLog("MOVE: (" + coord[0] + "," + coord[1] + ")");
                    game.Logger.addLog("      --Points: " + game.calculatePoints());
                    
                } else
                {
                    Console.WriteLine("Invalid Move! Try again!");
                }
                
                game.compressGrid();
                
                if (game.checkWin())
                {
                    GameLoop = false;
                    Console.WriteLine("You Win!");
                    game.Logger.addLog("GAME WIN: All bubbles eliminated");
                    game.Logger.addLog("FINAL POINTS: " + game.Points);
                    game.Logger.close();
                    Console.ReadLine();
                } else if (game.checkLocked())
                {
                    GameLoop = false;
                    //display final game board showing locked game
                    game.displayGrid();
                    Console.WriteLine("You Lose!");
                    game.Logger.addLog("GAME LOSS: Gameboard locked!");
                    game.Logger.addLog("FINAL POINTS: " + game.Points);
                    game.Logger.close();
                    Console.ReadLine();
                }
            }
    	}

        public static void newAIGame(Grid game)
        {
            bool GameLoop = true;
            bool list  = false;
            AI blue = new AI(50);
            Stack<MoveNode> moves = new Stack<MoveNode>();
            
            if((game.Colors <= 5 && game.X <7) || (game.Colors < 4 && game.X <= 15))
            {
           		MoveNode move = blue.solveTree(game);
           		if(move != null)
           		{
           			list = true;
           			
           			while(move != null)
           			{
           				moves.Push(move);
           				move = move.PreviousMove;
           			}
           			
           		}
            }
            
            while (GameLoop)
            {
                int[] nextMove;
                game.displayGrid();
                
                

				if(list)
				{
					nextMove = new int[2];
					MoveNode cur = moves.Pop();
					nextMove[0] = cur.Group.X;
					nextMove[1] = cur.Group.Y;
					
				}
				else
				{
                	nextMove = blue.makeNextMove(game);
                }
                
                Console.WriteLine("AI makes the move (" + nextMove[0] + ", " + nextMove[1] + ")");

                game.removeGroup(nextMove[0], nextMove[1]);
                game.Logger.addLog("MOVE: (" + nextMove[0] + "," + nextMove[1] + ")");
                game.Logger.addLog("      --Points: " + game.calculatePoints());

                game.compressGrid();

                if (game.checkWin())
                {
                    GameLoop = false;
                    Console.WriteLine("You Win!");
                    game.Logger.addLog("GAME WIN: All bubbles eliminated");
                    game.Logger.addLog("FINAL POINTS: " + game.Points);
                    game.Logger.close();
                    Console.ReadLine();
                }
                else if (game.checkLocked())
                {
                    GameLoop = false;
                    //display final game board showing locked game
                    game.displayGrid();
                    Console.WriteLine("You Lose!");
                    game.Logger.addLog("GAME LOSS: Gameboard locked!");
                    game.Logger.addLog("FINAL POINTS: " + game.Points);
                    game.Logger.close();
                    Console.ReadLine();
                }
            }
        }

        static void Main()
        {
            Console.WriteLine(@"
ChainShot    
---------
Eliminate all bubbles from the grid by selecting and deleting
groups of the same bubble type (2 or more).  
   	- Moves are specified in an; 'x y' syntax.
   	- Points are awarded based on the number of bubbles in an
   	  eliminated group.");

            bool menuLoop = true;

            while (menuLoop)
            {

                Console.WriteLine(@"
Game Menu:
1) Human game.
2) AI game.
Choice:");
                int inp = Int32.Parse(Console.ReadLine());
                bool ai;
                if (inp == 1)
                    ai = false;
                else
                    ai = true;

                Console.WriteLine(@"
Game Menu:
1) Specify a gameboard file to open.
2) Randomly Generate a gamemboard (4 bubble types)
Choice:");

                int input = Int32.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1: bool askfile = true;
                        string file = "";
                        while (askfile)
                        {
                            askfile = false;
                            Console.Write("Enter filename of gameboard:");
                            file = Console.ReadLine();
                            Console.WriteLine(Directory.GetCurrentDirectory()+ Path.DirectorySeparatorChar +file);
                            if (!File.Exists(Directory.GetCurrentDirectory()+Path.DirectorySeparatorChar +file))
                            {
                                Console.WriteLine("File does not exist, try again.");
                                askfile = true;
                            }
                        }
                        if(ai)
                            newAIGame(new Grid(file));
                        else
                            newGame(new Grid(file));
                        break;

                    case 2: int x;
                        Console.Write("Enter the dimension of the grid:");
                        x = Int32.Parse(Console.ReadLine());
                        if (ai)
                            newAIGame(new Grid(x));
                        else
                            newGame(new Grid(x));
                        break;

                    case 3: menuLoop = false;
                        break;
                    default: Console.WriteLine("Sorry I don't recognize that option, try again.");
                        break;
                }

                Console.Write("Play again? (y/n):");
                string pa = Console.ReadLine();
                if (pa != "y")
                {
                    menuLoop = false;
                }
            }
        }
    }
}
