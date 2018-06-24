using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SlugGenerator.Tests
{
    [TestFixture]
    public class SlugGeneratorTests
    {
        [Test, TestCaseSource(nameof(GenerateSlugTestCases))]
        public void GenerateSlug_WithValidData_ShouldReturnSlugString(string text)
        {
            var slug = text.GenerateSlug();
            Assert.IsNotNull(slug);
            Console.WriteLine(slug);
        }
        [Test, TestCaseSource(nameof(GenerateSlugTestCases))]
        public void GenerateSlug_WithValidDataAndOtherSlugSeparator_ShouldReturnSlugString(string text)
        {
            var slug = text.GenerateSlug("_");
            Assert.IsNotNull(slug);
            Console.WriteLine(slug);
        }
        [Test, TestCaseSource(nameof(GenerateUniqueSlugTestCases))]
        public void GenerateUniqueSlug_WithValidData_ShouldReturnUniqueSlugString(List<ConcreteSlug> items)
        {
            foreach (var item in items)
            {
                item.Slug = item.Text.GenerateUniqueSlug(items);
                Assert.IsNotNull(item.Slug);
                Console.WriteLine(item.Slug);
                var count = items.Count(concreteSlug => concreteSlug.Slug == item.Slug);
                Assert.IsTrue(count == 1);
            }
        }
        [Test, AutoData]
        public void GenerateUniqueSlugAndCustomProperty_WithRandomText_ShouldReturnSlugString(List<Entity> items)
        {
            foreach (var item in items)
            {
                item.SlugProperty = item.Text.GenerateUniqueSlug(items, i => i.SlugProperty);
                Assert.IsNotNull(item.SlugProperty);
                Console.WriteLine(item.SlugProperty);
                var count = items.Count(concreteSlug => concreteSlug.SlugProperty == item.SlugProperty);
                Assert.IsTrue(count == 1);
            }
        }
        [Test, TestCaseSource(nameof(GenerateEntityListUniqueSlugTestCases))]
        public void GenerateUniqueSlugAndCustomProperty_WithSameText_ShouldReturnUniqueSlugString(List<Entity> items)
        {
            foreach (var item in items)
            {
                item.SlugProperty = item.Text.GenerateUniqueSlug(items, i => i.SlugProperty);
                Assert.IsNotNull(item.SlugProperty);
                Console.WriteLine(item.SlugProperty);
                var count = items.Count(concreteSlug => concreteSlug.SlugProperty == item.SlugProperty);
                Assert.IsTrue(count == 1);
            }
        }
        private static IEnumerable<TestCaseData> GenerateSlugTestCases
        {
            get
            {
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData(null),
                    new TestCaseData("The quick brown fox jumps over the lazy dog"),
                    new TestCaseData("Тёмно-рыжая лиса прыг через лентяя-пса"),
                    new TestCaseData("Дует на море циклон, попадает на Цейлон"),
                    new TestCaseData("My test article title"),
                    new TestCaseData("Der schnelle braune Fuchs springt über den faulen Hund"),
                    new TestCaseData("빠른 갈색 여우가 게으른 개 점프"),
                    new TestCaseData("Тестируем223 разную КІРїЛЁЦґ"),
                    new TestCaseData("So            so                    long                   spaces  in                                                 string")
                };
                return testCaseData;
            }
        }
        private static IEnumerable<TestCaseData> GenerateUniqueSlugTestCases
        {
            get
            {
                var slugListItems = new List<ConcreteSlug>
                {
                    new ConcreteSlug
                    {
                        Text = "Дуетhg на море циклон, попадает на Цейлон"
                    },
                    new ConcreteSlug
                    {
                        Text = "Дует на море циклон, попадает на Цейлон"
                    },
                    new ConcreteSlug
                    {
                        Text = "Дует на море циклон, попадает на Цейлон"
                    },
                    new ConcreteSlug
                    {
                        Text = "Дует на море циклон, попадает на Цейлон"
                    },
                    new ConcreteSlug
                    {
                        Text = "Дует на море циклон123, попадает на Цейлон"
                    }
                };
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData(slugListItems)
                };
                return testCaseData;
            }
        }
        private static IEnumerable<TestCaseData> GenerateEntityListUniqueSlugTestCases
        {
            get
            {
                var slugListItems = new List<Entity>
                {
                    new Entity
                    {
                        Text = "Дует на море циклон, попадает на Цейлон"
                    },
                    new Entity
                    {
                        Text = "Дует на море циклон, попадает на Цейлон"
                    },
                    new Entity
                    {
                        Text = "Дует на море циклон, попадает на Цейлон"
                    }
                };
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData(slugListItems)
                };
                return testCaseData;
            }
        }
    }
}
