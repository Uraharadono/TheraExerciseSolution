namespace Exercise2_Reversi.Util
{
    public static class Converters
    {
        public static string Number2String(int number, bool isCaps)
        {
            // Use below one if your array doesnt start with 0. But mate you are wrong.
            // char c = (char)((isCaps ? 65 : 97) + (number - 1));
            char c = (char)((isCaps ? 65 : 97) + number);

            return c.ToString();
        }
    }
}
