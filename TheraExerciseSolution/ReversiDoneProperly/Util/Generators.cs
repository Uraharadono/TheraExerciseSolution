using System.Collections.Generic;

namespace ReversiDoneProperly.Util
{
    public class Generators
    {
        public static List<string> GetSearchTermsList(int width)
        {
            List<string> retList = new List<string>();
            for (int i = 1; i <= width - 2; i++)
            {
                string word = ".";
                for (int j = 0; j < i; j++)
                {
                    word += "O";
                }
                word += "X";
                retList.Add(word);
            }

            return retList;
        }
    }
}
