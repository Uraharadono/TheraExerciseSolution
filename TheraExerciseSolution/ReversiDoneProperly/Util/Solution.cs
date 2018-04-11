using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            List<string> listOfWordsToSearchFor = new List<string>();
            for (int i = 1; i <= width - 2; i++)
            {
                string word = ".";
                for (int j = 0; j < i; j++)
                {
                    word += "O";
                }
                word += "X";
                listOfWordsToSearchFor.Add(word);
            }

            // Create board
            char[,] myBoard = new char[height, width];
            Parsers.ParseBoard(board, myBoard);

            // Init our search class
            SearchEngine ws = new SearchEngine(myBoard, width, height);

            // Get results for all of our search terms
            List<Range> results = new List<Range>();
            foreach (var word in listOfWordsToSearchFor)
            {
                var result = ws.Search(word);
                if (result.Count > 0)
                {
                    results.AddRange(result);
                }
            }

            // After we got all of the results, then we join coordinates that have same Start
            List<Range> resultsJoined = new List<Range>();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Start.X == 5 && results[i].Start.Y == 3)
                {
                    var nest = "";
                }

                    if (!results[i].HasBeenJoined)
                {
                    Range temp = results[i];
                    temp.DistanceBetweenJoined = temp.DistanceBetween;

                    List<Range> sameResults = new List<Range>();
                    // For each current i, search for j
                    for (int j = i; j < results.Count; j++)
                    {
                        // if (temp.Start == results[j].Start)
                        if (temp.Start.IsSame(results[j].Start))
                        {
                            sameResults.Add(results[j]);
                            results[j].HasBeenJoined = true;
                        }
                    }

                    foreach (var sameResult in sameResults)
                    {
                       temp.DistanceBetweenJoined +=  sameResult.DistanceBetween;
                    }
                    resultsJoined.Add(temp);
                }
            }

            // If all moves game same length, return them all
            bool allAvailableMovesSameLength = resultsJoined.All(o => o.DistanceBetweenJoined == resultsJoined[0].DistanceBetweenJoined);
            if (allAvailableMovesSameLength)
            {
                List<string> vals = resultsJoined.Select(s => s.GetStartInfo()).ToList();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < vals.Count; i++)
                {
                    if (i == (vals.Count - 1))
                    {
                        sb.Append(vals[i]);
                    }
                    else
                    {
                        sb.Append(vals[i] + ", ");
                    }
                }
                return sb.ToString();
            }

            // Assuming I will always have result with "First()", this should throw errors if nothing is found
            Range retRange = resultsJoined.OrderByDescending(s => s.DistanceBetweenJoined).ToList().First();
            
            return retRange.GetStartInfo();
        }
    }

}
