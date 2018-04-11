namespace ReversiDoneProperly.NModels
{
    public class Coordinate
    {
        public Coordinate(int x, int y, int steps = 0)
        {
            X = x;
            Y = y;
            Steps = steps;
        }

        public int X { get; set; }
        public int Y { get; set; }

        // Unfortunately the only time this var will be used is in "End" coordinate
        // In Start one it will always be 0, but sacrifaces have to be made
        public int Steps { get; set; }

        // Did this, instead of overlaping a equality operator because it made some strange error I had no time debugging 
        public bool IsSame(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
