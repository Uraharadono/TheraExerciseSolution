using Exercise2_Reversi.Models;

namespace Exercise2_Reversi.Util
{
    public static class BoardHelpers
    {
        public static BoardPieceStatus ResolveBoardPieceStatus(string s)
        {
            switch (s)
            {
                case ".":
                    return BoardPieceStatus.EmptyTile;
                case "O":
                    return BoardPieceStatus.OpponentOwned;
                case "X":
                    return BoardPieceStatus.ActivePlayerOwned;
            }
            // This is bad, should be exception thrown but no time to think about that
            return BoardPieceStatus.EmptyTile;
        }
    }
}
