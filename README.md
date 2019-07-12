Unit testing Project:
- UnitTest_RomanNumeralsParser.cs = Unity Roman Numeral Parsing Program
- ConvertRomanNumeralsToInts.cs = Roman Numeral to Integer conversion class 
- InputViewObject.cs = Runtime UI for users running the app in unity

Folders:
UnityRomanNumeralParsingProject  = Unity Project 
RomanNumeralParser = Console Project  
RomanNumeralParserTesting = NUnit Console Project

NOTES:
Spent considerable time troubleshooting how to get the console Debugger SDK to work with Nunit, and was finding there were several issues that impeded other users besides me that I did not have time to resolve. Per Robert F.'s suggestion, I have switched to using Unity Test Runner, and added a view for running and testing the code in Unity Editor. The Console app was running the program, and NUnit could run tests. However, the app dependencies were not 100% compatible with ".NET Core." 

