using BenchmarkDotNet.Running;
using NameCaserBenchmark;
using NameCaser;

internal class Program
{
    private static void Main()
    {

        //var x = new BenchMarksConstantCasing();
        //var s = x.ConstantCaseWithAnalyzerAndAction();

        //var result = BenchmarkRunner.Run<BenchMarksCamelCasing>();
        var result = BenchmarkRunner.Run<BenchMarksKebabCasing>();
        //var result = BenchmarkRunner.Run<BenchMarksTrainCasing>();
        //var result = BenchmarkRunner.Run<BenchMarksConstantCasing>();
        //var result = BenchmarkRunner.Run<BenchMarksSpaceCasing>();
        //var result = BenchmarkRunner.Run<BenchMarksPascalCasing>();
    }
}

