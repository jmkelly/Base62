using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Base62.Benchmarks
{
	[MemoryDiagnoser]
    public class EncoderBenchmark
    {
		private readonly byte[] buffer = new byte[64];


        public EncoderBenchmark()
        {
            Encoder = new Base62Encoder();
        }

        public Base62Encoder Encoder { get; }

        [Benchmark]
        public string Base62Encoder()
        {
            return Encoder.Encode(buffer);
        }


        [Benchmark(Baseline = true)]
        public string DotNetBase64()
        {
            return Convert.ToBase64String(buffer);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<EncoderBenchmark>();
        }
    }
}
