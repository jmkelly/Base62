using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Base62.Benchmarks
{
    public class EncoderBenchmark
    {
        private const int N = 10000;
        private readonly byte[] data;


        public EncoderBenchmark()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
            Encoder = new Base62Encoder();
        }

        public Base62Encoder Encoder { get; }

        [Benchmark]
        public string Encode()
        {
            return Encoder.Encode(data);
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
