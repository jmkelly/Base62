using System.Buffers.Text;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Base62.Benchmarks
{
    [MemoryDiagnoser]
    public class EncoderBenchmark
    {
		private readonly byte[] buffer = new byte[64];
		private readonly string text = Encoding.UTF8.GetString(new byte[64]);


        public EncoderBenchmark()
        {
            Base62 = new Base62();
        }

        public Base62 Base62 { get; }



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

        //[Benchmark()]
        //public byte[] DotNetSystemConvertFromBase64()
        //{
        //    return Convert.FromBase64String(text);
        //}

        [Benchmark]
        public string Base62Encoder()
        {
            return Base62.Encode(buffer);
        }

        //[Benchmark]
        //public string Base62Decoder()
        //{
        //    return Base62.Decode(text);
        //}
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<EncoderBenchmark>();
        }
    }
}
