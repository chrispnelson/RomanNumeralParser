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

        public struct accumulators
        {
            public long subtractiveNum;
            public long additiveNum;
        }

         void Main(string[] args)
        {
            numeralPairs = SetNumeralPairs();
            accumulators accum;
            bool done = false;

            while (true)
            {
                Console.WriteLine("Roman Numeral Parser - CP Nelson v1.0");
                string input = GetStringTokensFromInput();

                numbersOutput = DiassembleStringToTokens(input);

                accum = AssembleOutput(numbersOutput);

                WriteIntTokens(numbersOutput, accum, input);

                done = IsDone();

                if (done)
                {
                    break;
                }
            }
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

        public static string GetStringTokensFromInput(string inputs = "\0", bool testMode = false)
        {
            string input;

            Console.WriteLine("[GUIDE]\n");
            Console.WriteLine("Supported base 10 Roman Numeral glyphs: ");
            Console.WriteLine("M = 1000; D = 500; C = 100; L = 50; X = 10; V = 5; I = 1");
            Console.WriteLine("Lower case converts to upper.");
            Console.WriteLine("Unsupported glyphs are evaluated as 0 (ZERO).");
            Console.WriteLine("\n[Subtractive notation] is single pair regular subtractive notation only (e.g. IV = 4, CD = 400),");
            Console.WriteLine("and NOT (ISN) irregular subtractive notation (e.g. IIIX = 7 is not supported notation).");
            Console.WriteLine("Inverse order inputting will result in a regular subtractive tally.");
            Console.WriteLine("For regular subtractive notation calculations (E.G. IVX = (-1 + -5 + 10) = 4 and NOT 6).");
            Console.WriteLine("\n[Additive notation], performs simple addition on glyphs. Useful for simple numbers.");
            Console.Write("\n\nInput Roman Numeral to convert to integer: ");

            //if (inputs.Length <= 0 || String.IsNullOrEmpty(inputs) || inputs.Equals("\0"))
            // if (testMode == false)
            // {
            //     input = Console.ReadLine();
            // }
            // else
            // {
            //     input = inputs;
            // }

            input = Console.ReadLine();

            return input.Replace(" ", "").ToUpper();
        }

        static void WriteIntTokens(List<int> numbers, accumulators accum, string inputString)
        {
            Console.WriteLine("Numeral Input: " + inputString);
            Console.WriteLine("Token Raw Val: ");

            foreach (var IntToken in numbers)
            {
                Console.Write("\t" + IntToken);
            }

            Console.WriteLine("\n");

            Console.WriteLine("Subtractive Notation Value:" + accum.subtractiveNum);
            Console.WriteLine("Additive Notation Value:" + accum.additiveNum);
        }

        static List<int> DiassembleStringToTokens(string Inputs)
        {
            Char[] glyphs = Inputs.ToCharArray();
            List<int> numOutput = new List<int>();

            numOutput = ConvertToInt(glyphs);

            return numOutput;
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

        static accumulators AssembleOutput(List<int> numbersSet)
        {
            int current;
            int next;
            int Size = numbersSet.Count;
            accumulators retAccum;

            retAccum.subtractiveNum = 0;
            retAccum.additiveNum = 0;

            for (current = 0; current < Size; current++)
            {
                next = current + 1;

                if (next < Size)
                {
                    if (numbersSet[current] < numbersSet[next])
                    {
                        retAccum.subtractiveNum = retAccum.subtractiveNum + (-numbersSet[current]);
                        retAccum.additiveNum = retAccum.additiveNum + numbersSet[current];
                    }
                    else
                    {
                        retAccum.subtractiveNum = retAccum.subtractiveNum + numbersSet[current];
                        retAccum.additiveNum = retAccum.additiveNum + numbersSet[current];
                    }
                }
                else
                {
                    retAccum.subtractiveNum = retAccum.subtractiveNum + numbersSet[current];
                    retAccum.additiveNum = retAccum.additiveNum + numbersSet[current];
                }
            }

            return retAccum;
        }

        static bool IsDone()
        {
            Console.Write("If Done, press [Q|q]! : ");

            char KEY = (char)Console.Read();
            char[] QUITCHAR = { 'Q', 'q' };
            char[] TESTCHAR = { 'T', 't' };

            return KEY.Equals(QUITCHAR[0]) || KEY.Equals(QUITCHAR[1]);
        }
    }
}
