using System;
using System.Linq;
using Exercise2_Reversi.Models;

namespace Exercise2_Reversi.Util
{
    public static class Parsers
    {
        public static string[] GetDimensions(string board)
        {
            String[] boardSplit = board.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            String[] firstBoardLineSplit = boardSplit[0].Split(new string[] { " " }, StringSplitOptions.None);
            return firstBoardLineSplit;
        }
        public static void ParseBoard(string board, Board myBoard)
        {
            String[] boardSplit = board.Split(new string[] { "\r\n" }, StringSplitOptions.None).Skip(1).ToArray();
            for (int i = 0; i < boardSplit.Length; i++)
            {
                String[] lineSplit = boardSplit[i].Trim().Split(new string[] { " " }, StringSplitOptions.None);
                for (int j = 0; j < lineSplit.Length; j++)
                {
                    BoardPiece nBoardPiece = new BoardPiece();
                    nBoardPiece.OriginalValue = lineSplit[j];
                    nBoardPiece.BoardPieceStatus = BoardHelpers.ResolveBoardPieceStatus(lineSplit[j]);
                    nBoardPiece.XPosition = i;
                    nBoardPiece.YPosition = j;
                    myBoard.AddBoardPiece(nBoardPiece, i, j);
                }
            }
        }
    }
}
