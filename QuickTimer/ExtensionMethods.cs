namespace QuickTimer
{
    public static class ExtensionMethods
    {
        public static string ToSeconds(this long milliseconds)
        {
            return $"{milliseconds / 1000d} seconds.";
        }
    }
}
