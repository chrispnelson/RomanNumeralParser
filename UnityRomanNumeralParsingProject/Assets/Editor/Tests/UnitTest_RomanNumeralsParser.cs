using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit;
using NUnit.Framework;
using NUnit.Compatibility;
using NUnit.Framework.Constraints;

namespace RomanNumeralParser.UnitTests
{
    [TestFixture]
    public class UnitTest_GetInput 
    {
        public enum NumeralVals
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

        private Dictionary<NumeralVals, char> numeralSets;

        [SetUp]
        public void Init()
        {
            numeralSets = new Dictionary<NumeralVals, char>()
            {
                {NumeralVals.I, 'I'},
                {NumeralVals.V, 'V'},
                {NumeralVals.X, 'X'},
                {NumeralVals.L, 'L'},
                {NumeralVals.C, 'C'},
                {NumeralVals.D, 'D'},
                {NumeralVals.M, 'M'}
            };
        }

        [TearDown]
        public void TearDownMethod()
        {
            
        }

        [Test]
        public void Test_AssertTest()
        {
            Assert.Pass();
        }

        [Test]
        [TestCase("mcmxiv", ExpectedResult = "MCMXIV")]
        public string Test_GetInput_InputRomanNumerals_RemoveSpaceSetToCaps(string inputs)
        {
            /*Arrange*/
            // Class Is RomanNumeralParser
            // Method Is GetInput
            
            
            /*Act*/
            // Pass inputs to GetInput return result
            // compare result to retInput
            string retInput = inputs.Replace(" ", "").ToUpper();
            
            /*Assert*/
            // GetInput return result and retInput are equal

            return retInput;
        }

        int ComputeSubtractiveNotationTally_ConvertHelper(char NumeralItem)
        {
            int returnInt = 0;

            foreach (var numeralSetItem in numeralSets)
            {
                if (numeralSetItem.Value.Equals(NumeralItem))
                {
                    returnInt = (int) numeralSetItem.Key;
                    break;
                }
            }

            return returnInt;
        }

        int ComputeSubtractiveNotationTally_CalculateHelper(List<int> inputCharsIntVals)
        {
            int current;
            int next;
            int Size = inputCharsIntVals.Count;
            int returnInt = 0;
            
            for (current = 0; current < Size; current++)
            {
                next = current + 1;

                if (next < Size)
                {
                    if (inputCharsIntVals[current] < inputCharsIntVals[next])
                    {
                        returnInt = returnInt + (-inputCharsIntVals[current]);
                    }
                    else
                    {
                        returnInt = returnInt + inputCharsIntVals[current];
                    }
                }
                else
                {
                    returnInt = returnInt + inputCharsIntVals[current];
                }
            }

            return returnInt;
        }
        
        [Test]
        [TestCase(new char[] {'I'}, ExpectedResult = 1)]
        [TestCase(new char[] {'I', 'I'}, ExpectedResult = 2)]
        [TestCase(new char[] {'I', 'I', 'I'}, ExpectedResult = 3)]
        [TestCase(new char[] {'I', 'V'}, ExpectedResult = 4)]
        [TestCase(new char[] {'V'}, ExpectedResult = 5)]
        [TestCase(new char[] {'V', 'I'}, ExpectedResult = 6)]
        [TestCase(new char[] {'V', 'I', 'I'}, ExpectedResult = 7)]
        [TestCase(new char[] {'V', 'I', 'I', 'I'}, ExpectedResult = 8)]
        [TestCase(new char[] {'I', 'X'}, ExpectedResult = 9)]
        [TestCase(new char[] {'X'}, ExpectedResult = 10)]
        [TestCase(new char[] {'X', 'I'}, ExpectedResult = 11)]
        [TestCase(new char[] {'X', 'I', 'I'}, ExpectedResult = 12)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = 13)]
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = 14)]
        [TestCase(new char[] {'X', 'V'}, ExpectedResult = 15)]
        [TestCase(new char[] {'X', 'V', 'I'}, ExpectedResult = 16)]
        [TestCase(new char[] {'X', 'V', 'I', 'I'}, ExpectedResult = 17)]
        [TestCase(new char[] {'X', 'V', 'I', 'I', 'I'}, ExpectedResult = 18)]
        [TestCase(new char[] {'I', 'X'}, ExpectedResult = 19)]
        [TestCase(new char[] {'X', 'X'}, ExpectedResult = 20)]
        [TestCase(new char[] {'X', 'L'}, ExpectedResult = 40)]
        [TestCase(new char[] {'I', 'L'}, ExpectedResult = 49)]
        [TestCase(new char[] {'L'}, ExpectedResult = 50)]
        [TestCase(new char[] {'L', 'X', 'I'}, ExpectedResult = 61)]
        [TestCase(new char[] {'X', 'C'}, ExpectedResult = 90)]
        [TestCase(new char[] {'V', 'C'}, ExpectedResult = 95)]
        [TestCase(new char[] {'I', 'C'}, ExpectedResult = 99)]
        [TestCase(new char[] {'C'}, ExpectedResult = 100)]
        [TestCase(new char[] {'C', 'D'}, ExpectedResult = 400)]
        [TestCase(new char[] {'L', 'D'}, ExpectedResult = 450)]
        [TestCase(new char[] {'X', 'D'}, ExpectedResult = 490)]
        [TestCase(new char[] {'V', 'D'}, ExpectedResult = 495)]
        [TestCase(new char[] {'I', 'D'}, ExpectedResult = 499)]
        [TestCase(new char[] {'D'}, ExpectedResult = 500)]
        [TestCase(new char[] {'M'}, ExpectedResult = 1000)]
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = 14)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = 13)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'V'}, ExpectedResult = 1914)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'I'}, ExpectedResult = 1912)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'X', 'X'}, ExpectedResult = 1930)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X'}, ExpectedResult = 1980)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X', 'I', 'V'}, ExpectedResult = 1984)]
        [TestCase(new char[] {'M', 'M', 'X', 'I', 'X'}, ExpectedResult = 2019)]
        [TestCase(new char[] {'M', 'M', 'C', 'D', 'X', 'X', 'I', 'V',}, ExpectedResult = 2424)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'X', 'I', 'V'}, ExpectedResult = 1974)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'X', 'I', 'I', 'I'}, ExpectedResult = 1973)]
        [TestCase(new char[] {'M', 'D', 'C', 'L', 'X', 'V', 'I'}, ExpectedResult = 1666)]
        [TestCase(new char[] {'M', 'D', 'C', 'D', 'I', 'I', 'I'}, ExpectedResult = 1903)]
        [TestCase(new char[] {'M', 'C', 'M', 'I', 'I', 'I'}, ExpectedResult = 1903)]
        [TestCase(new char[] {'M', 'C', 'M', 'X'}, ExpectedResult = 1910)]
        [TestCase(new char[] {'M', 'D', 'C', 'C', 'C', 'C', 'X'}, ExpectedResult = 1910)]
        [TestCase(new char[] {'M'}, ExpectedResult = 1000)]
        [TestCase(new char[] {'M', 'X', 'C', 'C', 'X'}, ExpectedResult = 1200)]
        [TestCase(new char[] {'I', 'V', 'X', 'L', 'C', 'D', 'M'}, ExpectedResult = 334)]
        [TestCase(new char[] {'I', 'V', 'P', 'I', 'T', 'E', 'R'}, ExpectedResult = 5)]
        [TestCase(new char[] {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'}, ExpectedResult = 0)]
        [TestCase(new char[] {'a', 'B', 'e', 'F', 'g', 'H', 'j', 'K', 'n', 'O', 'p'}, ExpectedResult = 0)]
        [TestCase(new char[] {'A', 'b', 'E', 'f', 'G', 'h', 'J', 'k', 'N', 'o', 'P'}, ExpectedResult = 0)]
        [TestCase(new char[]
        {
            '!', '@', '#', '$', '%', '^', '&', '*', 
            '(', ')', '-', '_', '=', '+', '/', '\\', 
            '`', '~', '\'', '\"', ',', '.', '/', '<', 
            '>', '?', ':', ';'
        }, ExpectedResult = 0)]
        [TestCase(new char[] {'\0'}, ExpectedResult = 0)]
        public int Test_ComputeSubtractiveNotationTally_TakeInChars_ConvertToInt(char[] inputChars)
        {
            /*Arrange*/
            int returnIntVal = 0;
            int Size = inputChars.Length;
            List<int> storeInts = new List<int>();

            /*Act*/
            for (int i = 0; i < Size; i++)
            {
                storeInts.Add(ComputeSubtractiveNotationTally_ConvertHelper(inputChars[i]));
            }

            returnIntVal = ComputeSubtractiveNotationTally_CalculateHelper(storeInts);

            /*Assert*/
            // Check that ComputeSubtractiveNotationTally return
            // Assert Are Equal value class method & returnIntVal
            
            return returnIntVal;
        }
    }
}


