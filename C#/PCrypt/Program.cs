using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PermutationCryptoSystems
{
    class PCrypt
    {
        IEnumerable<char> _input { get; set; }
        public string Input
        {
            get
            {
                return _input.ToString();
            }
            set
            {
                _input = value.ToArray<char>();
            }
        }
        public string Key { get; set; }

        void _swap(char[] value, int i, int j)
        {
            var a = value[i];
            value[i] = value[j];
            value[j] = a;
        }

        IEnumerable<int> _char_to_int(IEnumerable<char> value)
        {
            return value.Select(x => (int)(Int16)x);
        }

        IEnumerable<char> _int_to_char(IEnumerable<int> value)
        {
            return value.Select(x => (char)x);
        }

        IEnumerable<char> _swap(IEnumerable<char> value)
        {
            var a = value.ToArray();

            for (int i = 0; i < a.Length / 2; i++)
            {
                if ((i % 2) == 0)
                {
                    _swap(a, i, (a.Length - 1) - i);
                }
            }

            return a;
        }

        internal IEnumerable<char> Swap(IEnumerable<char> value)
        {
            var a = (int)(value.Count() * 0.75);

            return _swap(value.Skip(a).Concat(value.Take(a)));
        }

        internal IEnumerable<char> UnSwap(IEnumerable<char> value)
        {
            var a = value.Count();
            var b = (int)(a * 0.75);
            var c = _swap(value);

            return c.Skip(a - b).Take(b).Concat(c.Take(a - b));
        }

        internal IEnumerable<char> DiffEncoding(IEnumerable<char> value)
        {
            var a = _char_to_int(value).ToArray();
            var b = new int[a.Length];
            b[0] = a[0];

            for (int i = 1; i < a.Length; i++)
            {
                b[i] = a[i - 1] - a[i];
            }

            return _int_to_char(b).Reverse();
        }

        internal IEnumerable<char> DiffDecoding(IEnumerable<char> value)
        {
            var a = _char_to_int(value).Reverse().ToArray();

            for (int i = 1; i < a.Length; i++)
            {
                a[i] = a[i - 1] - a[i];
            }

            return _int_to_char(a);
        }

        internal IEnumerable<char> XOR(IEnumerable<char> value, int xor)
        {
            return _int_to_char(_char_to_int(value).Select(x => x ^ xor));
        }

        internal int XOR(int value, int xor)
        {
            return value ^ xor;
        }

        internal IEnumerable<char> ZigZag(IEnumerable<char> value)
        {
            return value.Where((x, i) => i % 2 == 0).Concat(value.Where((x, i) => i % 2 != 0));
        }

        internal IEnumerable<char> ZagZig(IEnumerable<char> value)
        {
            var a = value.Take((int)Math.Ceiling(value.Count() / 2d)).ToArray();
            var b = value.Skip(a.Count()).ToArray();

            var c = "";

            return c;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string input = "find it posted below each video...while these are not necessarily a wordby-word transcript of what I say in the videos, they do cover the exact same material and provide the code that I type in so you can copy and paste it into your app. I'm providing this for those that have a hearing disability and for those who don't use English as their primary language. Also, it should be helpful for reference purposes so that you don't have to go back through";


            var a = String.Join("", new PCrypt().ZigZag("12345"));



            var b = String.Join("", new PCrypt().ZagZig(a));

            Console.WriteLine(a + " -- " + b);


            Console.Read();

        }
    }
}
