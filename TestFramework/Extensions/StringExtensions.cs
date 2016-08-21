using System;
using System.Linq;

namespace TestFramework.Extensions
{
    public static class StringExtensions
    {
        public static bool IsElementClassName(this string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException(nameof(className), "Element class name is null or empty. Class names should be strings that begin with \".\"");
            }
            return className.ToCharArray().First().ToString() == ".".ToLowerInvariant();
        }

        public static bool IsElementId(this string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Element id is null or empty. Element ids should be strings that begin with \"#\"");
            }
            return id.ToCharArray().First().ToString() == "#".ToLowerInvariant();
        }
    }
}
