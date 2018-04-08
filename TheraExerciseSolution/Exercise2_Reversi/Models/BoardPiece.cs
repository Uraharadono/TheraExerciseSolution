using System.ComponentModel;
using Exercise2_Reversi.Util;

namespace Exercise2_Reversi.Models
{
    public class BoardPiece
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public string OriginalValue { get; set; }
        public BoardPieceStatus BoardPieceStatus { get; set; }

        public override string ToString()
        {
            return OriginalValue;
        }

        public string GetPrettyPosition()
        {
            return Converters.Number2String(YPosition, true) + (XPosition + 1);
        }
    }

    public enum BoardPieceStatus
    {
        [Description("Empty Tile")]
        EmptyTile = 0,
        [Description("Opponent Owned")]
        OpponentOwned = 1,
        [Description("Empty Tile")]
        ActivePlayerOwned = 2
    }
}
