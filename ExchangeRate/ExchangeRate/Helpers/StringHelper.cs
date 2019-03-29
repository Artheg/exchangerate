using System;

namespace ExchangeRate.Helpers
{
    public static class StringHelper
    {
        public static string[] SplitByNewLine(string source)
        {
            return source.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
