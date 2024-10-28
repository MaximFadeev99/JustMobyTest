namespace JustMobyTest.Utilities
{
    public static class Extentions
    {
        public static string ToReadableInt(this int number)
        {
            return number.ToString("# ### ### ##0").Trim();
        }
    }
}
