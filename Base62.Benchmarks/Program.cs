using System;
using System.Buffers.Text;
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



        [Benchmark(Baseline = true)]
        public string DotNetSystemTextBufferBase64EncodToUt8InPlace()
        {
            Span<byte> span = buffer.AsSpan();
            _ = Base64.EncodeToUtf8InPlace(span, span.Length, out _);
			return span.ToString();
        }

        [Benchmark()]
        public string DotNetSystemConvertToBase64()
        {
            return Convert.ToBase64String(buffer);
        }

        [Benchmark]
        public string Base62Encoder()
        {
            return Encoder.Encode(buffer);
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
