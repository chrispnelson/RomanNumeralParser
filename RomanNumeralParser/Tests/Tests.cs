using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Excel = Microsoft.Office.Interop.Excel;
using RomanNumeralParser;

[TestFixture]
public class Test
{
    public struct TEST_STRINGS
    {
        public ExcelHelperClass exHelper;
        public ExcelHelperClass.ExcelFile exFile;
        public ExcelHelperClass.TestData testData;
        public ExcelHelperClass.CellTarget CellTarget;
    }

    public TEST_STRINGS helper;

    [SetUp]
    public void Init()
    {
        Program rn = new Program();

        /*
        helper.exHelper = new ExcelHelperClass();
        helper.exFile = helper.exHelper.GetExcelFile();
        helper.CellTarget = helper.exHelper.GetSingleRange(helper.exFile.worksheet, "1", "A");

        List<string> testStrings = new List<string>();

        helper.testData.testdata = new string[3];


        helper.testData.testdata[0] = helper.exHelper.GetStringFromRange
            (
                helper.exFile.worksheet,
                helper.CellTarget
            );
        helper.testData.testdata[1] = helper.exHelper.GetStringFromRange
            (
                helper.exFile.worksheet,
                helper.CellTarget
            );
        helper.testData.testdata[2] = helper.exHelper.GetStringFromRange
            (
                helper.exFile.worksheet,
                helper.CellTarget
            );
        */
    }

    [TearDown]
    public void TearDown()
    {
        helper.exHelper.CloseExcel(helper.exFile);
    }

    [Test]
    public void Test_TestRunnerExecute()
    {
        Assert.AreSame(1, 1);
    }

    [Test, TestCaseSource("helper.testData.testdata")]
    public void Test_GetInputMethod(string inputs)
    {
        Assert.That(inputs, Is.Not.Empty);

        Console.WriteLine(inputs);
    }
}

public class ExcelHelperClass
{
    public struct ExcelFile
    {
        public string filePath;
        public Excel.Workbook workbook;
        public Excel.Worksheet worksheet;
        public Excel.Worksheets worksheets;
        public Excel.Sheets sheets;
    }

    public struct TestData
    {
        public ExcelFile exFile;
        public string[] testdata;
    }

    public struct CellTarget
    {
        public string row;
        public string col;
        public Excel.Range Range;
    }

    public CellTarget GetSingleRange(Excel.Worksheet worksheet, string column, string row)
    {
        CellTarget retCellTarget;

        retCellTarget.row = row;
        retCellTarget.col = column;
        retCellTarget.Range = (Excel.Range)worksheet.Cells[row, column];

        return retCellTarget;
    }

    public string GetStringFromRange(Excel.Worksheet worksheet, CellTarget cell)
    {
        string retVal;

        retVal = worksheet.Cells[cell.row, cell.col].ToString();

        return retVal;
    }

    public static string[] TestDataStrings(List<string> testStrings)
    {
        string[] retString = new string[testStrings.Count];

        for (int i = 0; i < testStrings.Count; i++)
        {
            retString[i] = testStrings[i];
        }

        return retString;
    }

    public ExcelFile GetExcelFile(string filePath = "TestInputData.xlsx")
    {
        ExcelFile exFile;
        exFile.filePath = filePath;

        Excel.Application excelApp = new Excel.Application();
        exFile.workbook = excelApp.Workbooks.Open(exFile.filePath);
        exFile.worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
        exFile.worksheets = (Excel.Worksheets)excelApp.Worksheets;
        exFile.sheets = excelApp.Sheets;

        excelApp.Visible = true;

        return exFile;
    }

    public void CloseExcel(ExcelFile excelFile)
    {
        excelFile.workbook.Close(false, excelFile.filePath, null);
        Marshal.ReleaseComObject(excelFile.workbook);
    }

    public void ExcelLoadTestData()
    {
        ExcelFile ec;
        ec.filePath = "TestInputData.xlsx";
        Excel.Application excelApp = new Excel.Application();

        ec.workbook = excelApp.Workbooks.Open(ec.filePath);
        ec.worksheet = (Excel.Worksheet)excelApp.ActiveSheet;

        excelApp.Visible = true;
        string workBookName = ec.workbook.Name;
        int numberOfSheets = ec.workbook.Sheets.Count;

        ec.workbook.Close(false, ec.filePath, null);
        Marshal.ReleaseComObject(ec.workbook);
    }
}