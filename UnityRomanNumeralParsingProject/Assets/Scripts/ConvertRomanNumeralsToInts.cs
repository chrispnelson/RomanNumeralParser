using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanNumeralParser.Converter
{
    public class ConvertRomanNumeralsToInts
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

        public struct Accumulators
        {
            public long subtractive;
            public long additive;
            public long irregular;
        }
        
        public struct Attibutes
        {
            public List<int> numbersOutput { get; set; }
            public Dictionary<Numerals, char> numeralPairs { get; set; }
            public string input;
            public bool done;
            public Accumulators accumulators;
        }

        public ConvertRomanNumeralsToInts()
        {
            this.Init();
        }

        public Attibutes Init()
        {
            Attibutes attribs = new Attibutes();
            
            attribs.numeralPairs = new Dictionary<Numerals, char>(); 
            attribs.numeralPairs = SetNumeralPairs();
            attribs.input = "";
            attribs.done = false;
            attribs.accumulators.additive = 0;
            attribs.accumulators.subtractive = 0;
            attribs.accumulators.irregular = 0;

            Debug.Log("Roman Numeral Parser - CP Nelson v1.0");
            PostHelpMSG();

            return attribs;
        }

        public Attibutes SendInputsToAccumulator(string inputsString)
        {
            Attibutes attribs = new Attibutes();
        
            attribs.input = GetCleanedStringToken(inputsString);
            
            attribs.numbersOutput = DiassembleStringToIntTokens(attribs.input);

            attribs.accumulators = AssembleOutput(attribs.numbersOutput);

            WriteIntTokens(attribs);

            attribs.done = IsDone();

            return attribs;
        }

        public static Dictionary<Numerals, char> SetNumeralPairs()
        {
            Dictionary<Numerals, char> DictlocalSet = new Dictionary<Numerals, char>(); 

            DictlocalSet.Add(Numerals.I, 'I');
            DictlocalSet.Add(Numerals.V, 'V');
            DictlocalSet.Add(Numerals.X, 'X');
            DictlocalSet.Add(Numerals.L, 'L');
            DictlocalSet.Add(Numerals.C, 'C');
            DictlocalSet.Add(Numerals.D, 'D');
            DictlocalSet.Add(Numerals.M, 'M');

            return DictlocalSet;
        }

        private static void PostHelpMSG()
        {
            Debug.Log(
                "[HELP INFO GUIDE]\n" +
                "Supported base 10 Roman Numeral glyphs: " +
                "M = 1000; D = 500; C = 100; L = 50; X = 10; V = 5; I = 1" +
                "Lower case converts to upper." +
                "Unsupported glyphs are evaluated as 0 (ZERO)." +
                "\n[Subtractive notation] is lower value before higher value, input left to right left to right in " +
                "single pair regular subtractive notation only (e.g. IV = 4, CD = 400), " +
                "[(ISN) Irregular Subtractive Notation] works for small values, but is not fully supported notation (e.g. IIIX = 7)." +
                "Inverse order inputting will result in a regular subtractive tally." +
                "For regular subtractive notation calculations (E.G. IVX = (-1 + -5 + 10) = 4)." +
                "\n[Additive notation], performs simple addition on glyphs. Useful for simple numbers.\n"
            );
        }

        public string GetCleanedStringToken(string InputString = "")
        {
            return InputString.Replace(" ", "").ToUpper();
        }

        public void WriteIntTokens(List<int> intVals)
        {
            foreach (var IntToken in intVals)
            {
                Debug.Log(IntToken);
            }
        }

        public List<int> DiassembleStringToIntTokens(string Inputs)
        {
            List<int> retInts = new List<int>();
            char[] glyphs = Inputs.ToCharArray();

            retInts = ConvertToInts(glyphs);

            return retInts;
        }

        public List<int> ConvertToInts(char[] glyphs)
        {
            int Size = glyphs.Length;
            List<int> integerSet = new List<int>();

            if (Size < 1)
            {
                integerSet.Add(0);
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    integerSet.Add(GetIntValue(glyphs[i]));
                }
            }

            return integerSet;
        }

        public Numerals GetNumerals(char searchChar)
        {
            Numerals key = Numerals.NONE;

            foreach (var pair in SetNumeralPairs())
            {
                if (pair.Value == searchChar)
                {
                    key = pair.Key;
                    break;
                }
            }

            return key;
        }

        public int GetIntValue(Numerals numeral)
        {
            return (int)GetNumerals(SetNumeralPairs()[numeral]);
        }

        public int GetIntValue(char numeral)
        {
            return (int)GetNumerals(numeral);
        }

        public void WriteIntTokens(Attibutes attributeSet)
        {
            string loggingOutput = "[RESULTS]\n";

            loggingOutput = loggingOutput + "Numeral Input: " + attributeSet.input + "\n";
            loggingOutput = loggingOutput + "Token Raw Val: " + "\n";

            foreach (var IntToken in attributeSet.numbersOutput)
            {
                loggingOutput = loggingOutput + " " + IntToken;
            }

            loggingOutput = loggingOutput + "\n";

            loggingOutput = loggingOutput + "Subtractive Notation Value: " + attributeSet.accumulators.subtractive + "\n";
            loggingOutput = loggingOutput + "Additive Notation Value: " + attributeSet.accumulators.additive + "\n";
            loggingOutput = loggingOutput + "Irregular Subtractive Notation Value: " + attributeSet.accumulators.irregular + "\n";
            
            Debug.Log(loggingOutput);
        }

        public Accumulators CalculateAdditiveValue(Accumulators acumulatorSet, List<int> numbersSet)
        {
            Accumulators retAccum = new Accumulators();
            int current;
            int next;
            int Size = numbersSet.Count;

            retAccum = acumulatorSet;
            
            for (current = 0; current < Size; current++)
            {
                next = current + 1;

                if (next < Size)
                {
                    if (numbersSet[current] < numbersSet[next])
                    {
                        retAccum.additive = retAccum.additive + numbersSet[current];
                    }
                    else
                    {
                        retAccum.additive = retAccum.additive + numbersSet[current];
                    }
                }
                else
                {
                    retAccum.additive = retAccum.additive + numbersSet[current];
                }
            }

            return retAccum;
        }

        public Accumulators CalculateSubtractiveValue(Accumulators acumulatorSet, List<int> numbersSet)
        {
            Accumulators retAccum = new Accumulators();
            int current;
            int next;
            int Size = numbersSet.Count;

            retAccum = acumulatorSet;
            
            for (current = 0; current < Size; current++)
            {
                next = current + 1;

                if (next < Size)
                {
                    if (numbersSet[current] < numbersSet[next])
                    {
                        retAccum.subtractive = retAccum.subtractive + (-numbersSet[current]);
                    }
                    else
                    {
                        retAccum.subtractive = retAccum.subtractive + numbersSet[current];
                    }
                }
                else
                {
                    retAccum.subtractive = retAccum.subtractive + numbersSet[current];
                }
            }

            return retAccum;
        }

        public Accumulators CalculateIrregularValue(Accumulators acumulatorSet, List<int> numbersSet)
        {
            Accumulators retAccum;
            List<int> sortedSet = new List<int>();
            
            retAccum = acumulatorSet;
            
            foreach (var sourceSetItem in numbersSet)
            {
                sortedSet.Add(sourceSetItem);
            }

            sortedSet.Sort();

            foreach (var sIntItem in sortedSet)
            {
                retAccum.irregular -= sIntItem;
            }

            retAccum.irregular += sortedSet[sortedSet.Count - 1];
            retAccum.irregular += sortedSet[sortedSet.Count - 1];

            return retAccum;
        }

        public Accumulators AssembleOutput(List<int> numbersSet)
        {
            Accumulators retAccum;

            retAccum.subtractive = 0;
            retAccum.additive = 0;
            retAccum.irregular = 0;

            retAccum = CalculateAdditiveValue(retAccum, numbersSet);
            retAccum = CalculateSubtractiveValue(retAccum, numbersSet);
            retAccum = CalculateIrregularValue(retAccum, numbersSet);

            return retAccum;
        }


        public bool IsDone()
        {
            return true;
        }

    }
}

