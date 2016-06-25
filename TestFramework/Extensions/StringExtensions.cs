using System.Linq;

namespace TestFramework.Extensions
{
    public static class StringExtensions
    {
        public static bool IsElementClassName(this string className)
        {
            return className.ToCharArray().First().ToString() == ".".ToLowerInvariant();
        }

        public static bool IsElementId(this string id)
        {
            return id.ToCharArray().First().ToString() == "#".ToLowerInvariant();
        }
    }
}
