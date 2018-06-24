using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Unidecode;

namespace SlugGenerator
{
    public static class SlugGenerator
    {
        /// <summary>
        /// Method generate slug by text (multiple languages)
        /// </summary>
        /// <param name="incomingString">Text for slug</param>
        /// <param name="slugSeparator">Slug separator, example "-" or "_"</param>
        /// <returns>Slug for url</returns>
        public static string GenerateSlug(this string incomingString, string slugSeparator = "-")
        {
            incomingString = incomingString.UniDecode();
            var alphaNum = Regex.Replace(incomingString, @"[^a-zA-Z0-9\s]", string.Empty);
            alphaNum = Regex.Replace(alphaNum, @"\s+", slugSeparator);
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

        /// <summary>
        /// Method generate slug by text (multiple languages)
        /// </summary>
        /// <param name="incomingString">Text for slug</param>
        /// <param name="expression"></param>
        /// <param name="slugSeparator">Slug separator, example "-" or "_"</param>
        /// <param name="items">list of exist items</param>
        /// <returns>Slug for url</returns>
        public static string GenerateUniqueSlug<TSource>(this string incomingString, List<TSource> items, Expression<Func<TSource, string>> expression, string slugSeparator = "-")
        {
            var propInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
            var slug = GenerateSlug(incomingString, slugSeparator);
            if (items.Any(m => propInfo?.GetValue(m)?.ToString() == slug))
            {
                slug += slug.GetHashCode();
                if (items.Any(m => propInfo?.GetValue(m)?.ToString() == slug))
                {
                    slug += Guid.NewGuid();
                }
            }
            return slug;
        }
    }
}
