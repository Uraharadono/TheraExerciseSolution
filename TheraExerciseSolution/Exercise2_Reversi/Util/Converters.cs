namespace Exercise2_Reversi.Util
{
    public static class Converters
    {
        public static string Number2String(int number, bool isCaps)
        {
            // Use below one if your array starts with 1. You n00b.
            // char c = (char)((isCaps ? 65 : 97) + (number - 1));
            char c = (char)((isCaps ? 65 : 97) + number);

            return c.ToString();
        }
    }
}
