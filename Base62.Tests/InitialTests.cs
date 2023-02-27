using Xunit;
using Shouldly;
using Xunit.Abstractions;
using System.Text;
using System.Numerics;

namespace Base62.Tests
{
    public class InitialTests
    {
        private readonly ITestOutputHelper output;

        public InitialTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("H", "1A")]
        [InlineData("a", "1Z")]
        [InlineData("Z", "1S")]
        [InlineData("1", "n")]
        [InlineData("9", "v")]
        [InlineData("z", "1y")]
        [InlineData("He", "4ov")]
        [InlineData("Hello, world!", "1wJfrzvdbthTq5ANZB")]
        public void EncoderTest(string input, string encoded)
        {
            var encoder = new Base62Encoder();
            encoder.Encode(input).ShouldBe(encoded);
        }

        [Theory]
        [InlineData("H")]
        [InlineData("e")]
        [InlineData("He")]
        [InlineData("Hello, world!")]
        public void ToBytesToBigIntegerTest(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var number = new BigInteger(bytes);
            output.WriteLine("BigInt: {0}", number);
            output.WriteLine("Mod62: {0}", number % 62);
            output.WriteLine("Remainder: {0}", number / 62);
        }
    }
}
