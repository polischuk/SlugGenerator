# What is SlugGenerator?
SlugGenerator it's simple Slug and clean URL generator. Supports multiple languages such as:
Cyrillic, Latin, Chinese and other languages encodings

## Where can I get it?
Install from the package manager console:

    PM> Install-Package SlugGenerator

## Slug generation
### Simple usage
```cs
  using SlugGenerator;
  
  "my test text".GenerateSlug(); // return my-test-text
```
### Using Custom space separator
```cs
  using SlugGenerator;
  
  "my test text".GenerateSlug("_"); // set "_" as separator and return "my_test_text" string
```

## Multilanguage feature
Slug generator transliteration all basic languages to english charters

```cs
  using SlugGenerator;
  
  // Russian language
  "привет как дела".GenerateSlug(); // return privet-kak-dela
  "你好你怎麼樣".GenerateSlug(); // return ni-hao-ni-zen-mo-yang-
```
## Generate Unique slug 
This method help u Generate unique slug, if slug already exist on list, method generate slug with additional numbers or guid.
```cs
  using SlugGenerator;
    public class ConcreteSlug : ISlug
    {
        public string Slug { get; set; }
    }
  
  
    var slug = item.Text.GenerateUniqueSlug(items);
    var itemsList = new List<ConcreteSlug>
            {
                new ConcreteSlug
                {
                    Slug = "test"
                },
                new ConcreteSlug
                {
                    Slug = "test2"
                }
            };
    var slug = "test".GenerateUniqueSlug(itemsList); // return slug which is not in an itemsList
```
