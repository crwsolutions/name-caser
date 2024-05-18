﻿using BenchmarkDotNet.Running;
using NameCaserBenchmark;

internal class Program
{

    private static void Main(string[] args)
    {
        var x = new BenchMarksConstantCasing();
        var s = x.ConstantCaseWithAnalyzerAsBytes();

        //var result = BenchmarkRunner.Run<BenchMarksKebabCasing>();
        //var result = BenchmarkRunner.Run<BenchMarksConstantCasing>();
        var result = BenchmarkRunner.Run<BenchMarksCamelCasing>();
    }
}

