namespace Exercise2_Reversi.Util
{
    public static class Converters
    {
        public static string Number2String(int number, bool isCaps)
        {
            char c = (char)((isCaps ? 65 : 97) + (number - 1));

            return c.ToString();

        }
    }
}
