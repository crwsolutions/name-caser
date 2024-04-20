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
