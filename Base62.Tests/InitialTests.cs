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
        [InlineData("This is quite a long sentence. Trying to see if we can get any test failures, but not sure how.  What about 0,1,2,3,4,5,2,1,30a, or other stuff?", "2MNo0MSS5sjbp0ofJxi47GifvGa9tW9f6CeqglrySvJT1hduJ4QqVSIXdQJKPKo8pH2is3JKLasoaJA1i5Tn0XgoBSdJmmiEepw02CQIUM6E3CW7JyEpf9VC4nQ9U2Fe2XXD6bHUAxshtWrWKd9uxtS3mFMWrIwz9AOAeUF3byElKscPH3pDJ54wVuxs3ayLap")]
		[InlineData("0", "m")]

        public void EncoderTest(string input, string encoded)
        {
            Base62Encoder encoder = new();
            encoder.Encode(input).ShouldBe(encoded);
        }
    }
}
