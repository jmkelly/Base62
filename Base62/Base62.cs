using System.Numerics;
using System.Text;

namespace Base62
{
    public class Base62
    {
        private readonly char[] charSet = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private const int bits = 62;


        public string Encode(byte[] bytes)
        {
            StringBuilder sb = new();
            BigInteger number = new(bytes, isUnsigned: true, isBigEndian: true);

            while (number > 0)
            {
                //now we divide by 62 and record the remainder
                number = BigInteger.DivRem(number, bits, out BigInteger remainder);
                //insert at the start of the string
                sb.Insert(0,charSet[(int)remainder]);
            }

            return sb.ToString();
        }

        public string Decode(string base62)
        {
            char[] chars = base62.ToCharArray();
            BigInteger number = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                //convert each char to its charset number to get the remainder, then add
                //onto the product of the number and the bits
                number = (number * bits) + Array.IndexOf(charSet, chars[i]);
            }
            //convert number to bytes
            byte[] bytes = number.ToByteArray(isUnsigned: true, isBigEndian: true);
            //convert bytes to UTF8 encoded string
            return Encoding.UTF8.GetString(bytes);

        }


        public string Encode(string value)
        {
            //get the binary in utf8
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Encode(bytes);
        }
    }
}



