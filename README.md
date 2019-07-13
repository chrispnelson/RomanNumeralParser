Unit testing Project:
- UnitTest_RomanNumeralsParser.cs = Unity Roman Numeral Parsing Program
- ConvertRomanNumeralsToInts.cs = Roman Numeral to Integer conversion class 
- InputViewObject.cs = Runtime UI for users running the app in unity

Folders:
UnityRomanNumeralParsingProject  = Unity Project 

NOTES:
Spent considerable time troubleshooting how to get the console Debugger SDK to work with Nunit, and was finding there were several issues that impeded other users besides me that I did not have time to resolve. Per Robert F.'s suggestion, I have switched to using Unity Test Runner, and added a view for running and testing the code in Unity Editor. The Console app was running the program, and NUnit could run tests. However, the app dependencies were not 100% compatible with ".NET Core." 

***** Tests In Progress ***** 
- public Attibutes SendInputsToAccumulator(string inputsString)

- public List<int> DiassembleStringToIntTokens(string Inputs)
- public int GetIntValue(char numeral)
- public void WriteIntTokens(Attibutes attributeSet)
- public Accumulators CalculateAdditiveValue(Accumulators acumulatorSet, List<int> numbersSet)
- public Accumulators CalculateSubtractiveValue(Accumulators acumulatorSet, List<int> numbersSet)
- public Accumulators CalculateIrregularValue(Accumulators acumulatorSet, List<int> numbersSet)
- public Accumulators AssembleOutput(List<int> numbersSet)

***** Tests Implemented ***** 
- public Attibutes Init()
- public string GetCleanedStringToken(string InputString = "")
- public List<int> ConvertToInts(char[] glyphs)
- public Numerals GetNumerals(char searchChar

**** Tests Verified Indirectly ****
- private static Dictionary<Numerals, char> SetNumeralPairs()
- private bool IsDone()
- private static void PostHelpMSG()
