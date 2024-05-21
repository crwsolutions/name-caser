# name-caser
Extension methods to convert Pascal casing to other casings

[![NuGet version (NameCaser)](https://img.shields.io/nuget/v/NameCaser?color=blue)](https://www.nuget.org/packages/NameCaser/)

Examples:
```csharp
using NameCaser;

var simple = "SomeSimpleString";
Console.WriteLine(simple.ToCamelCase());         // someSimpleString
Console.WriteLine(simple.ToSnakeCase());         // some_simple_string
Console.WriteLine(simple.ToKebabCase());         // some-simple-string
Console.WriteLine(simple.ToSpaceCase());         // Some simple string
Console.WriteLine(simple.ToConstantCase());      // SOME_SIMPLE_STRING
Console.WriteLine(simple.ToTrainCase());         // Some-Simple-String

var ioDriver = "IODriver";
Console.WriteLine(ioDriver.ToCamelCase());        // ioDriver
Console.WriteLine(ioDriver.ToSnakeCase());        // io_driver
Console.WriteLine(ioDriver.ToKebabCase());        // io-driver
Console.WriteLine(ioDriver.ToSpaceCase());        // IO driver
Console.WriteLine(ioDriver.ToConstantCase());     // IO_DRIVER
Console.WriteLine(ioDriver.ToTrainCase());        // IO-Driver

var someUTPCable = "SomeUTPCable";
Console.WriteLine(someUTPCable.ToCamelCase());    // someUTPCable
Console.WriteLine(someUTPCable.ToSnakeCase());    // some_utp_cable
Console.WriteLine(someUTPCable.ToKebabCase());    // some-utp-cable
Console.WriteLine(someUTPCable.ToSpaceCase());    // Some UTP cable
Console.WriteLine(someUTPCable.ToConstantCase()); // SOME_UTP_CABLE
Console.WriteLine(someUTPCable.ToTrainCase());    // Some-UTP-Cable
```

## Supports the conversion to the following casings

### 1. camelCase

Camel Case is actually inspired from animal “Camel”. Where first word will be small letters and from second word, first character will be captialized like camelCase.

> Camel Case is often used for property Naming in typescript

### 2. snake_case

Snake Case is naming with words separated by _ ( underscore ) and all small letters

> Snake Case is often used for file Naming

### 3. kebab-case

Kebab Case is naming with words separated by — ( hyphen ) with all small letters

> Kebab Case is often used for CSS naming

### 4. Space case

Space case is naming with words separated by ' ' ( space ) and all small letters

> Space Case is often used auto generating comments

### 5. CONSTANT_CASE

Constant case is naming with all letters Capitalized but the words are separated by _ ( underscore)

> Constant case is often used for constants and macros

### 6. Train-Case

Train Case is naming with first character of every word of name is Capitalised and words are connected with — ( hyphen ).

> Train Case is also often used for file Naming
