namespace QuickTimer
{
    public static class ExtensionMethods
    {
        public static string ToSeconds(this long milliseconds)
        {
            return $"{milliseconds / 1000L} seconds.";
        } 

        //4. Lambda body extenstion methods
        //public static string ToSeconds(this long milliseconds) => $"{milliseconds / 1000L} seconds.";

    }
}
