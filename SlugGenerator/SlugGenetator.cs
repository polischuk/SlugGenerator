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
        /// Method generate slug by text (multiple languages)
        /// </summary>
        /// <param name="incomingString">Text for slug</param>
        /// <param name="slugSeparator">Slug separator, example "-" or "_"</param>
        /// <returns>Slug for url</returns>
        public static string GenerateSlug(this string incomingString, string slugSeparator = "-")
        {
            incomingString = incomingString.Unidecode();
            incomingString = Regex.Replace(incomingString, @"\s+", slugSeparator);
            var alphaNum = Regex.Replace(incomingString, @"[^A-Za-z0-9-]+", "");
            return alphaNum.ToLower();
        }

        /// <summary>
        /// Method generate unique slug by text and list of exist slugs  (multiple languages)
        /// </summary>
        /// <param name="incomingString">Text for slug</param>
        /// <param name="items">list of slugs</param>
        /// <param name="slugSeparator"></param>
        /// <returns>Slug for url</returns>
        public static string GenerateUniqueSlug<T>(this string incomingString, List<T> items, string slugSeparator = "-") where T : ISlug
        {
            var slug = GenerateSlug(incomingString, slugSeparator);
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
