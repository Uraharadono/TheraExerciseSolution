using System;
using System.Collections.Generic;
using System.Linq;
using ReversiDoneProperly.NModels;

namespace ReversiDoneProperly.Util
{
    public class Solution
    {
        public static string PlaceToken(string board)
        {
            string[] dimensions = Parsers.GetDimensions(board);
            int width = Convert.ToInt32(dimensions[0]);
            int height = Convert.ToInt32(dimensions[1]);

            /*
             * I didn't check for size of board, because I didn't see in examples that there were any with wrong size
             * if needed would be simple. 
             */

            // Prepare list of terms to search for
            List<string> listOfWordsToSearchFor = Generators.GetSearchTermsList(width);

            // Create our reversi board
            char[,] myBoard = new char[height, width];
            Parsers.ParseBoard(board, myBoard); //send board by reference, why not :D

            // Init our search class
            SearchEngine searchEngine = new SearchEngine(myBoard, width, height);

            // Get results for all of our search terms
            List<Range> results = searchEngine.Search(listOfWordsToSearchFor);

            // After we got all of the results, then we join coordinates that have same Start
            List<Range> resultsJoined = GeneralHelpers.JoinSearchResults(results);
            
            // If all moves hame same length, return them all
            bool allAvailableMovesSameLength = resultsJoined.All(o => o.DistanceBetweenJoined == resultsJoined[0].DistanceBetweenJoined);
            if (allAvailableMovesSameLength)
                return GeneralHelpers.GetMultiValueResult(resultsJoined);

            // If not, we then order our list by biggest distance first, and we return first element of array
            // Note: I am assuming I will always have results here that's why I am using "First()", this should throw errors if nothing is found
            Range retRange = resultsJoined.OrderByDescending(s => s.DistanceBetweenJoined).ToList().First();

            return retRange.GetStartInfo();
        }
    }

}
