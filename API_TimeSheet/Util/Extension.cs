namespace Extension
{
    public static class Extension
    {
        public static string FormatarData(this string data)
        {
            return data.Replace("-", "/");
        }
    }
}