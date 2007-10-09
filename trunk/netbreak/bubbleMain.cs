using System;
using System.Collections.Generic;
using System.Text;

namespace bubblebreak
{
    class bubbleMain {

        public static void Main(string[] args) {
           bubbleGrid game = new bubbleGrid(5,5);
            bool play = true;
            while(play) {
                game.displayGrid();
                Console.Write("Input x: ");
                string input = Console.ReadLine(); 
                int x = Int32.Parse(input);
                Console.Write("Input y: ");
                string input2 = Console.ReadLine(); 
                int y = Int32.Parse(input2);
                game.removeConnected(game.get(x,y));
                if(game.checkWin()) {
                    Console.WriteLine("Congratulations! You won!");
                    play = false;
                } else if(game.checkLocked()) {
                    Console.WriteLine("No more move available.  You lose!");
                    play = false;
                }
            }
        }
    }
}
