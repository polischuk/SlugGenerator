using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnidecodeSharpFork;

namespace SlugGenerator
{
    public static class SlugGenetator
    {
        /// <summary>
        /// Method generate slug by text (multiply languages)
        /// </summary>
        /// <param name="incomingString">Text for slug</param>
        /// <returns>Slug for url</returns>
        public static string GenerateSlug(this string incomingString)
        {
            incomingString = incomingString.Unidecode();
            incomingString = Regex.Replace(incomingString, @"\s+", "-");
            var alphaNum = Regex.Replace(incomingString, @"[^A-Za-z0-9-]+", "");
            return alphaNum.ToLower();
        }
        /// <summary>
        /// Method generate unique slug by text and list of exist slugs  (multiply languages)
        /// </summary>
        /// <param name="incomingString">Text for slug</param>
        /// <param name="items">list of slugs</param>
        /// <returns>Slug for url</returns>
        public static string GenerateUniqueSlug<T>(this string incomingString, List<T> items) where T : ISlug
        {
            var slug = GenerateSlug(incomingString);
            if (items.Any(m => m.Slug == slug))
            {
                slug += slug.GetHashCode();
                if (items.Any(m => m.Slug == slug))
                {
                    slug += Guid.NewGuid();
                }
            }
            return slug;
        }
    }
}
