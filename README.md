Unit testing Project:
- UnitTest_RomanNumeralsParser.cs = Unity Roman Numeral Parsing Program
- ConvertRomanNumeralsToInts.cs = Roman Numeral to Integer conversion class 
- InputViewObject.cs = Runtime UI for users running the app in unity

Folders:
UnityRomanNumeralParsingProject  = Unity Project 

NOTES:
Spent considerable time troubleshooting how to get the console Debugger SDK to work with Nunit, and was finding there were several issues that impeded other users besides me that I did not have time to resolve. Per Robert F.'s suggestion, I have switched to using Unity Test Runner, and added a view for running and testing the code in Unity Editor. The Console app was running the program, and NUnit could run tests. However, the app dependencies were not 100% compatible with ".NET Core." 

***** Tests In Progress ***** 
Class InputViewObject : MonoBehaviour

- private void Start()
- public void OnSubmit()

***** Tests Implemented ***** 
- Class ConvertRomanNumeralsToInts
- public Attributes Init()
- public Numerals GetNumerals(char searchChar
- public int GetIntValue(char numeral)
- public List<int> ConvertToInts(char[] glyphs)
- public string GetCleanedStringToken(string InputString = "")
- public Accumulators CalculateSubtractiveValue(Accumulators accumulatorSet, List<int> numbersSet)
- public Accumulators CalculateAdditiveValue(Accumulators accumulatorSet, List<int> numbersSet)
- public Accumulators CalculateIrregularValue(Accumulators accumulatorSet, List<int> numbersSet)
- public List<int> DisassembleStringToIntTokens(string Inputs)
- public Accumulators AssembleOutput(List<int> numbersSet)
- public Attributes SendInputsToAccumulator(string inputsString)
--- Tests subtractive notation
--- Tests additive notation
--- Tests irregular subtractive notation

**** Tests Verified Indirectly ****
- Class ConvertRomanNumeralsToInts
- private static Dictionary<Numerals, char> SetNumeralPairs()
- private void WriteIntTokens(Attributes attributeSet)
- private static void PostHelpMSG()
- private bool IsDone()

