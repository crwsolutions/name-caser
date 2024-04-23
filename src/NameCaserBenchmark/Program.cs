using BenchmarkDotNet.Running;

internal class Program
{

    private static void Main(string[] args)
    {
        //var result = BenchmarkRunner.Run<BenchMarksKebabCasing>();
        var result = BenchmarkRunner.Run<BenchMarksConstantCasing>();

    }
}

