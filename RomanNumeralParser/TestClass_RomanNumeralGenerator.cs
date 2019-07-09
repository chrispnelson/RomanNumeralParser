using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;


namespace RomanNumeralParser.Test
{
    [TestFixture]
    public class TestClass_RomanNumeralGeneratorTheoryTest
    {
        #region INPUT_TESTDATA

        public static string temp;
        public static Queue<string> testingSet;

        public const string NULLENTRY = "";

        public const string BAD_CHARS = @" !@#$%^&*()_+`1234567890-={}|[]\:""'<>?,./";

        public static string TYPICAL_DATA
        {
            get
            {
                return "MCMXIXIVII";
            }
        }

        public static string TYPICAL_DATA_SPACED
        {
            get
            {
                return " M C M X I X I V I I ";
            }
        }

        public static string[] INVERTED_SET
        {
            get
            {
                string[] retSet =
                {
                    "IVXLCDM",
                    "IVIXILICIDIM",
                    "VXVLVCVDVM",
                    "XLXCXDXM",
                    "LCLDLM",
                    "CDCM",
                    "IIIIIIVVVVVVXXXXXXLLLLLLCCCCCCDDDDDDMMMMMMM",
                    " I I I I I I V V V V V V X X X X X X L L L L L L C C C C C C D D D D D D M M M M M M M "
                };

                return retSet;
            }
        }

        public static string[] ERROR_EXAMPLE_SET
        {
            get
            {
                string[] retSet =
                {
                    "IVPITER",
                    "A",
                    "B",
                    "E",
                    "F",
                    "G",
                    "H",
                    "J",
                    "K",
                    "N",
                    "O",
                    "P",
                    "Q",
                    "R",
                    "S",
                    "T",
                    "U",
                    "W",
                    "Y",
                    "Z",
                    "ↅ",
                    "Ω",
                    " ̷",
                    "AB",
                    "ABE",
                    "ABEF",
                    "ABEFG",
                    "ABEFGH",
                    "ABEFGHJ",
                    "ABEFGHJK",
                    "ABEFGHJKN",
                    "ABEFGHJKNO",
                    "ABEFGHJKNOP",
                    "ABEFGHJKNOPQ",
                    "ABEFGHJKNOPQR",
                    "ABEFGHJKNOPQRS",
                    "ABEFGHJKNOPQRST",
                    "ABEFGHJKNOPQRSTU",
                    "ABEFGHJKNOPQRSTUW",
                    "ABEFGHJKNOPQRSTUWY",
                    "ABEFGHJKNOPQRSTUWYZ",
                    "ABEFGHJKNOPQRSTUWYZↅ",
                    "ABEFGHJKNOPQRSTUWYZↅΩ",
                    "ABEFGHJKNOPQRSTUWYZↅΩ ̷",
                    "QRSTUWYZↅΩ ̷QRSTUWYZↅΩ ̷QRSTUWYZↅΩ ̷ABEFGHJKNOPQRSTUWYZↅΩ ̷QRSTUWYZↅΩ ̷QRSTUWYZↅΩ ̷QRSTUWYZↅΩ ̷",
                    "0123456789",
                    "9876543210",
                    @" !@#$%^&*()_+`1234567890-={}|[]\:""''<>?,./",
                    @" ! @ # $ % ^ & * ( ) _ + ` 1 2 3 4 5 6 7 8 9 0 - = { } | [ ] \ : "" '' < > ? , . /",
                    @" !@#$%^&*()_+`1234567890-={}|[]\:""''<>?,./" + @" !@#$%^&*()_+`1234567890-={}|[]\:""''<>?,./"
                };

                return retSet;
            }
        }

        public static string[] EXAMPLE_SET
        {
            get
            {
                string[] retSet =
                {
                    "XXXIX",
                    "CCXLVI",
                    "DCCLXXXIX",
                    "MMCDXXI",
                    "CLX",
                    "CCVII",
                    "MIX",
                    "MLXVI",
                    "MDCCLXXVI",
                    "MCMLIV",
                    "MMXIV",
                    "MMXIX",
                    "MCMLXXIII",
                    "MCMLXXXIV",
                    "MCMXCIX",
                    "MMMCMXCIX",
                    "IIII",
                    "XXXX",
                    "CCCC",
                    "XXIIII",
                    "LXXIIII",
                    "CCCCLXXXX",
                    "VIIII",
                    "LXXXX",
                    "DCCCC",
                    "XLIIII",
                    "MCM",
                    "MDCCCCX",
                    "MCMX",
                    "MDCDIII",
                    "MCMIII",
                    "IIIXX",
                    "IIXX",
                    "IIC",
                    "IC",
                    "XVIII",
                    "XVIXIII",
                    "XVCXIX"
                };

                return retSet;
            }
        }

        public static string LARGE_SET
        {
            get
            {
                string retSet =
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM" +
                "MCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIMCMXIXIVIIM";

                return retSet;
            }
        }

        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                testingSet.TryDequeue(out temp);
                yield return new TestCaseData(temp).Returns(temp.Replace(" ", "").ToUpper());
            }
        }

        #endregion

        [SetUp]
        public void Init()
        {
            temp = "";
            testingSet = new Queue<string>();

            testingSet.Enqueue(NULLENTRY);
            testingSet.Enqueue(BAD_CHARS);
            testingSet.Enqueue(TYPICAL_DATA);
            testingSet.Enqueue(TYPICAL_DATA_SPACED);
            testingSet.Enqueue(LARGE_SET);

            for (int i = 0; i < INVERTED_SET.Length; i++)
            {
                testingSet.Enqueue(INVERTED_SET[i]);
            }

            for (int i = 0; i < ERROR_EXAMPLE_SET.Length; i++)
            {
                testingSet.Enqueue(ERROR_EXAMPLE_SET[i]);
            }

            for (int i = 0; i < EXAMPLE_SET.Length; i++)
            {
                testingSet.Enqueue(EXAMPLE_SET[i]);
            }
        }

        [Test]
        public void Test_ArbitraryInstanceTest()
        {
            RomanNumeralParser.Program rnProgram = new Program();

            Assert.IsNotNull(rnProgram);
        }

        [TestCase("", ExpectedResult = "")]
        public string Test_InputputNullProcessingErrorFree(string input)
        {
            Program rnProgram = new Program();

            Assert.That(Program.GetStringTokensFromInput(NULLENTRY), Is.Empty);

            Assert.That(Program.GetStringTokensFromInput(input, true), Is.TypeOf(typeof(string)));

            return Program.GetStringTokensFromInput(input, true);
        }

        [TestCase(BAD_CHARS, ExpectedResult = BAD_CHARS)]
        public string Test_InputputProcessingErrorFree(string input)
        {
            Program rnProgram = new Program();

            Assert.That(input.Length >= 0 && input.Length < Int32.MaxValue);

            Assert.That
            (
                Program.GetStringTokensFromInput(input, true) == input.Replace(" ", "").ToUpper()
            );

            Assert.That(Program.GetStringTokensFromInput(NULLENTRY), Is.Empty);

            Assert.That(Program.GetStringTokensFromInput(input, true), Is.TypeOf(typeof(string)));

            return Program.GetStringTokensFromInput(input, true);
        }
    }

    public class TestDataClass
    {

    }
}
