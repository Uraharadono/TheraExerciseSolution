using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReversiDoneProperly.NModels;

namespace ReversiDoneProperly.Util
{
    public static class GeneralHelpers
    {
        public static List<Range> JoinSearchResults(List<Range> results)
        {
            List<Range> resultsJoined = new List<Range>();
            for (int i = 0; i < results.Count; i++)
            {
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
                        temp.DistanceBetweenJoined += sameResult.DistanceBetween;
                    }
                    resultsJoined.Add(temp);
                }
            }

            return resultsJoined;
        }

        public static string GetMultiValueResult(List<Range> resultsJoined)
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
    }
}
