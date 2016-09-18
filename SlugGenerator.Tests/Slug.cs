using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlugGenerator.Tests
{
    public class ConcreteSlug : ISlug
    {
        public string Text { get; set; }
        public string Slug { get; set; }
    }
}
