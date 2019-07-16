using System;
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
        private ConvertRomanNumeralsToInts.Attributes TestingAttributes;
        private ConvertRomanNumeralsToInts.Accumulators TestingAccumulators;
        
        [SetUp]
        public void Init()
        {
            TestingRomanNumeralParser = new ConvertRomanNumeralsToInts();
            TestingAttributes = new ConvertRomanNumeralsToInts.Attributes();
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
        
        #region TestMethodHelpers
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

        int TestHelper_CalculateAdditiveValue_Calculate(List<int> inputCharsIntVals)
        {
            int current;

            int Size = inputCharsIntVals.Count;
            int returnInt = 0;
            
            for (current = 0; current < Size; current++)
            {
                returnInt = returnInt + inputCharsIntVals[current];
            }

            return returnInt;
        }

        int TestHelper_CalculateIrregularValue_Calculate(List<int> inputCharsIntVals)
        {
            int retAccum = 0;
            List<int> sortedSet = new List<int>();
            
            foreach (var sourceSetItem in inputCharsIntVals)
            {
                sortedSet.Add(sourceSetItem);
            }

            sortedSet.Sort();

            foreach (var sIntItem in sortedSet)
            {
                retAccum -= sIntItem;
            }

            retAccum += sortedSet[sortedSet.Count - 1];
            retAccum += sortedSet[sortedSet.Count - 1];

            return retAccum;
        }
        #endregion

        #region TestInit

        [Test]
        public void Test_Init_TakesNoPrameters_ReturnsInitialisedAttributes()
        {
            /*Arrange*/
            ConvertRomanNumeralsToInts.Attributes TestAttributes = new ConvertRomanNumeralsToInts.Attributes();
            List<int> LocalIntList = new List<int>();
            Dictionary<ConvertRomanNumeralsToInts.Numerals, char> LocalDicSet =
                new Dictionary<ConvertRomanNumeralsToInts.Numerals, char>();
            
            /*Act*/
            TestAttributes = TestingRomanNumeralParser.Init();
            LocalIntList = TestAttributes.numbersOutput;
            LocalDicSet = TestAttributes.numeralPairs;
            
            /*Assert*/            
            Assert.IsNotNull(TestingRomanNumeralParser.Init());
            Assert.That(TestingRomanNumeralParser.Init(), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Attributes)));
            Assert.That(TestingRomanNumeralParser.Init().input, Is.TypeOf(typeof(string)));
            Assert.That(TestingRomanNumeralParser.Init().done, Is.TypeOf(typeof(bool)));
            Assert.That(TestingRomanNumeralParser.Init().accumulators.subtractive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.Init().accumulators.additive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.Init().accumulators.irregular, Is.TypeOf(typeof(long)));
            CollectionAssert.AreEquivalent(TestingRomanNumeralParser.Init().numbersOutput, LocalIntList);
            CollectionAssert.AreEquivalent(TestingRomanNumeralParser.Init().numeralPairs, LocalDicSet);
            CollectionAssert.AllItemsAreInstancesOfType(TestingRomanNumeralParser.Init().numbersOutput, typeof(int));
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.I), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.V), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.X), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.L), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.C), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.D), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.M), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsKey(ConvertRomanNumeralsToInts.Numerals.NONE), Is.False);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsValue('I'), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsValue('V'), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsValue('X'), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsValue('L'), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsValue('C'), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsValue('D'), Is.True);
            Assert.That(TestingRomanNumeralParser.Init().numeralPairs.ContainsValue('M'), Is.True);
        }

        #endregion
        
        #region TestGetNumerals
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
        #endregion
        
        #region TestGetIntValue

        [Test]
        [TestCase('I', ExpectedResult = 1)]
        [TestCase('V', ExpectedResult = 5)]
        [TestCase('X', ExpectedResult = 10)]
        [TestCase('L', ExpectedResult = 50)]
        [TestCase('C', ExpectedResult = 100)]
        [TestCase('D', ExpectedResult = 500)]
        [TestCase('M', ExpectedResult = 1000)]
        [TestCase('i', ExpectedResult = 0)]
        [TestCase('v', ExpectedResult = 0)]
        [TestCase('x', ExpectedResult = 0)]
        [TestCase('l', ExpectedResult = 0)]
        [TestCase('c', ExpectedResult = 0)]
        [TestCase('d', ExpectedResult = 0)]
        [TestCase('m', ExpectedResult = 0)]
        [TestCase('0', ExpectedResult = 0)]
        [TestCase('1', ExpectedResult = 0)]
        [TestCase('2', ExpectedResult = 0)]
        [TestCase('3', ExpectedResult = 0)]
        [TestCase('4', ExpectedResult = 0)]
        [TestCase('5', ExpectedResult = 0)]
        [TestCase('6', ExpectedResult = 0)]
        [TestCase('7', ExpectedResult = 0)]
        [TestCase('8', ExpectedResult = 0)]
        [TestCase('9', ExpectedResult = 0)]
        [TestCase('å', ExpectedResult = 0)]
        [TestCase('ø', ExpectedResult = 0)]
        [TestCase('!', ExpectedResult = 0)]
        [TestCase('@', ExpectedResult = 0)]
        [TestCase('#', ExpectedResult = 0)]
        [TestCase('$', ExpectedResult = 0)]
        [TestCase('%', ExpectedResult = 0)]
        [TestCase('^', ExpectedResult = 0)]
        [TestCase('&', ExpectedResult = 0)]
        [TestCase('*', ExpectedResult = 0)]
        [TestCase('(', ExpectedResult = 0)]
        [TestCase(')', ExpectedResult = 0)]
        [TestCase('-', ExpectedResult = 0)]
        [TestCase('_', ExpectedResult = 0)]
        [TestCase('=', ExpectedResult = 0)]
        [TestCase('+', ExpectedResult = 0)]
        [TestCase('~', ExpectedResult = 0)]
        [TestCase('`', ExpectedResult = 0)]
        [TestCase('[', ExpectedResult = 0)]
        [TestCase(']', ExpectedResult = 0)]
        [TestCase('{', ExpectedResult = 0)]
        [TestCase('}', ExpectedResult = 0)]
        [TestCase('|', ExpectedResult = 0)]
        [TestCase('\\', ExpectedResult = 0)]
        [TestCase(';', ExpectedResult = 0)]
        [TestCase(':', ExpectedResult = 0)]
        [TestCase('\'', ExpectedResult = 0)]
        [TestCase('\"', ExpectedResult = 0)]
        [TestCase('\0', ExpectedResult = 0)]
        [TestCase(',', ExpectedResult = 0)]
        [TestCase('.', ExpectedResult = 0)]
        [TestCase('<', ExpectedResult = 0)]
        [TestCase('>', ExpectedResult = 0)]
        [TestCase('/', ExpectedResult = 0)]
        [TestCase('?', ExpectedResult = 0)]
        [TestCase('\n', ExpectedResult = 0)]
        [TestCase('\t', ExpectedResult = 0)]
        [TestCase('\r', ExpectedResult = 0)]
        public int Test_GetIntValue_TakeInAChar_ReturnAnInt(char inputChar)
        {
            /*Arrange*/
            int TestInt = 0;
            int LocalInt = 0;
            
            /*Act*/
            TestInt = TestingRomanNumeralParser.GetIntValue(inputChar);
            LocalInt = TestHelper_ConvertNumeralCharToInt(inputChar);

            /*Assert*/
            Assert.That(TestingRomanNumeralParser.GetIntValue(inputChar), Is.TypeOf(typeof(int)));
            Assert.AreEqual(TestInt, LocalInt);
            
            return TestInt;
        }

        #endregion
        
        #region TestConvertToInts
        
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

        #region TestGetCleanedStringToken
        
        [Test]
        [TestCase("mcmxiv", ExpectedResult = true)]
        [TestCase(" mcm xiv ", ExpectedResult = true)]
        [TestCase(" m c m x i v ", ExpectedResult = true)]
        [TestCase("    m c    m    x i    v ", ExpectedResult = true)]
        [TestCase("    m    c    m    x    i    v ", ExpectedResult = true)]
        public bool Test_GetCleanedStringToken_InputRomanNumerals_RemoveSpaceSetToCaps(string inputs)
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

        #region TestCalculateSubtractiveValue

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
        [TestCase(new char[]
        {
            'M', 'M', 'M', 'M', 'M', 
            'M', 'M', 'M', 'M', 'M',
            'C', 'X', 'C', 'I'
        }, ExpectedResult = 10191)]
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

        #region TestCalculateAdditiveValue

        [Test]
        [TestCase(new char[] {'I'}, ExpectedResult = 1)]
        [TestCase(new char[] {'I', 'I'}, ExpectedResult = 2)]
        [TestCase(new char[] {'I', 'I', 'I'}, ExpectedResult = 3)]
        [TestCase(new char[] {'I', 'V'}, ExpectedResult = 6)]
        [TestCase(new char[] {'V'}, ExpectedResult = 5)]
        [TestCase(new char[] {'V', 'I'}, ExpectedResult = 6)]
        [TestCase(new char[] {'V', 'I', 'I'}, ExpectedResult = 7)]
        [TestCase(new char[] {'V', 'I', 'I', 'I'}, ExpectedResult = 8)]
        [TestCase(new char[] {'I', 'X'}, ExpectedResult = 11)]
        [TestCase(new char[] {'X'}, ExpectedResult = 10)]
        [TestCase(new char[] {'X', 'I'}, ExpectedResult = 11)]
        [TestCase(new char[] {'X', 'I', 'I'}, ExpectedResult = 12)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = 13)]
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = 16)]
        [TestCase(new char[] {'X', 'V'}, ExpectedResult = 15)]
        [TestCase(new char[] {'X', 'V', 'I'}, ExpectedResult = 16)]
        [TestCase(new char[] {'X', 'V', 'I', 'I'}, ExpectedResult = 17)]
        [TestCase(new char[] {'X', 'V', 'I', 'I', 'I'}, ExpectedResult = 18)]
        [TestCase(new char[] {'X', 'I', 'X'}, ExpectedResult = 21)]
        [TestCase(new char[] {'X', 'X'}, ExpectedResult = 20)]
        [TestCase(new char[] {'X', 'L'}, ExpectedResult = 60)]
        [TestCase(new char[] {'I', 'L'}, ExpectedResult = 51)]
        [TestCase(new char[] {'L'}, ExpectedResult = 50)]
        [TestCase(new char[] {'L', 'X', 'I'}, ExpectedResult = 61)]
        [TestCase(new char[] {'X', 'C'}, ExpectedResult = 110)]
        [TestCase(new char[] {'V', 'C'}, ExpectedResult = 105)]
        [TestCase(new char[] {'I', 'C'}, ExpectedResult = 101)]
        [TestCase(new char[] {'C'}, ExpectedResult = 100)]
        [TestCase(new char[] {'C', 'D'}, ExpectedResult = 600)]
        [TestCase(new char[] {'L', 'D'}, ExpectedResult = 550)]
        [TestCase(new char[] {'X', 'D'}, ExpectedResult = 510)]
        [TestCase(new char[] {'V', 'D'}, ExpectedResult = 505)]
        [TestCase(new char[] {'I', 'D'}, ExpectedResult = 501)]
        [TestCase(new char[] {'D'}, ExpectedResult = 500)]
        [TestCase(new char[] {'M'}, ExpectedResult = 1000)]
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = 16)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = 13)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'V'}, ExpectedResult = 2116)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'I'}, ExpectedResult = 2112)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'X', 'X'}, ExpectedResult = 2130)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X'}, ExpectedResult = 2180)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X', 'I', 'V'}, ExpectedResult = 2186)]
        [TestCase(new char[] {'M', 'M', 'X', 'I', 'X'}, ExpectedResult = 2021)]
        [TestCase(new char[] {'M', 'M', 'C', 'D', 'X', 'X', 'I', 'V',}, ExpectedResult = 2626)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'V'}, ExpectedResult = 2176)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'I', 'I'}, ExpectedResult = 2173)]
        [TestCase(new char[] {'M', 'D', 'C', 'L', 'X', 'V', 'I'}, ExpectedResult = 1666)]
        [TestCase(new char[] {'M', 'D', 'C', 'D', 'I', 'I', 'I'}, ExpectedResult = 2103)]
        [TestCase(new char[] {'M', 'C', 'M', 'I', 'I', 'I'}, ExpectedResult = 2103)]
        [TestCase(new char[] {'M', 'C', 'M', 'X'}, ExpectedResult = 2110)]
        [TestCase(new char[] {'M', 'D', 'C', 'C', 'C', 'C', 'X'}, ExpectedResult = 1910)]
        [TestCase(new char[] {'M'}, ExpectedResult = 1000)]
        [TestCase(new char[] {'M', 'X', 'C', 'C', 'X'}, ExpectedResult = 1220)]
        [TestCase(new char[] {'I', 'V', 'X', 'L', 'C', 'D', 'M'}, ExpectedResult = 1666)]
        [TestCase(new char[] {'I', 'V', 'P', 'I', 'T', 'E', 'R'}, ExpectedResult = 7)]
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
        public long Test_CalculateAdditiveValue_TakeInChars_ConvertToInt(char[] inputChars)
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

            returnIntVal = TestHelper_CalculateAdditiveValue_Calculate(storeInts);

            TestingAccumulators = TestingRomanNumeralParser.CalculateAdditiveValue(TestingAccumulators, TestStoreInts);

            bool isGood = TestingAccumulators.additive >= 0;
            
            /*Assert*/
            // Check CalculateSubtractiveValue return correct value
            // Assert Are Equal value class method value & test method returnIntVal
            
            Assert.IsTrue(isGood);
            Assert.AreEqual(TestingAccumulators.additive, returnIntVal);
            
            return TestingAccumulators.additive;
        }

        #endregion

        #region TestCalculateIrregularValue

        [Test]
        [TestCase(new char[] {'I'}, ExpectedResult = 1)]
        [TestCase(new char[] {'I', 'I'}, ExpectedResult = 0)]
        [TestCase(new char[] {'I', 'I', 'I'}, ExpectedResult = -1)]
        [TestCase(new char[] {'I', 'V'}, ExpectedResult = 4)]
        [TestCase(new char[] {'V'}, ExpectedResult = 5)]
        [TestCase(new char[] {'V', 'I'}, ExpectedResult = 4)]
        [TestCase(new char[] {'V', 'I', 'I'}, ExpectedResult = 3)]
        [TestCase(new char[] {'V', 'I', 'I', 'I'}, ExpectedResult = 2)]
        [TestCase(new char[] {'I', 'X'}, ExpectedResult = 9)]
        [TestCase(new char[] {'X'}, ExpectedResult = 10)]
        [TestCase(new char[] {'X', 'I'}, ExpectedResult = 9)]
        [TestCase(new char[] {'X', 'I', 'I'}, ExpectedResult = 8)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = 7)]
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = 4)]
        [TestCase(new char[] {'X', 'V'}, ExpectedResult = 5)]
        [TestCase(new char[] {'X', 'V', 'I'}, ExpectedResult = 4)]
        [TestCase(new char[] {'X', 'V', 'I', 'I'}, ExpectedResult = 3)]
        [TestCase(new char[] {'X', 'V', 'I', 'I', 'I'}, ExpectedResult = 2)]
        [TestCase(new char[] {'X', 'I', 'X'}, ExpectedResult = -1)]
        [TestCase(new char[] {'X', 'X'}, ExpectedResult = 0)]
        [TestCase(new char[] {'X', 'L'}, ExpectedResult = 40)]
        [TestCase(new char[] {'I', 'L'}, ExpectedResult = 49)]
        [TestCase(new char[] {'L'}, ExpectedResult = 50)]
        [TestCase(new char[] {'L', 'X', 'I'}, ExpectedResult = 39)]
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
        [TestCase(new char[] {'X', 'I', 'V'}, ExpectedResult = 4)]
        [TestCase(new char[] {'X', 'I', 'I', 'I'}, ExpectedResult = 7)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'V'}, ExpectedResult = -116)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'I', 'I'}, ExpectedResult = -112)]
        [TestCase(new char[] {'M', 'C', 'M', 'X', 'X', 'X'}, ExpectedResult = -130)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X'}, ExpectedResult = -180)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'X', 'I', 'V'}, ExpectedResult = -186)]
        [TestCase(new char[] {'M', 'M', 'X', 'I', 'X'}, ExpectedResult = -21)]
        [TestCase(new char[] {'M', 'M', 'C', 'D', 'X', 'X', 'I', 'V',}, ExpectedResult = -626)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'V'}, ExpectedResult = -176)]
        [TestCase(new char[] {'M', 'C', 'M', 'L', 'X', 'X', 'I', 'I', 'I'}, ExpectedResult = -173)]
        [TestCase(new char[] {'M', 'D', 'C', 'L', 'X', 'V', 'I'}, ExpectedResult = 334)]
        [TestCase(new char[] {'M', 'D', 'C', 'D', 'I', 'I', 'I'}, ExpectedResult = -103)]
        [TestCase(new char[] {'M', 'C', 'M', 'I', 'I', 'I'}, ExpectedResult = -103)]
        [TestCase(new char[] {'M', 'C', 'M', 'X'}, ExpectedResult = -110)]
        [TestCase(new char[] {'M', 'D', 'C', 'C', 'C', 'C', 'X'}, ExpectedResult = 90)]
        [TestCase(new char[] {'M'}, ExpectedResult = 1000)]
        [TestCase(new char[] {'M', 'X', 'C', 'C', 'X'}, ExpectedResult = 780)]
        [TestCase(new char[] {'I', 'V', 'X', 'L', 'C', 'D', 'M'}, ExpectedResult = 334)]
        [TestCase(new char[] {'I', 'V', 'P', 'I', 'T', 'E', 'R'}, ExpectedResult = 3)]
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
        public long Test_CalculateIrregularValue_TakeInChars_ConvertToInt(char[] inputChars)
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

            returnIntVal = TestHelper_CalculateIrregularValue_Calculate(storeInts);

            TestingAccumulators = TestingRomanNumeralParser.CalculateIrregularValue(TestingAccumulators, TestStoreInts);

            /*Assert*/
            // Check CalculateSubtractiveValue return correct value
            // Assert Are Equal value class method value & test method returnIntVal
            
            Assert.AreEqual(TestingAccumulators.irregular, returnIntVal);
            
            return TestingAccumulators.irregular;
        }

        #endregion

        #region TestSendInputsToAccumulator

        [Test]
        [TestCase("m", ExpectedResult = true)]
        [TestCase("d", ExpectedResult = true)]
        [TestCase("c", ExpectedResult = true)]
        [TestCase("l", ExpectedResult = true)]
        [TestCase("x", ExpectedResult = true)]
        [TestCase("v", ExpectedResult = true)]
        [TestCase("i", ExpectedResult = true)]
        [TestCase("ii", ExpectedResult = true)]
        [TestCase("iii", ExpectedResult = true)]
        [TestCase("iiii", ExpectedResult = true)]
        [TestCase("iiiii", ExpectedResult = true)]
        [TestCase("iiiiii", ExpectedResult = true)]
        [TestCase("iv", ExpectedResult = true)]
        [TestCase("iiv", ExpectedResult = true)]
        [TestCase("iiiv", ExpectedResult = true)]
        [TestCase("vi", ExpectedResult = true)]
        [TestCase("vii", ExpectedResult = true)]
        [TestCase("viii", ExpectedResult = true)]
        [TestCase("ix", ExpectedResult = true)]
        [TestCase("iix", ExpectedResult = true)]
        [TestCase("iiix", ExpectedResult = true)]
        [TestCase("xi", ExpectedResult = true)]
        [TestCase("xii", ExpectedResult = true)]
        [TestCase("xiii", ExpectedResult = true)]
        [TestCase("vx", ExpectedResult = true)]
        [TestCase("vix", ExpectedResult = true)]
        [TestCase("vvx", ExpectedResult = true)]
        [TestCase("vvix", ExpectedResult = true)]
        [TestCase("vvvx", ExpectedResult = true)]
        [TestCase("xv", ExpectedResult = true)]
        [TestCase("xiv", ExpectedResult = true)]
        [TestCase("xvv", ExpectedResult = true)]
        [TestCase("xvi", ExpectedResult = true)]
        [TestCase("xivv", ExpectedResult = true)]
        [TestCase("xvvv", ExpectedResult = true)]
        [TestCase("xXx", ExpectedResult = true)]
        [TestCase("xvx", ExpectedResult = true)]
        [TestCase("XvX", ExpectedResult = true)]
        [TestCase("XxVvVxX", ExpectedResult = true)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = true)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdmmxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = true)]
        [TestCase("MCMLXXXIV", ExpectedResult = true)]
        [TestCase("mcmlxxxiv", ExpectedResult = true)]
        [TestCase("MDLXXVI", ExpectedResult = true)]
        [TestCase("MMDLXXVI", ExpectedResult = true)]
        [TestCase("MxMxDLXXVI", ExpectedResult = true)]
        [TestCase("MCMLXXIII", ExpectedResult = true)]
        [TestCase("mcmlxxiii", ExpectedResult = true)]
        [TestCase("mmmmmmmmmmcxci", ExpectedResult = true)]
        [TestCase("MMMMMMMMMMCXCI", ExpectedResult = true)]
        [TestCase("IVPITER", ExpectedResult = true)]
        [TestCase("supercalifragilisticexpialidocious", ExpectedResult = true)]
        [TestCase(@"1234567890`-=[]\;',./~!@#$%^&*()_+{}|:><?àáâäæãåāaèéêëēėęîïíīįìôöòóœøōõûüùúūÿçćčñńłßśš", ExpectedResult = true)]
        [TestCase("\"\'\0\r\n\t", ExpectedResult = true)]
        public bool Test_SendInputsToAccumulator_TakeInString_ReturnPopulatedAttribute(string inputs)
        {
            /*Arrange*/
            bool isGood = false;
            char[] LocalCharInputs = inputs.Replace(" ","").ToUpper().ToCharArray();
            int Size = LocalCharInputs.Length;
            ConvertRomanNumeralsToInts.Attributes LocalAttributes = new ConvertRomanNumeralsToInts.Attributes();
            
            LocalAttributes.numbersOutput = new List<int>();
            LocalAttributes.numeralPairs = new Dictionary<ConvertRomanNumeralsToInts.Numerals, char>();
            
            /*Act*/
            TestingAttributes = TestingRomanNumeralParser.SendInputsToAccumulator(inputs);

            for (int i = 0; i < Size; i++)
            {
                LocalAttributes.numbersOutput.Add(TestHelper_ConvertNumeralCharToInt(LocalCharInputs[i]));
            }
             
            LocalAttributes.accumulators.subtractive = TestHelper_CalculateSubtractiveValue_Calculate(LocalAttributes.numbersOutput);
            LocalAttributes.accumulators.additive = TestHelper_CalculateAdditiveValue_Calculate(LocalAttributes.numbersOutput);
            LocalAttributes.accumulators.irregular = TestHelper_CalculateIrregularValue_Calculate(LocalAttributes.numbersOutput);
            
            /*Assert*/
            Assert.AreEqual(TestingAttributes.accumulators.subtractive, LocalAttributes.accumulators.subtractive);
            Assert.AreEqual(TestingAttributes.accumulators.additive, LocalAttributes.accumulators.additive);
            Assert.AreEqual(TestingAttributes.accumulators.irregular, LocalAttributes.accumulators.irregular);
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Attributes)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators, Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.subtractive, Is.TypeOf(typeof(long)));            
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.additive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.irregular, Is.TypeOf(typeof(long)));

            isGood = 
                TestingAttributes.accumulators.subtractive == LocalAttributes.accumulators.subtractive &&
                TestingAttributes.accumulators.additive == LocalAttributes.accumulators.additive &&
                TestingAttributes.accumulators.irregular == LocalAttributes.accumulators.irregular;
            
            return isGood;
        }

        [Test]
        [TestCase("m", ExpectedResult = 1000)]
        [TestCase("d", ExpectedResult = 500)]
        [TestCase("c", ExpectedResult = 100)]
        [TestCase("l", ExpectedResult = 50)]
        [TestCase("x", ExpectedResult = 10)]
        [TestCase("v", ExpectedResult = 5)]
        [TestCase("i", ExpectedResult = 1)]
        [TestCase("ii", ExpectedResult = 2)]
        [TestCase("iii", ExpectedResult = 3)]
        [TestCase("iiii", ExpectedResult = 4)]
        [TestCase("iiiii", ExpectedResult = 5)]
        [TestCase("iiiiii", ExpectedResult = 6)]
        [TestCase("iv", ExpectedResult = 4)]
        [TestCase("iiv", ExpectedResult = 5)]
        [TestCase("iiiv", ExpectedResult = 6)]
        [TestCase("vi", ExpectedResult = 6)]
        [TestCase("vii", ExpectedResult = 7)]
        [TestCase("viii", ExpectedResult = 8)]
        [TestCase("ix", ExpectedResult = 9)]
        [TestCase("iix", ExpectedResult = 10)]
        [TestCase("iiix", ExpectedResult = 11)]
        [TestCase("xi", ExpectedResult = 11)]
        [TestCase("xii", ExpectedResult = 12)]
        [TestCase("xiii", ExpectedResult = 13)]
        [TestCase("vx", ExpectedResult = 5)]
        [TestCase("vix", ExpectedResult = 14)]
        [TestCase("vvx", ExpectedResult = 10)]
        [TestCase("vvix", ExpectedResult = 19)]
        [TestCase("vvvx", ExpectedResult = 15)]
        [TestCase("xv", ExpectedResult = 15)]
        [TestCase("xiv", ExpectedResult = 14)]
        [TestCase("xvv", ExpectedResult = 20)]
        [TestCase("xvi", ExpectedResult = 16)]
        [TestCase("xivv", ExpectedResult = 19)]
        [TestCase("xvvv", ExpectedResult = 25)]
        [TestCase("xXx", ExpectedResult = 30)]
        [TestCase("xvx", ExpectedResult = 15)]
        [TestCase("XvX", ExpectedResult = 15)]
        [TestCase("XxVvVxX", ExpectedResult = 45)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = 10024)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdmmxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = 20048)]
        [TestCase("MCMLXXXIV", ExpectedResult = 1984)]
        [TestCase("mcmlxxxiv", ExpectedResult = 1984)]
        [TestCase("MDLXXVI", ExpectedResult = 1576)]
        [TestCase("MMDLXXVI", ExpectedResult = 2576)]
        [TestCase("MxMxDLXXVI", ExpectedResult = 2556)]
        [TestCase("MCMLXXIII", ExpectedResult = 1973)]
        [TestCase("mcmlxxiii", ExpectedResult = 1973)]
        [TestCase("mmmmmmmmmmcxci", ExpectedResult = 10191)]
        [TestCase("MMMMMMMMMMCXCI", ExpectedResult = 10191)]
        [TestCase("IVPITER", ExpectedResult = 5)]
        [TestCase("supercalifragilisticexpialidocious", ExpectedResult = 961)]
        [TestCase(@"1234567890`-=[]\;',./~!@#$%^&*()_+{}|:><?àáâäæãåāaèéêëēėęîïíīįìôöòóœøōõûüùúūÿçćčñńłßśš", ExpectedResult = 0)]
        [TestCase("\"\'\0\r\n\t", ExpectedResult = 0)]
        public long Test_SendInputsToAccumulator_TakeInString_ReturnSubtractiveNotationIntValue(string inputs)
        {
            /*Arrange*/
            char[] LocalCharInputs = inputs.Replace(" ","").ToUpper().ToCharArray();
            int Size = LocalCharInputs.Length;
            ConvertRomanNumeralsToInts.Attributes LocalAttributes = new ConvertRomanNumeralsToInts.Attributes();
            
            LocalAttributes.numbersOutput = new List<int>();
            LocalAttributes.numeralPairs = new Dictionary<ConvertRomanNumeralsToInts.Numerals, char>();
            
            /*Act*/
            TestingAttributes = TestingRomanNumeralParser.SendInputsToAccumulator(inputs);

            for (int i = 0; i < Size; i++)
            {
                LocalAttributes.numbersOutput.Add(TestHelper_ConvertNumeralCharToInt(LocalCharInputs[i]));
            }
             
            LocalAttributes.accumulators.subtractive = TestHelper_CalculateSubtractiveValue_Calculate(LocalAttributes.numbersOutput);
            
            /*Assert*/
            Assert.AreEqual(TestingAttributes.accumulators.subtractive, LocalAttributes.accumulators.subtractive);
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Attributes)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators, Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.subtractive, Is.TypeOf(typeof(long)));
            
            return TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.subtractive;
        }
        
        [Test]
        [TestCase("m", ExpectedResult = 1000)]
        [TestCase("d", ExpectedResult = 500)]
        [TestCase("c", ExpectedResult = 100)]
        [TestCase("l", ExpectedResult = 50)]
        [TestCase("x", ExpectedResult = 10)]
        [TestCase("v", ExpectedResult = 5)]
        [TestCase("i", ExpectedResult = 1)]
        [TestCase("ii", ExpectedResult = 2)]
        [TestCase("iii", ExpectedResult = 3)]
        [TestCase("iiii", ExpectedResult = 4)]
        [TestCase("iiiii", ExpectedResult = 5)]
        [TestCase("iiiiii", ExpectedResult = 6)]
        [TestCase("iv", ExpectedResult = 6)]
        [TestCase("iiv", ExpectedResult = 7)]
        [TestCase("iiiv", ExpectedResult = 8)]
        [TestCase("vi", ExpectedResult = 6)]
        [TestCase("vii", ExpectedResult = 7)]
        [TestCase("viii", ExpectedResult = 8)]
        [TestCase("ix", ExpectedResult = 11)]
        [TestCase("iix", ExpectedResult = 12)]
        [TestCase("iiix", ExpectedResult = 13)]
        [TestCase("xi", ExpectedResult = 11)]
        [TestCase("xii", ExpectedResult = 12)]
        [TestCase("xiii", ExpectedResult = 13)]
        [TestCase("vx", ExpectedResult = 15)]
        [TestCase("vix", ExpectedResult = 16)]
        [TestCase("vvx", ExpectedResult = 20)]
        [TestCase("vvix", ExpectedResult = 21)]
        [TestCase("vvvx", ExpectedResult = 25)]
        [TestCase("xv", ExpectedResult = 15)]
        [TestCase("xiv", ExpectedResult = 16)]
        [TestCase("xvv", ExpectedResult = 20)]
        [TestCase("xvi", ExpectedResult = 16)]
        [TestCase("xivv", ExpectedResult = 21)]
        [TestCase("xvvv", ExpectedResult = 25)]
        [TestCase("xXx", ExpectedResult = 30)]
        [TestCase("xvx", ExpectedResult = 25)]
        [TestCase("XvX", ExpectedResult = 25)]
        [TestCase("XxVvVxX", ExpectedResult = 55)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = 14308)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdmmxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = 28616)]
        [TestCase("MCMLXXXIV", ExpectedResult = 2186)]
        [TestCase("mcmlxxxiv", ExpectedResult = 2186)]
        [TestCase("MDLXXVI", ExpectedResult = 1576)]
        [TestCase("MMDLXXVI", ExpectedResult = 2576)]
        [TestCase("MxMxDLXXVI", ExpectedResult = 2596)]
        [TestCase("MCMLXXIII", ExpectedResult = 2173)]
        [TestCase("mcmlxxiii", ExpectedResult = 2173)]
        [TestCase("mmmmmmmmmmcxci", ExpectedResult = 10211)]
        [TestCase("MMMMMMMMMMCXCI", ExpectedResult = 10211)]
        [TestCase("IVPITER", ExpectedResult = 7)]
        [TestCase("supercalifragilisticexpialidocious", ExpectedResult = 967)]
        [TestCase(@"1234567890`-=[]\;',./~!@#$%^&*()_+{}|:><?àáâäæãåāaèéêëēėęîïíīįìôöòóœøōõûüùúūÿçćčñńłßśš", ExpectedResult = 0)]
        [TestCase("\"\'\0\r\n\t", ExpectedResult = 0)]
        public long Test_SendInputsToAccumulator_TakeInString_ReturnAdditiveNotationIntValue(string inputs)
        {
            /*Arrange*/
            char[] LocalCharInputs = inputs.Replace(" ","").ToUpper().ToCharArray();
            int Size = LocalCharInputs.Length;
            ConvertRomanNumeralsToInts.Attributes LocalAttributes = new ConvertRomanNumeralsToInts.Attributes();
            
            LocalAttributes.numbersOutput = new List<int>();
            LocalAttributes.numeralPairs = new Dictionary<ConvertRomanNumeralsToInts.Numerals, char>();
            
            /*Act*/
            TestingAttributes = TestingRomanNumeralParser.SendInputsToAccumulator(inputs);

            for (int i = 0; i < Size; i++)
            {
                LocalAttributes.numbersOutput.Add(TestHelper_ConvertNumeralCharToInt(LocalCharInputs[i]));
            }
             
            LocalAttributes.accumulators.additive = TestHelper_CalculateAdditiveValue_Calculate(LocalAttributes.numbersOutput);
            
            /*Assert*/
            Assert.AreEqual(TestingAttributes.accumulators.additive, LocalAttributes.accumulators.additive);
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Attributes)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators, Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.additive, Is.TypeOf(typeof(long)));
            
            return TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.additive;
        }
        
        [Test]
        [TestCase("m", ExpectedResult = 1000)]
        [TestCase("d", ExpectedResult = 500)]
        [TestCase("c", ExpectedResult = 100)]
        [TestCase("l", ExpectedResult = 50)]
        [TestCase("x", ExpectedResult = 10)]
        [TestCase("v", ExpectedResult = 5)]
        [TestCase("i", ExpectedResult = 1)]
        [TestCase("ii", ExpectedResult = 0)]
        [TestCase("iii", ExpectedResult = -1)]
        [TestCase("iiii", ExpectedResult = -2)]
        [TestCase("iiiii", ExpectedResult = -3)]
        [TestCase("iiiiii", ExpectedResult = -4)]
        [TestCase("iv", ExpectedResult = 4)]
        [TestCase("iiv", ExpectedResult = 3)]
        [TestCase("iiiv", ExpectedResult = 2)]
        [TestCase("iiiiv", ExpectedResult = 1)]
        [TestCase("vi", ExpectedResult = 4)]
        [TestCase("vii", ExpectedResult = 3)]
        [TestCase("viii", ExpectedResult = 2)]
        [TestCase("viiii", ExpectedResult = 1)]
        [TestCase("ix", ExpectedResult = 9)]
        [TestCase("iix", ExpectedResult = 8)]
        [TestCase("iiix", ExpectedResult = 7)]
        [TestCase("iiiix", ExpectedResult = 6)]
        [TestCase("iiiiix", ExpectedResult = 5)]
        [TestCase("iiiiiix", ExpectedResult = 4)]
        [TestCase("iiiiiiix", ExpectedResult = 3)]
        [TestCase("iiiiiiiix", ExpectedResult = 2)]
        [TestCase("iiiiiiiiix", ExpectedResult = 1)]
        [TestCase("xi", ExpectedResult = 9)]
        [TestCase("xii", ExpectedResult = 8)]
        [TestCase("xiii", ExpectedResult = 7)]
        [TestCase("xiiii", ExpectedResult = 6)]
        [TestCase("xiiiii", ExpectedResult = 5)]
        [TestCase("xiiiiii", ExpectedResult = 4)]
        [TestCase("xiiiiiii", ExpectedResult = 3)]
        [TestCase("xiiiiiiii", ExpectedResult = 2)]
        [TestCase("xiiiiiiiii", ExpectedResult = 1)]
        [TestCase("vx", ExpectedResult = 5)]
        [TestCase("vvx", ExpectedResult = 0)]
        [TestCase("vvvx", ExpectedResult = -5)]
        [TestCase("vix", ExpectedResult = 4)]
        [TestCase("vvix", ExpectedResult = -1)]
        [TestCase("xv", ExpectedResult = 5)]
        [TestCase("xvv", ExpectedResult = 0)]
        [TestCase("xiv", ExpectedResult = 4)]
        [TestCase("xvi", ExpectedResult = 4)]
        [TestCase("xivv", ExpectedResult = -1)]
        [TestCase("xvvv", ExpectedResult = -5)]
        [TestCase("xXx", ExpectedResult = -10)]
        [TestCase("xvx", ExpectedResult = -5)]
        [TestCase("XvX", ExpectedResult = -5)]
        [TestCase("XxVvVxX", ExpectedResult = -35)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = -12308)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdmmxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = -26616)]
        [TestCase("MCMLXXXIV", ExpectedResult = -186)]
        [TestCase("mcmlxxxiv", ExpectedResult = -186)]
        [TestCase("MDLXXVI", ExpectedResult = 424)]
        [TestCase("MMDLXXVI", ExpectedResult = -576)]
        [TestCase("MxMxDLXXVI", ExpectedResult = -596)]
        [TestCase("MCMLXXIII", ExpectedResult = -173)]
        [TestCase("mcmlxxiii", ExpectedResult = -173)]
        [TestCase("mmmmmmmmmmcxci", ExpectedResult = -8211)]
        [TestCase("MMMMMMMMMMCXCI", ExpectedResult = -8211)]
        [TestCase("IVPITER", ExpectedResult = 3)]
        [TestCase("supercalifragilisticexpialidocious", ExpectedResult = 33)]
        [TestCase(@"1234567890`-=[]\;',./~!@#$%^&*()_+{}|:><?àáâäæãåāaèéêëēėęîïíīįìôöòóœøōõûüùúūÿçćčñńłßśš", ExpectedResult = 0)]
        [TestCase("\"\'\0\r\n\t", ExpectedResult = 0)]
        public long Test_SendInputsToAccumulator_TakeInString_ReturnIrregularSubtractiveNotationIntValue(string inputs)
        {
            /*Arrange*/
            char[] LocalCharInputs = inputs.Replace(" ","").ToUpper().ToCharArray();
            int Size = LocalCharInputs.Length;
            ConvertRomanNumeralsToInts.Attributes LocalAttributes = new ConvertRomanNumeralsToInts.Attributes();
            
            LocalAttributes.numbersOutput = new List<int>();
            LocalAttributes.numeralPairs = new Dictionary<ConvertRomanNumeralsToInts.Numerals, char>();
            
            /*Act*/
            TestingAttributes = TestingRomanNumeralParser.SendInputsToAccumulator(inputs);

            for (int i = 0; i < Size; i++)
            {
                LocalAttributes.numbersOutput.Add(TestHelper_ConvertNumeralCharToInt(LocalCharInputs[i]));
            }
             
            LocalAttributes.accumulators.irregular = TestHelper_CalculateIrregularValue_Calculate(LocalAttributes.numbersOutput);
            
            /*Assert*/
            Assert.AreEqual(TestingAttributes.accumulators.irregular, LocalAttributes.accumulators.irregular);
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Attributes)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators, Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.irregular, Is.TypeOf(typeof(long)));
            
            return TestingRomanNumeralParser.SendInputsToAccumulator(inputs).accumulators.irregular;
        }

        #endregion

        #region TestDiassembleStringToIntTokens

        [Test]
        [TestCase("m", ExpectedResult = true)]
        [TestCase("d", ExpectedResult = true)]
        [TestCase("c", ExpectedResult = true)]
        [TestCase("l", ExpectedResult = true)]
        [TestCase("x", ExpectedResult = true)]
        [TestCase("v", ExpectedResult = true)]
        [TestCase("i", ExpectedResult = true)]
        [TestCase("ii", ExpectedResult = true)]
        [TestCase("iii", ExpectedResult = true)]
        [TestCase("iiii", ExpectedResult = true)]
        [TestCase("iiiii", ExpectedResult = true)]
        [TestCase("iiiiii", ExpectedResult = true)]
        [TestCase("iv", ExpectedResult = true)]
        [TestCase("iiv", ExpectedResult = true)]
        [TestCase("iiiv", ExpectedResult = true)]
        [TestCase("vi", ExpectedResult = true)]
        [TestCase("vii", ExpectedResult = true)]
        [TestCase("viii", ExpectedResult = true)]
        [TestCase("ix", ExpectedResult = true)]
        [TestCase("iix", ExpectedResult = true)]
        [TestCase("iiix", ExpectedResult = true)]
        [TestCase("xi", ExpectedResult = true)]
        [TestCase("xii", ExpectedResult = true)]
        [TestCase("xiii", ExpectedResult = true)]
        [TestCase("vx", ExpectedResult = true)]
        [TestCase("vix", ExpectedResult = true)]
        [TestCase("vvx", ExpectedResult = true)]
        [TestCase("vvix", ExpectedResult = true)]
        [TestCase("vvvx", ExpectedResult = true)]
        [TestCase("xv", ExpectedResult = true)]
        [TestCase("xiv", ExpectedResult = true)]
        [TestCase("xvv", ExpectedResult = true)]
        [TestCase("xvi", ExpectedResult = true)]
        [TestCase("xivv", ExpectedResult = true)]
        [TestCase("xvvv", ExpectedResult = true)]
        [TestCase("xXx", ExpectedResult = true)]
        [TestCase("xvx", ExpectedResult = true)]
        [TestCase("XvX", ExpectedResult = true)]
        [TestCase("XxVvVxX", ExpectedResult = true)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = true)]
        [TestCase("mxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdmmxcdlviicxdvmmivxxdcddcdmmmcdcmdmcdm", ExpectedResult = true)]
        [TestCase("MCMLXXXIV", ExpectedResult = true)]
        [TestCase("mcmlxxxiv", ExpectedResult = true)]
        [TestCase("MDLXXVI", ExpectedResult = true)]
        [TestCase("MMDLXXVI", ExpectedResult = true)]
        [TestCase("MxMxDLXXVI", ExpectedResult = true)]
        [TestCase("MCMLXXIII", ExpectedResult = true)]
        [TestCase("mcmlxxiii", ExpectedResult = true)]
        [TestCase("mmmmmmmmmmcxci", ExpectedResult = true)]
        [TestCase("MMMMMMMMMMCXCI", ExpectedResult = true)]
        [TestCase("IVPITER", ExpectedResult = true)]
        [TestCase("supercalifragilisticexpialidocious", ExpectedResult = true)]
        [TestCase(@"1234567890`-=[]\;',./~!@#$%^&*()_+{}|:><?àáâäæãåāaèéêëēėęîïíīįìôöòóœøōõûüùúūÿçćčñńłßśš", ExpectedResult = true)]
        [TestCase("\"\'\0\r\n\t", ExpectedResult = true)]
        public bool Test_DiassembleStringToIntTokens_TakeInString_ReturnListOfRawIntValues(string inputs)
        {
            /*Arrange*/
            bool isGood = true;
            
            /*Act*/
            foreach (var LocalListIntsItem in TestingRomanNumeralParser.DiassembleStringToIntTokens(inputs))
            {
                isGood = (isGood && (LocalListIntsItem >= 0));
            }
            
            /*Assert*/
            Assert.IsTrue(isGood);
            CollectionAssert.AllItemsAreInstancesOfType(TestingRomanNumeralParser.DiassembleStringToIntTokens(inputs), typeof(int));
            
            return isGood;
        }

        #endregion

        #region TestAssembleOutput

        [Test]
        [TestCase(new int[] {1000, 100, 1000, 50, 10, 1, 1, 1}, ExpectedResult = true)]
        [TestCase(new int[] {0}, ExpectedResult = true)]
        [TestCase(new int[] {1}, ExpectedResult = true)]
        [TestCase(new int[] {1000123456}, ExpectedResult = true)]
        [TestCase(new int[] {int.MaxValue}, ExpectedResult = true)]
        [TestCase(new int[] {int.MaxValue, int.MinValue}, ExpectedResult = true)]
        public bool Test_AssembleOutput_TakesInListOfInts_ReturnsAccumulatorsPostCalculation(int[] integersSet)
        {
            /*Arrange*/
            bool isGood = false;
            int Size = integersSet.Length;
            List<int> LocalListOfInts = new List<int>();

            /*Act*/
            for (int i = 0; i < Size; i++)
            {
                LocalListOfInts.Add(integersSet[i]);
            }

            TestingAccumulators = TestingRomanNumeralParser.AssembleOutput(LocalListOfInts);
            
            /*Assert*/
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).subtractive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).additive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).irregular, Is.TypeOf(typeof(long)));

            isGood =
                TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).GetType() == typeof(ConvertRomanNumeralsToInts.Accumulators) &&
                TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).subtractive.GetType() == typeof(long) &&
                TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).additive.GetType() == typeof(long) &&
                TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).irregular.GetType() == typeof(long);
            
            return isGood;
        }

        [Test]
        [TestCase(new int[] {1000, 50, 500, 1, 5}, ExpectedResult = 1454)]
        [TestCase(new int[] {1000, 10, 500, 1, 5}, ExpectedResult = 1494)]
        [TestCase(new int[] {1000, 1000, 10, 500, 1, 1, 1}, ExpectedResult = 2493)]
        [TestCase(new int[] {1000, 100, 1000, 10, 50, 1, 1, 1}, ExpectedResult = 1943)]
        [TestCase(new int[] {1000, 100, 1000, 50, 10, 1, 1, 1}, ExpectedResult = 1963)]
        [TestCase(new int[] {100, 1000, 50, 10, 1, 1, 1}, ExpectedResult = 963)]
        [TestCase(new int[] {1000, 50, 10, 1, 1, 1}, ExpectedResult = 1063)]
        [TestCase(new int[] {50, 10, 1, 1, 1}, ExpectedResult = 63)]
        [TestCase(new int[] {10, 1, 1, 1}, ExpectedResult = 13)]
        [TestCase(new int[] {10, 1, 1}, ExpectedResult = 12)]
        [TestCase(new int[] {10, 1}, ExpectedResult = 11)]
        [TestCase(new int[] {10}, ExpectedResult = 10)]
        [TestCase(new int[] {10, 5, 1}, ExpectedResult = 16)]
        [TestCase(new int[] {10, 1, 5}, ExpectedResult = 14)]
        [TestCase(new int[] {1, 10}, ExpectedResult = 9)]
        [TestCase(new int[] {0}, ExpectedResult = 0)]
        [TestCase(new int[] {1}, ExpectedResult = 1)]
        [TestCase(new int[] {1000123456}, ExpectedResult = 1000123456)]
        [TestCase(new int[] {int.MaxValue}, ExpectedResult = int.MaxValue)]
        [TestCase(new int[] {int.MinValue}, ExpectedResult = int.MinValue)]
        [TestCase(new int[] {int.MaxValue, int.MinValue}, ExpectedResult = -1)]
        [TestCase(new int[] {int.MinValue, int.MinValue}, ExpectedResult = -4294967296)]
        [TestCase(new int[] {int.MaxValue, int.MaxValue}, ExpectedResult = 4294967294)]
        [TestCase(new int[] {int.MinValue, int.MaxValue}, ExpectedResult = -1)]
        public long Test_AssembleOutput_TakesInListOfInts_ReturnsAccumulatorSubtractiveNotationValue(int[] integersSet)
        {
            /*Arrange*/
            long LocalLong = 0;
            int Size = integersSet.Length;
            List<int> LocalListOfInts = new List<int>();
            
            /*Act*/
            for (int i = 0; i < Size; i++)
            {
                LocalListOfInts.Add(integersSet[i]);
            }

            TestingAccumulators = TestingRomanNumeralParser.AssembleOutput(LocalListOfInts);
            
            /*Assert*/
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).subtractive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).additive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).irregular, Is.TypeOf(typeof(long)));
            
            return TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).subtractive;
        }

        [Test]
        [TestCase(new int[] {1000, 50, 500, 1, 5}, ExpectedResult = 1556)]
        [TestCase(new int[] {1000, 10, 500, 1, 5}, ExpectedResult = 1516)]
        [TestCase(new int[] {1000, 1000, 10, 500, 1, 1, 1}, ExpectedResult = 2513)]
        [TestCase(new int[] {1000, 100, 1000, 10, 50, 1, 1, 1}, ExpectedResult = 2163)]
        [TestCase(new int[] {1000, 100, 1000, 50, 10, 1, 1, 1}, ExpectedResult = 2163)]
        [TestCase(new int[] {100, 1000, 50, 10, 1, 1, 1}, ExpectedResult = 1163)]
        [TestCase(new int[] {1000, 50, 10, 1, 1, 1}, ExpectedResult = 1063)]
        [TestCase(new int[] {50, 10, 1, 1, 1}, ExpectedResult = 63)]
        [TestCase(new int[] {10, 1, 1, 1}, ExpectedResult = 13)]
        [TestCase(new int[] {10, 1, 1}, ExpectedResult = 12)]
        [TestCase(new int[] {10, 1}, ExpectedResult = 11)]
        [TestCase(new int[] {10}, ExpectedResult = 10)]
        [TestCase(new int[] {10, 5, 1}, ExpectedResult = 16)]
        [TestCase(new int[] {10, 1, 5}, ExpectedResult = 16)]
        [TestCase(new int[] {1, 10}, ExpectedResult = 11)]
        [TestCase(new int[] {0}, ExpectedResult = 0)]
        [TestCase(new int[] {1}, ExpectedResult = 1)]
        [TestCase(new int[] {1000123456}, ExpectedResult = 1000123456)]
        [TestCase(new int[] {int.MaxValue}, ExpectedResult = int.MaxValue)]
        [TestCase(new int[] {int.MinValue}, ExpectedResult = int.MinValue)]
        [TestCase(new int[] {int.MaxValue, int.MinValue}, ExpectedResult = -1)]
        [TestCase(new int[] {int.MinValue, int.MinValue}, ExpectedResult = -4294967296)]
        [TestCase(new int[] {int.MaxValue, int.MaxValue}, ExpectedResult = 4294967294)]
        [TestCase(new int[] {int.MinValue, int.MaxValue}, ExpectedResult = -1)]
        public long Test_AssembleOutput_TakesInListOfInts_ReturnsAccumulatorAdditiveNotationValue(int[] integersSet)
        {
            /*Arrange*/
            long LocalLong = 0;
            int Size = integersSet.Length;
            List<int> LocalListOfInts = new List<int>();
            
            /*Act*/
            for (int i = 0; i < Size; i++)
            {
                LocalListOfInts.Add(integersSet[i]);
            }

            TestingAccumulators = TestingRomanNumeralParser.AssembleOutput(LocalListOfInts);
            
            /*Assert*/
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).subtractive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).additive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).irregular, Is.TypeOf(typeof(long)));
            
            return TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).additive;
        }

        [Test]
        [TestCase(new int[] {1000, 50, 500, 1, 5}, ExpectedResult = 444)]
        [TestCase(new int[] {1000, 10, 500, 1, 5}, ExpectedResult = 484)]
        [TestCase(new int[] {1000, 1000, 10, 500, 1, 1, 1}, ExpectedResult = -513)]
        [TestCase(new int[] {1000, 100, 1000, 10, 50, 1, 1, 1}, ExpectedResult = -163)]
        [TestCase(new int[] {1000, 100, 1000, 50, 10, 1, 1, 1}, ExpectedResult = -163)]
        [TestCase(new int[] {100, 1000, 50, 10, 1, 1, 1}, ExpectedResult = 837)]
        [TestCase(new int[] {1000, 50, 10, 1, 1, 1}, ExpectedResult = 937)]
        [TestCase(new int[] {50, 10, 1, 1, 1}, ExpectedResult = 37)]
        [TestCase(new int[] {10, 1, 1, 1}, ExpectedResult = 7)]
        [TestCase(new int[] {10, 1, 1}, ExpectedResult = 8)]
        [TestCase(new int[] {10, 1}, ExpectedResult = 9)]
        [TestCase(new int[] {10}, ExpectedResult = 10)]
        [TestCase(new int[] {10, 5, 1}, ExpectedResult = 4)]
        [TestCase(new int[] {10, 1, 5}, ExpectedResult = 4)]
        [TestCase(new int[] {1, 10}, ExpectedResult = 9)]
        [TestCase(new int[] {0}, ExpectedResult = 0)]
        [TestCase(new int[] {1}, ExpectedResult = 1)]
        [TestCase(new int[] {1000123456}, ExpectedResult = 1000123456)]
        [TestCase(new int[] {int.MaxValue}, ExpectedResult = int.MaxValue)]
        [TestCase(new int[] {int.MinValue}, ExpectedResult = int.MinValue)]
        [TestCase(new int[] {int.MaxValue, int.MinValue}, ExpectedResult = 4294967295)]
        [TestCase(new int[] {int.MinValue, int.MinValue}, ExpectedResult = 0)]
        [TestCase(new int[] {int.MaxValue, int.MaxValue}, ExpectedResult = 0)]
        [TestCase(new int[] {int.MinValue, int.MaxValue}, ExpectedResult = 4294967295)]
        public long Test_AssembleOutput_TakesInListOfInts_ReturnsAccumulatorIrregularSubtractiveNotationValue(int[] integersSet)
        {
            /*Arrange*/
            long LocalLong = 0;
            int Size = integersSet.Length;
            List<int> LocalListOfInts = new List<int>();
            
            /*Act*/
            for (int i = 0; i < Size; i++)
            {
                LocalListOfInts.Add(integersSet[i]);
            }

            TestingAccumulators = TestingRomanNumeralParser.AssembleOutput(LocalListOfInts);
            
            /*Assert*/
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts), Is.TypeOf(typeof(ConvertRomanNumeralsToInts.Accumulators)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).subtractive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).additive, Is.TypeOf(typeof(long)));
            Assert.That(TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).irregular, Is.TypeOf(typeof(long)));
            
            return TestingRomanNumeralParser.AssembleOutput(LocalListOfInts).irregular; 
        }

        #endregion
    }
}


