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

    public struct accumulators
    {
        public long subtractive;
        public long additive;
        public long irregularSubtractive;
    }

    static List<int> numbersOutput { get; set; }
    static Dictionary<Numerals, Char> numeralPairs { get; set; }

    static void Main(string[] args)
    {
        numeralPairs = SetNumeralPairs();
        string input = "";
        bool done = false;
        accumulators accum;

        Console.WriteLine("Roman Numeral Parser - CP Nelson v1.0\n");
        PostHelpMSG();

        while (true)
        {
            input = GetStringToken();

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

    static void PostHelpMSG()
    {
        Console.WriteLine("\n\n[GUIDE]\n");
        Console.WriteLine("Supported base 10 Roman Numeral glyphs: ");
        Console.WriteLine("M = 1000; D = 500; C = 100; L = 50; X = 10; V = 5; I = 1");
        Console.WriteLine("Lower case converts to upper.");
        Console.WriteLine("Unsupported glyphs are evaluated as 0 (ZERO).");
        Console.WriteLine(
            "\n[Subtractive notation] is lower value before higher value, input left to right left to right in " +
            "single pair regular subtractive notation only (e.g. IV = 4, CD = 400), "
        );
        Console.WriteLine("[(ISN) Irregular Subtractive Notation] is not supported notation (e.g. IIIX = 7).");
        Console.WriteLine("Inverse order inputting will result in a regular subtractive tally.");
        Console.WriteLine("For regular subtractive notation calculations (E.G. IVX = (-1 + -5 + 10) = 4).");
        Console.WriteLine("\n[Additive notation], performs simple addition on glyphs. Useful for simple numbers.\n");
    }

    static string GetStringToken()
    {
        Console.Write("\n\nInput Roman Numeral to convert to integer: ");
        string input = "";


        while (String.IsNullOrEmpty(input))
        {
            input = Console.ReadLine();
        }
        
        return input.Replace(" ", "").ToUpper();
    }

    static void WriteIntTokens(List<int> intVals)
    {
        foreach (var IntToken in intVals)
        {
            Console.WriteLine(IntToken);
        }
    }

    static List<int> DiassembleStringToTokens(string Inputs)
    {
        List<int> retInts = new List<int>();
        Char[] glyphs = Inputs.ToCharArray();

        retInts = ConvertToInt(glyphs);

        return retInts;
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

    static void WriteIntTokens(List<int> numbers, accumulators accum, string inputString)
    {
        Console.WriteLine("Numeral Input: " + inputString);
        Console.WriteLine("Token Raw Val: ");

        foreach (var IntToken in numbers)
        {
            Console.Write("\t" + IntToken);
        }

        Console.WriteLine("\n");

        Console.WriteLine("Subtractive Notation Value:" + accum.subtractive);
        Console.WriteLine("Additive Notation Value:" + accum.additive);
        Console.WriteLine("Irregular Subtractive Notation Value:" + accum.irregularSubtractive);
    }

    static accumulators AssembleOutput(List<int> numbersSet)
    {
        int current;
        int current2;
        int next;
        int[] next2 = new int[numbersSet.Count];

        int Size = numbersSet.Count;
        int currentMAX;
        accumulators retAccum;
        List<int> sortedSet = new List<int>();
        List<int> irregularSubtractivePatternInts = new List<int>();
        List<int> irregularAdditivePatternInts = new List<int>();

        retAccum.subtractive = 0;
        retAccum.additive = 0;
        
        for (current = 0; current < Size; current++)
        {
            next = current + 1;

            if (next < Size)
            {
                if (numbersSet[current] < numbersSet[next])
                {
                    retAccum.subtractive = retAccum.subtractive + (-numbersSet[current]);
                    retAccum.additive = retAccum.additive + numbersSet[current];
                }
                else
                {
                    retAccum.subtractive = retAccum.subtractive + numbersSet[current];
                    retAccum.additive = retAccum.additive + numbersSet[current];
                }
            }
            else
            {
                retAccum.subtractive = retAccum.subtractive + numbersSet[current];
                retAccum.additive = retAccum.additive + numbersSet[current];
            }
        }

        currentMAX = 0;
        retAccum.irregularSubtractive = 0;

        foreach (var sourceSetItem in numbersSet)
        {
            sortedSet.Add(sourceSetItem);
        }

        sortedSet.Sort();

        foreach (var sIntItem in sortedSet)
        {
            retAccum.irregularSubtractive -= sIntItem;
        }

        retAccum.irregularSubtractive += sortedSet[sortedSet.Count - 1];
        retAccum.irregularSubtractive += sortedSet[sortedSet.Count - 1];

        return retAccum;
    }


    static bool IsDone()
    {
        Console.WriteLine("Press any key to continue, except for [Q/H/?].");
        Console.WriteLine("If Done press [Q], if needed press [H/?] for help.");

        char KEY = (char)Console.Read();
        char[] QUITCHAR = { 'Q', 'q' };
        char[] TESTCHAR = { 'T', 't' };
        char[] HELPCHAR = { 'H', 'h', '?' };

        if (KEY.Equals(HELPCHAR[0]) || KEY.Equals(HELPCHAR[1]) || KEY.Equals(HELPCHAR[2]))
        {
            PostHelpMSG();
        }

        return KEY.Equals(QUITCHAR[0]) || KEY.Equals(QUITCHAR[1]);
    }

}
}