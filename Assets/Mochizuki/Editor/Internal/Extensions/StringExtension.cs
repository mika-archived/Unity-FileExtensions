#if UNITY_2017

namespace Mochizuki.Editor.Internal.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrWhitespace(this string str)
        {
            return str == null || str.Trim().Length == 0;
        }
    }
}

#endif