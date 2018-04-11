using System;
using System.Collections.Generic;

namespace ReversiDoneProperly.NModels
{
    public class SearchEngine
    {
        public SearchEngine(char[,] puzzle, int width, int height)
        {
            Board = puzzle;
            Width = width;
            Height = height;
        }

        public char[,] Board { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // represents the array offsets for each
        // character surrounding the current one
        private readonly Coordinate[] directions =
        {
            new Coordinate(0,-1), // West
            new Coordinate(-1,-1), // North West
            new Coordinate(-1, 0), // North
            new Coordinate(-1, 1), // North East
            new Coordinate(0, 1),  // East
            new Coordinate(1, 1),  // South East
            new Coordinate(1, 0),  // South
            new Coordinate(1, -1)  // South West
         };

        public List<Range> Search(List<string> wordsList)
        {
            List <Range>  results = new List<Range>();
            foreach (var word in wordsList)
            {
                var result = Search(word);
                if (result.Count > 0)
                {
                    results.AddRange(result);
                }
            }

            return results;
        }

        public List<Range> Search(string word)
        {
            // This list will contain results for all of directions in this matrix for given search term e.g. ".OX"
            List<Range> results = new List<Range>();

            // scan the puzzle line by line
            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    if (Board[x, y] == word[0])
                    {
                        // and when we find a character that matches 
                        // the start of the word, scan in each direction 
                        // around it looking for the rest of the word
                        Coordinate start = new Coordinate(x, y);

                        char[] chars = word.ToCharArray();
                        for (int direction = 0; direction < 8; direction++)
                        {
                            Coordinate reference = SearchDirection(chars, x, y, direction);
                            // If word has been found in this direction, add it then countinue searching in other directions as well
                            // We might have results in other directions as well
                            if (reference != null)
                            {
                                results.Add(new Range(start, reference));
                            }
                        }
                    }
                }
            }
            return results;
        }

        private Coordinate SearchDirection(char[] chars, int x, int y, int direction, int step = 0)
        {
            // have we ve moved passed the boundary of the puzzle
            if (x < 0 || y < 0 || x >= Height || y >= Width)
                return null;

            if (Board[x, y] != chars[0])
                return null;

            // when we reach the last character in the word
            // the values of x,y represent location in the
            // puzzle where the word stops
            if (chars.Length == 1)
                return new Coordinate(x, y, step);

            // test the next character in the current direction
            char[] copy = new char[chars.Length - 1];
            Array.Copy(chars, 1, copy, 0, chars.Length - 1);
            return SearchDirection(copy, x + directions[direction].X, y + directions[direction].Y, direction, ++step);
        }
    }
}
