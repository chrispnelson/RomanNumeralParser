using System;
using System.Collections.Generic;

namespace RomanNumeralParser
{
    class Program
    {
        public enum Numerals
        {
            NONE = 0,
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000
        }
        
        static List<int> numbersOutput { get; set; }
        static Dictionary<Numerals, Char> numeralPairs { get; set; }

        static void Main(string[] args)
        {
            numeralPairs = SetNumeralPairs();
            Console.WriteLine("Roman Numeral Parser - CP Nelson v1.0");
            string input = GetStringToken();

            DiassembleStringToTokens(input);

            WriteIntTokens();
        }

        static Dictionary<Numerals, Char> SetNumeralPairs()
        {
            Dictionary<Numerals, Char> localSet = new Dictionary<Numerals, Char>();

            localSet.Add(Numerals.I, 'I');
            localSet.Add(Numerals.V, 'V');
            localSet.Add(Numerals.X, 'X');
            localSet.Add(Numerals.L, 'L');
            localSet.Add(Numerals.C, 'C');
            localSet.Add(Numerals.D, 'D');
            localSet.Add(Numerals.M, 'M');

            return localSet;
        }

        static string GetStringToken()
        {
            Console.Write("Please input a roman Numeral to convert to integer:");
            string input = Console.ReadLine();

            return input;
        }

        static void WriteIntTokens()
        {
            foreach (var IntToken in numbersOutput)
            {
                Console.WriteLine(IntToken);
            }
        }

        static void DiassembleStringToTokens(string Inputs)
        {
            Char[] glyphs = Inputs.ToCharArray();

            numbersOutput = ConvertToInt(glyphs);
        }

        static List<int> ConvertToInt(Char[] glyphs)
        {
            List<int> integerSet = new List<int>();

            for (int i = 0; i < glyphs.Length; i++)
            {
                integerSet.Add(GetIntValue(glyphs[i]));
            }

            return integerSet;
        }

        static Numerals GetNumerals(Char searchChar)
        {
            Numerals key = Numerals.NONE;

            foreach (var pair in numeralPairs)
            {
                if (pair.Value == searchChar)
                {
                    key = pair.Key;
                    break;
                }
            }

            return key;
        }

        static int GetIntValue(Numerals numeral)
        {
            return (int)GetNumerals(numeralPairs[numeral]);
        }

        static int GetIntValue(Char numeral)
        {
            return (int)GetNumerals(numeral);
        }

        static int ConvertSingleTokenToInt(Numerals numerals)
        {
            return (int)numerals;
        }

        static void AssembleOutput(string Tokens, int[] Numerics)
        {

        }

        static bool IsSubtractiveToken(string Token)
        {
            return false;
        }

        static bool IsAdditiveToken(string Token)
        {
            return false;
        }

        static int additiveNumerics(int[] integerValues)
        {
            return 0;
        }

        static int subtractiveNumerics(int[] integerValues)
        {
            return 0;
        }
    }
}
