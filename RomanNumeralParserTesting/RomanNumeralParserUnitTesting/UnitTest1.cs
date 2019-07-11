using NUnit.Framework;
using System;

namespace Tests
{
    public class Tests
    {
       // RomanNumeralParser.Program program;
        public float[] testvals;
        public float testProperty { get; set; }

        [SetUp]
        public void Setup()
        {
            //program = new RomanNumeralParser.Program();

            testvals = new float[]
            {
                1, 2, 1.1f, 2.2f, 0, 3, 3.3f, 4, 4.4f
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        #region TEST_FLOATMATH
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(0.1f)]
        [TestCase(1.1f)]
        [TestCase(2.1f)]
        [TestCase(3.1f)]
        [TestCase(4.1f)]
        [TestCase(5.1f)]
        [TestCase(6.1f)]
        [TestCase(7.1f)]
        [TestCase(8.1f)]
        [TestCase(9.1f)]
        [TestCase(10.1f)]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [TestCase(-4)]
        [TestCase(-5)]
        [TestCase(-6)]
        [TestCase(-7)]
        [TestCase(-8)]
        [TestCase(-9)]
        [TestCase(-10)]
        [TestCase(-0.1f)]
        [TestCase(-1.1f)]
        [TestCase(-2.1f)]
        [TestCase(-3.1f)]
        [TestCase(-4.1f)]
        [TestCase(-5.1f)]
        [TestCase(-6.1f)]
        [TestCase(-7.1f)]
        [TestCase(-8.1f)]
        [TestCase(-9.1f)]
        [TestCase(-10.1f)]
        public void RunTest(float inputs)
        {
            float floatVar = 2 * inputs;
            float floatVar2 = 3 * inputs;
            bool t1 = floatVar == (2 * inputs) ? true : false;
            bool t2 = floatVar2 == (3 * inputs) ? true : false;

            Assert.IsTrue(t1);
            Assert.IsTrue(t2);
        }
        #endregion

        [Test]
        [TestCase("abcdefghij", ExpectedResult = "jihgfedcba")]
        public string TestStringStuff(string inputString)
        {
            string outputString = "";
            char[] stringArray = inputString.ToCharArray();

            for (int i = (stringArray.Length - 1); i >= 0; i--)
            {
                outputString = String.Concat(outputString, stringArray[i]);
            }

            return outputString;
        }
    }
}