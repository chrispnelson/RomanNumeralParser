using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RomanNumeralParser.Converter;

public class InputViewObject : MonoBehaviour
{
    public InputField InputFieldGameObject;
    public Text OutputField;

    public ConvertRomanNumeralsToInts romanNumeralParser;
    private ConvertRomanNumeralsToInts.Attributes attributes;
    private List<string> outputs;
    private string InputTextData;

    private void Start()
    {
        romanNumeralParser = new ConvertRomanNumeralsToInts();
        attributes = new ConvertRomanNumeralsToInts.Attributes();

        attributes = romanNumeralParser.Init();
    }

    public void OnSubmit()
    {
        InputTextData = InputFieldGameObject.text;
        OutputField.text = "";
        
        Debug.Log("Input Field Text Data: " + InputTextData);

        if (String.IsNullOrEmpty(InputTextData))
        {
            OutputField.text = "[NO INPUT] Enter a Roman Numeral Below!";
            Debug.LogError("[NO INPUT] Enter a Roman Numeral!");
        }
        else
        {
            attributes = romanNumeralParser.SendInputsToAccumulator(InputTextData);
        
            outputs = new List<string>()
            {
                "[OUTPUTS FOR NUMERALS] \n" + attributes.input + "\n",
                "[SUBTRACTIVE NOTATION VALUE] " + attributes.accumulators.subtractive + "\n",
                "[ADDITIVE NOTATION VALUE] " + attributes.accumulators.additive + "\n",
                "[IRREGULAR NOTATION VALUE] " + attributes.accumulators.irregular + "\n"
            };

            foreach (var outputsItem in outputs)
            {
                OutputField.text = OutputField.text + outputsItem;
            }
            
            Debug.Log(OutputField.text);
        }
    }
}
