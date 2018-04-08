using System;
using Exercise2_Reversi.Util;

namespace Exercise2_Reversi
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                // 1. Correct Answer: "E1"
                string board1 = @"5 1
X O O O . ";

                string result1 = Solution.PlaceToken(board1);
                Console.WriteLine("board 1: " + result1);

                // 2. Correct Answer: "B2" 
                string board2 = @"8 7
. . . . . . . . 
. . . . . . . . 
. . O . . . . . 
. . . O X . . . 
. . . X O O . . 
. . . . . X . . 
. . . . . . X . ";

                string result2 = Solution.PlaceToken(board2);
                Console.WriteLine("board 2: " + result2);

                // 3. Correct Answer: "D3", "C4", "F5", "E6" 
                string board3 = @"8 8
. . . . . . . . 
. . . . . . . . 
. . . . . . . . 
. . . O X . . . 
. . . X O . . . 
. . . . . . . . 
. . . . . . . . 
. . . . . . . . ";

                string result3 = Solution.PlaceToken(board3);
                Console.WriteLine("board 3: " + result3);

                // 4. Correct Answer: "D6 
                string board4 = @"7 6
. . . . . . . 
. . . O . O . 
X O O X O X X 
. O X X X O X 
. X O O O . X 
. . . . . . . ";

                string result4 = Solution.PlaceToken(board4);
                Console.WriteLine("board 4: " + result4);
            }

        }
    }
}
