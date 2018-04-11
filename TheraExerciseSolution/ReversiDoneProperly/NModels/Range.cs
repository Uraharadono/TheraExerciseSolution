using ReversiDoneProperly.Util;

namespace ReversiDoneProperly.NModels
{
    public class Range
    {
        public Range(Coordinate start, Coordinate end)
        {
            Start = start;
            End = end;
        }

        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }

        public bool HasBeenJoined { get; set; }
        public int DistanceBetween
        {
            get
            {
                return End.Steps;
                #region Overthinking

                //pythagorean theorem c^2 = a^2 + b^2
                //thus c = square root(a^2 + b^2)
                //int a = (End.X - Start.X);
                //int b = (End.Y - Start.Y);
                //return (int) Math.Sqrt(a * a + b * b);

                #endregion
            }
        }
        public int DistanceBetweenJoined { get; set; }

        public string GetStartInfo()
        {
            return Converters.Number2String(Start.Y, true) + (Start.X + 1);
        }

        public string GetEndInfo()
        {
            return Converters.Number2String(End.Y, true) + (End.X + 1);
        }
    }
}
