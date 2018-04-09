using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise2_Reversi.Models
{
    public class Board
    {
        public BoardPiece[,] ReversiBoard { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            ReversiBoard = new BoardPiece[height, width];
        }

        public void AddBoardPiece(BoardPiece piece, int x, int y)
        {
            ReversiBoard[x, y] = piece;
        }

        public void ShowBoard()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(ReversiBoard[i, j] + "  ");
                }
                Console.WriteLine("\n");
            }
        }

        public string GetOptimalPathToTake()
        {
            // First check for number of available spaces left on board
            int numberOfEmptyBoardPieces = GetNumberOfEmptyBoardPieces();

            // If there are none left return 
            if (numberOfEmptyBoardPieces <= 0)
                return "No space";

            // If there is only one left, return that one
            if (numberOfEmptyBoardPieces == 1)
                return GetThatEmptyTile();

            // Valid range for coordinates
            IEnumerable<int> iRange = Enumerable.Range(0, Height - 1);
            IEnumerable<int> jRange = Enumerable.Range(0, Width - 1);

            List<int> numberOfSpacesList = new List<int>();
            List<BoardPiece> availableMoves = new List<BoardPiece>();
            // Loop trought all of the elements untill you find enemy one
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (ReversiBoard[i, j].BoardPieceStatus == BoardPieceStatus.OpponentOwned)
                    {
                        // When enemy one has been found, first check if surrounding tiles are empty or exist at all

                        /* =======================
                         * TOP TO BOTTOM
                         * ======================= */

                        // Left - horizontal search
                        int leftI = i;
                        int leftJ = j - 1;
                        if (iRange.Contains(leftI) && jRange.Contains(leftJ))
                        {
                            // Only if it's empty tile, do something
                            // Go from that point in all directions looking for our figure
                            if (ReversiBoard[leftI, leftJ].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                            {
                                int numberOfSpaces = SearchHorizontallyLeftToRight(i, j);
                                if (numberOfSpaces > 0)
                                {
                                    numberOfSpacesList.Add(numberOfSpaces);
                                    availableMoves.Add(ReversiBoard[leftI, leftJ]);
                                }
                            }
                        }

                        // Upper Left - diagonal search
                        int upperLeftI = i - 1;
                        int upperLeftJ = j - 1;
                        if (iRange.Contains(upperLeftI) && jRange.Contains(upperLeftJ))
                        {
                            if (ReversiBoard[upperLeftI, upperLeftJ].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                            {
                                int numberOfSpaces = SearchDiagonallyLeftRightTopBottom(i, j);
                                if (numberOfSpaces > 0)
                                {
                                    numberOfSpacesList.Add(numberOfSpaces);
                                    availableMoves.Add(ReversiBoard[upperLeftI, upperLeftJ]);
                                }
                            }
                        }

                        // Top one - vertical search
                        int topI = i - 1;
                        int topJ = j;
                        // Console.WriteLine(ReversiBoard[topI, topJ].GetPrettyPosition());
                        if (iRange.Contains(topI) && jRange.Contains(topJ))
                        {
                            // Only if it's empty tile, do something
                            // Go from that point in all directions looking for our figure
                            if (ReversiBoard[topI, topJ].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                            {
                                int numberOfSpaces = SearchVerticallyTopBottom(i, j);
                                if (numberOfSpaces > 0)
                                {
                                    numberOfSpacesList.Add(numberOfSpaces);
                                    availableMoves.Add(ReversiBoard[topI, topJ]);
                                }
                            }
                        }

                        /* =======================
                         * BOTTOM TO TOP
                         * ======================= */

                        // Bottom - vertical search up
                        int bottomI = i + 1;
                        int bottomJ = j;
                        if (iRange.Contains(bottomI) && jRange.Contains(bottomJ))
                        {
                            if (ReversiBoard[bottomI, bottomJ].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                            {
                                int numberOfSpaces = SearchVerticallBottomTop(i, j);
                                if (numberOfSpaces > 0)
                                {
                                    numberOfSpacesList.Add(numberOfSpaces);
                                    availableMoves.Add(ReversiBoard[bottomI, bottomJ]);
                                }
                            }
                        }

                        // Right - Horizontal search from left to right
                        int rightI = i;
                        int rightJ = j + 1;
                        if (iRange.Contains(rightI) && jRange.Contains(rightJ))
                        {
                            if (ReversiBoard[rightI, rightJ].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                            {
                                int numberOfSpaces = SearchHorizontallyRightToLeft(i, j);
                                if (numberOfSpaces > 0)
                                {
                                    numberOfSpacesList.Add(numberOfSpaces);
                                    availableMoves.Add(ReversiBoard[rightI, rightJ]);
                                }
                            }
                        }

                        // Bottom Left - horizontal search left up
                        int bottomRightI = i + 1;
                        int bottomRightJ = j + 1;
                        if (iRange.Contains(bottomRightI) && jRange.Contains(bottomRightJ))
                        {
                            if (ReversiBoard[bottomRightI, bottomRightJ].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                            {
                                int numberOfSpaces = SearchDiagonallyUp(i, j);
                                if (numberOfSpaces > 0)
                                {
                                    numberOfSpacesList.Add(numberOfSpaces);
                                    availableMoves.Add(ReversiBoard[bottomRightI, bottomRightJ]);
                                }
                            }
                        }


                    }
                }
            }

            // If all moves game same length, return them all
            bool allAvailableMovesSameLength = numberOfSpacesList.All(o => o == numberOfSpacesList[0]);
            if (allAvailableMovesSameLength)
            {
                List<string> vals = availableMoves.Select(s => s.GetPrettyPosition()).ToList();
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


            int indexOfHighestSpaces = 0;
            int numberOfHighestSpaces = 0;
            for (int i = 0; i < numberOfSpacesList.Count; i++)
            {
                if (numberOfHighestSpaces < numberOfSpacesList[i])
                {
                    numberOfHighestSpaces = numberOfSpacesList[i];
                    indexOfHighestSpaces = i;
                }
            }


            if (numberOfSpacesList.Count <= 0)
                return "none";
            return availableMoves[indexOfHighestSpaces].GetPrettyPosition();
        }

        // This method will ONLY be called if there is ONLY ONE free field
        private string GetThatEmptyTile()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (ReversiBoard[i, j].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                    {
                        return ReversiBoard[i, j].GetPrettyPosition();
                    }
                }
            }
            return null;
        }


        /* Top bottom */
        private int SearchHorizontallyLeftToRight(int leftI, int leftJ)
        {
            int counter = 0;
            bool activeUserTileFound = false;
            for (int j = leftJ; j < Width; j++)
            {
                if (ReversiBoard[leftI, j].BoardPieceStatus == BoardPieceStatus.OpponentOwned)
                {
                    counter++;
                }
                if (ReversiBoard[leftI, j].BoardPieceStatus == BoardPieceStatus.ActivePlayerOwned)
                {
                    activeUserTileFound = true;
                    break;
                }
            }

            if (activeUserTileFound)
                return counter;
            return 0;
        }
        private int SearchVerticallyTopBottom(int leftI, int leftJ)
        {
            int counter = 0;
            bool activeUserTileFound = false;
            for (int i = leftI; i < Height; i++)
            {
                if (ReversiBoard[i, leftJ].BoardPieceStatus == BoardPieceStatus.OpponentOwned)
                {
                    counter++;
                }
                if (ReversiBoard[i, leftJ].BoardPieceStatus == BoardPieceStatus.ActivePlayerOwned)
                {
                    activeUserTileFound = true;
                    break;
                }
            }

            if (activeUserTileFound)
                return counter;
            return 0;
        }
        private int SearchDiagonallyLeftRightTopBottom(int leftI, int leftJ)
        {
            int counter = 0;
            bool activeUserTileFound = false;

            for (int i = leftI; i < Height; i++)
            {
                for (int j = leftJ; j < Width; j++)
                {
                    if (i == j)
                    {
                        if (ReversiBoard[i, j].BoardPieceStatus == BoardPieceStatus.OpponentOwned)
                        {
                            counter++;
                        }
                        if (ReversiBoard[i, j].BoardPieceStatus == BoardPieceStatus.ActivePlayerOwned)
                        {
                            activeUserTileFound = true;
                            break;
                        }
                    }
                }
                if (activeUserTileFound)
                    break;
            }

            if (activeUserTileFound)
                return counter + 1; //add one more for that starting position
            return 0;
        }

        /* Bottom top */
        private int SearchHorizontallyRightToLeft(int leftI, int leftJ)
        {
            int counter = 0;
            bool activeUserTileFound = false;

            for (int j = leftJ; j > 0; j--)
            {
                if (ReversiBoard[leftI, j].BoardPieceStatus == BoardPieceStatus.OpponentOwned)
                {
                    counter++;
                }
                if (ReversiBoard[leftI, j].BoardPieceStatus == BoardPieceStatus.ActivePlayerOwned)
                {
                    activeUserTileFound = true;
                    break;
                }
            }

            if (activeUserTileFound)
                return counter;
            return 0;
        }
        private int SearchVerticallBottomTop(int bottomI, int bottomJ)
        {
            int counter = 0;
            bool activeUserTileFound = false;
            for (int i = bottomI; i > 0; i--)
            {
                if (ReversiBoard[i, bottomJ].BoardPieceStatus == BoardPieceStatus.OpponentOwned)
                {
                    counter++;
                }
                if (ReversiBoard[i, bottomJ].BoardPieceStatus == BoardPieceStatus.ActivePlayerOwned)
                {
                    activeUserTileFound = true;
                    break;
                }
            }

            if (activeUserTileFound)
                return counter;
            return 0;
        }
        private int SearchDiagonallyUp(int leftI, int leftJ)
        {
            int counter = 0;
            bool activeUserTileFound = false;

            for (int i = leftI; i > 0; i--)
            {
                for (int j = leftJ; j > 0; j--)
                {
                    if (i == j)
                    {
                        if (ReversiBoard[i, j].BoardPieceStatus == BoardPieceStatus.OpponentOwned)
                        {
                            counter++;
                        }
                        if (ReversiBoard[i, j].BoardPieceStatus == BoardPieceStatus.ActivePlayerOwned)
                        {
                            activeUserTileFound = true;
                            break;
                        }
                    }
                }
                if (activeUserTileFound)
                    break;
            }
            if (activeUserTileFound)
                return counter;
            return 0;
        }

        public BoardPiece[] ValidMoves(BoardPieceStatus color)
        {
            List<BoardPiece> Moves = new List<BoardPiece>();
            for (int col = 0; col < Height; ++col)
            {
                for (int row = 0; row < Width; ++row)
                {
                    // Making no move at all is always invalid
                    if (color == BoardPieceStatus.EmptyTile) continue;

                    // Check if `col` and `row` are in the boundaries of the board and if (`col`, `row`) is an empty square
                    if (ReversiBoard[col, row].BoardPieceStatus != BoardPieceStatus.EmptyTile) continue;

                    // Flip over the pieces of the other color that become enclosed between two pieces of `color`
                    bool piecesFlipped = false;                 // Whether or not some pieces are flipped over
                    for (int dx = -1; dx <= 1; ++dx)
                    {
                        for (int dy = -1; dy <= 1; ++dy)
                        {
                            if (dx == 0 && dy == 0) continue;

                            // Determine the amount of steps that we should go in the current direction until we encounter a piece of our own color
                            // Then, if we find a piece of our own color, flip over all pieces in between
                            // If we do encounter such a piece, or if we encounter an empty square first, we won't flip over any pieces
                            for (int steps = 1; steps <= Math.Max(Width, Height); ++steps)
                            {
                                int currX = col + steps * dx;
                                int currY = row + steps * dy;

                                if (currX < 0 || currX >= Width || currY < 0 || currY >= Height || ReversiBoard[currY, currX].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                                    break;

                                if (ReversiBoard[currX, currY].BoardPieceStatus == color)
                                {
                                    piecesFlipped = piecesFlipped || steps > 1;
                                    break;
                                }
                            }
                        }
                    }

                    // If we've flipped over some pieces, the move was valid
                    // In that case we only need to place the new piece
                    // If we haven't flipped over any pieces, then nothing has changed
                    // In that case we simply return false
                    if (piecesFlipped)
                        Moves.Add(ReversiBoard[col, row]);
                    else
                        continue;
                }
            }

            return Moves.ToArray();
        }


        public int GetNumberOfEmptyBoardPieces()
        {
            int numberOfEmptyBoardPieces = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (ReversiBoard[i, j].BoardPieceStatus == BoardPieceStatus.EmptyTile)
                    {
                        numberOfEmptyBoardPieces++;
                    }
                }
            }

            return numberOfEmptyBoardPieces;
        }
    }
}