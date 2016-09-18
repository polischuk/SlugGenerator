#What is SlugGenerator?


##Where can I get it?
Install from the package manager console:

    PM> Install-Package SlugGenerator

##Slug generation
```cs
  using SlugGenerator;
  
  "my test text".GenerateSlug(); // return my-test-text
```

###Multilanguage feature
Slug generator transliteration all basic languages to english charters

```cs
  using SlugGenerator;
  
  // Russian language
  "my test text".GenerateSlug(); // return privet-kak-dela
  "你好你怎麼樣".GenerateSlug(); // return ni-hao-ni-zen-mo-yang-
```
