using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit;
using NUnit.Framework;
using NUnit.Compatibility;
using NUnit.Framework.Constraints;
using RomanNumeralParser.Converter;
using UnityEditor.VersionControl;

namespace RomanNumeralParser.UnitTests
{
    [TestFixture]
    public class UnitTest_RomanNumeralParserClass 
    {
        #region TestingHelpers

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

        private Dictionary<NumeralVals, char> TestingNumeralSets;
        
        private ConvertRomanNumeralsToInts TestingRomanNumeralParser;
        private ConvertRomanNumeralsToInts.Attibutes TestingAttributes;
        private ConvertRomanNumeralsToInts.Accumulators TestingAccumulators;
        
        [SetUp]
        public void Init()
        {
            TestingRomanNumeralParser = new ConvertRomanNumeralsToInts();
            TestingAttributes = new ConvertRomanNumeralsToInts.Attibutes();
            TestingAccumulators = new ConvertRomanNumeralsToInts.Accumulators();

            TestingAttributes = TestingRomanNumeralParser.Init();
            
            TestingNumeralSets = new Dictionary<NumeralVals, char>()
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
        #endregion
        
        [TearDown]
        public void TearDownMethod()
        {
           
        }

        [Test]
        public void Test_AssertTest()
        {
            Assert.Pass();
        }
        
        #region TestMethodHelbers
        int TestHelper_ConvertNumeralCharToInt(char NumeralItem)
        {
            int returnInt = 0;

            foreach (var numeralSetItem in TestingNumeralSets)
            {
                if (numeralSetItem.Value.Equals(NumeralItem))
                {
                    returnInt = (int) numeralSetItem.Key;
                    break;
                }
            }

            return returnInt;
        }
        
        NumeralVals TestHelper_ConvertNumeralCharToNumeralVals(char NumeralItem)
        {
            NumeralVals returnVal = NumeralVals.NONE;

            foreach (var numeralSetItem in TestingNumeralSets)
            {
                if (numeralSetItem.Value.Equals(NumeralItem))
                {
                    returnVal = numeralSetItem.Key;
                    break;
                }
            }

            return returnVal;
        }
        #endregion

        [Test]
        [TestCase('I', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.I)]
        [TestCase('V', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.V)]
        [TestCase('X', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.X)]
        [TestCase('L', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.L)]
        [TestCase('C', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.C)]
        [TestCase('D', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.D)]
        [TestCase('M', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.M)]
        [TestCase('i', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('v', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('x', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('l', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('c', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('d', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('m', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('0', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('1', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('2', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('3', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('4', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('5', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('6', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('7', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('8', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('9', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('å', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('ø', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('!', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('@', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('#', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('$', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('%', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('^', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('&', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('*', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('(', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase(')', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('-', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('_', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('=', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('+', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('~', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('`', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('[', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase(']', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('{', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('}', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('|', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('\\', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase(';', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase(':', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('\'', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('\"', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('\0', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase(',', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('.', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('<', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('>', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('/', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('?', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('\n', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('\t', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        [TestCase('\r', ExpectedResult = ConvertRomanNumeralsToInts.Numerals.NONE)]
        public ConvertRomanNumeralsToInts.Numerals Test_GetNumerals_TakeinAChar_ConvertToEnumValue(char inputChar)
        {
            /*Arrange*/
            ConvertRomanNumeralsToInts.Numerals TestValNumerals = ConvertRomanNumeralsToInts.Numerals.NONE;
            NumeralVals LocalNumerals = NumeralVals.NONE;
            int TestNumeralIntVal = 0;
            int LocalNumeralIntVal = 0;
            bool isGood = false;
            
            /*Act*/
            TestValNumerals = TestingRomanNumeralParser.GetNumerals(inputChar);
            LocalNumerals = TestHelper_ConvertNumeralCharToNumeralVals(inputChar);

            TestNumeralIntVal = (int) TestValNumerals;
            LocalNumeralIntVal = (int) LocalNumerals;

            switch (TestValNumerals)
            {
                case ConvertRomanNumeralsToInts.Numerals.NONE:
                    isGood = LocalNumerals == NumeralVals.NONE;
                    break;
                case ConvertRomanNumeralsToInts.Numerals.I:
                    isGood = LocalNumerals == NumeralVals.I;
                    break;
                case ConvertRomanNumeralsToInts.Numerals.V:
                    isGood = LocalNumerals == NumeralVals.V;
                    break;
                case ConvertRomanNumeralsToInts.Numerals.X:
                    isGood = LocalNumerals == NumeralVals.X;
                    break;
                case ConvertRomanNumeralsToInts.Numerals.L:
                    isGood = LocalNumerals == NumeralVals.L;
                    break;
                case ConvertRomanNumeralsToInts.Numerals.C:
                    isGood = LocalNumerals == NumeralVals.C;
                    break;
                case ConvertRomanNumeralsToInts.Numerals.D:
                    isGood = LocalNumerals == NumeralVals.D;
                    break;
                case ConvertRomanNumeralsToInts.Numerals.M:
                    isGood = LocalNumerals == NumeralVals.M;
                    break;
            }

            /*Assert*/
            
            Assert.IsTrue(isGood);
            Assert.AreEqual(TestNumeralIntVal, LocalNumeralIntVal);
            
            return TestValNumerals;
        }

        
        #region ConvertNumeralsToInts
        
        [Test]
        [TestCase(new char[] {'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'V', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'V', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'V', 'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'V', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'V', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'V', 'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'I', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'L'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'L'}, ExpectedResult = true)]
        [TestCase(new char[] {'L'}, ExpectedResult = true)]
        [TestCase(new char[] {'L', 'X', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'C'}, ExpectedResult = true)]
        [TestCase(new char[] {'V', 'C'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'C'}, ExpectedResult = true)]
        [TestCase(new char[] {'C'}, ExpectedResult = true)]
        [TestCase(new char[] {'C', 'D'}, ExpectedResult = true)]
        [TestCase(new char[] {'L', 'D'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'D'}, ExpectedResult = true)]
        [TestCase(new char[] {'V', 'D'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'D'}, ExpectedResult = true)]
        [TestCase(new char[] {'D'}, ExpectedResult = true)]
        [TestCase(new char[] {'M'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'X', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X', 'I', 'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'M', 'X', 'I', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'M', 'C', 'D', 'X', 'X', 'I', 'V',}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'V'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'D', 'C', 'L', 'X', 'V', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'D', 'C', 'D', 'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'I', 'I', 'I'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'C', 'M', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'D', 'C', 'C', 'C', 'C', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'M'}, ExpectedResult = true)]
        [TestCase(new char[] {'M', 'X', 'C', 'C', 'X'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'V', 'X', 'L', 'C', 'D', 'M'}, ExpectedResult = true)]
        [TestCase(new char[] {'I', 'V', 'P', 'I', 'T', 'E', 'R'}, ExpectedResult = true)]
        [TestCase(new char[] {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'}, ExpectedResult = true)]
        [TestCase(new char[] {'a', 'B', 'e', 'F', 'g', 'H', 'j', 'K', 'n', 'O', 'p'}, ExpectedResult = false)]
        [TestCase(new char[] {'A', 'b', 'E', 'f', 'G', 'h', 'J', 'k', 'N', 'o', 'P'}, ExpectedResult = false)]
        [TestCase(new char[]
        {
            '!', '@', '#', '$', '%', '^', '&', '*',
            '(', ')', '-', '_', '=', '+', '/', '\\',
            '`', '~', '\'', '\"', ',', '.', '/', '<',
            '>', '?', ':', ';'
        }, ExpectedResult = true)]
        [TestCase(new char[] {'\0'}, ExpectedResult = true)]
        public bool Test_ConvertToInts_TakeInArrayOfChars_ConvertToListOfInts(char[] ParamInputChars)
        {
            /*Arrange*/
            bool isTestSumValid = false;
            bool isLocalSumValid = false;
            bool isTestRetInputValid = false;
            bool isAllGood = false;
            int LocalSize = 0;
            int TestSize = 0;
            int LocalSum = 0;
            int TestSum = 0;
            
            char[] TestInputChars;
            char[] LocalInputChars;
            
            string TestStringInputs = ""; 
            string TestRetInput = "";
            string LocalStringInput = "";
            List<int> LocalInts = new List<int>();
            List<int> TestRetInts = new List<int>();

            /*Act*/
            LocalInputChars = ParamInputChars;
            LocalStringInput = new string(LocalInputChars);
            LocalInputChars =  LocalStringInput.ToUpper().Replace(" ", "").ToCharArray();
            LocalSize = LocalInputChars.Length;
            
            for (int i = 0; i < LocalSize; i++)
            {
                LocalInts.Add(TestHelper_ConvertNumeralCharToInt(ParamInputChars[i]));
            }
            
            foreach (var LocalIntsItem in LocalInts)
            {
                LocalSum = LocalSum + LocalIntsItem;
            }
            
            TestInputChars = ParamInputChars;
            TestStringInputs = new string(TestInputChars);
            TestRetInput = TestingRomanNumeralParser.GetCleanedStringToken(TestStringInputs);
            TestSize = TestInputChars.Length;
            
            TestRetInts = TestingRomanNumeralParser.ConvertToInts(TestInputChars);
            
            foreach (var TestRetIntsItem in TestRetInts)
            {
                TestSum = TestSum + TestRetIntsItem;
            }
            
            isTestSumValid = TestSum >= 0;
            isLocalSumValid = LocalSum >= 0;
            isTestRetInputValid = TestRetInput.Equals(TestStringInputs);
            isAllGood = isTestSumValid && isLocalSumValid && isTestRetInputValid;

            /*Assert*/
            Assert.IsNotNull(TestRetInts);
            Assert.IsNotNull(LocalInts);
            Assert.AreEqual(LocalSize, TestSize);
            Assert.AreEqual(TestSum, LocalSum);

            return isAllGood;
        }
        #endregion

        #region GetInputTests
        
        [Test]
        [TestCase("mcmxiv", ExpectedResult = true)]
        [TestCase(" mcm xiv ", ExpectedResult = true)]
        [TestCase(" m c m x i v ", ExpectedResult = true)]
        [TestCase("    m c    m    x i    v ", ExpectedResult = true)]
        [TestCase("    m    c    m    x    i    v ", ExpectedResult = true)]
        public bool Test_GetInput_InputRomanNumerals_RemoveSpaceSetToCaps(string inputs)
        {
            /*Arrange*/
            // Class Is RomanNumeralParser
            // Method Is GetInput
            string TestRetInput = TestingRomanNumeralParser.GetCleanedStringToken(inputs);
            
            /*Act*/
            // Pass inputs to GetInput return result
            // compare result to retInput
            string retInput = inputs.Replace(" ", "").ToUpper();
            
            /*Assert*/
            // GetInput return result and retInput are equal
            bool compareStrings = TestRetInput.Equals(retInput);
            
            Assert.IsTrue(compareStrings);
            Assert.AreEqual(TestRetInput, retInput);

            return compareStrings;
        }
        #endregion

        #region SubtractiveNotationConversionTest

        int TestHelper_CalculateSubtractiveValue_Calculate(List<int> inputCharsIntVals)
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
        [TestCase(new char[] {'X', 'I', 'X'}, ExpectedResult = 19)]
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
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'V'}, ExpectedResult = 1974)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'I', 'I'}, ExpectedResult = 1973)]
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
        public long Test_CalculateSubtractiveValue_TakeInChars_ConvertToInt(char[] inputChars)
        {
            /*Arrange*/
            long returnIntVal = 0;
            int Size = inputChars.Length;
            string TestLocalInputString = "";
            List<int> storeInts = new List<int>();
            List<int> TestStoreInts = new List<int>();
            
            /*Act*/
            TestLocalInputString = new string(inputChars);
            TestStoreInts = TestingRomanNumeralParser.DiassembleStringToIntTokens(TestLocalInputString);
            
            for (int i = 0; i < Size; i++)
            {
                storeInts.Add(TestHelper_ConvertNumeralCharToInt(inputChars[i]));
            }

            returnIntVal = TestHelper_CalculateSubtractiveValue_Calculate(storeInts);

            TestingAccumulators = TestingRomanNumeralParser.CalculateSubtractiveValue(TestingAccumulators, TestStoreInts);

            bool isGood = TestingAccumulators.subtractive >= 0;
            
            /*Assert*/
            // Check CalculateSubtractiveValue return correct value
            // Assert Are Equal value class method value & test method returnIntVal
            
            Assert.IsTrue(isGood);
            Assert.AreEqual(TestingAccumulators.subtractive, returnIntVal);
            
            return TestingAccumulators.subtractive;
        }
        #endregion
        
    }
}


