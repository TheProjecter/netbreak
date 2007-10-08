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
                int x = Console.Read(); 
               // int x = Convert.ToInt32(input);
                Console.Write("Input y: ");
                int y = Console.Read(); 
               // int y = Convert.ToInt32(input2);
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
