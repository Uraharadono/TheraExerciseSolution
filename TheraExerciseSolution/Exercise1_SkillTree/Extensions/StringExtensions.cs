namespace Exercise1_SkillTree.Extensions
{
    public static class StringExtensions
    {
        public static string Indent(this string value, int size)
        {
            return "".PadLeft(size) + value;
        }
    }
}
