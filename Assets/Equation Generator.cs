using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class EquationGenerator : MonoBehaviour
{
    public int equationLength = 5;
    public int numberRange = 20;
    public GameObject equationText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int eq1Len = Random.Range(1, equationLength);
        string eq1String = GenerateEquation(eq1Len).eqToString();
        string eq2String = GenerateEquation(equationLength - eq1Len).eqToString();
        equationText.GetComponent<TextMeshProUGUI>().text = eq1String + "=" + eq2String;
    }
   
    // Update is called once per frame
    void Update()
    {

    }

    EquationNode GenerateEquation(int eqLen)
    {
        EquationNode TestEq = new EquationNode();
        TestEq.operators = new string[] { "+", "-", "*" };
        TestEq.GenerateEq(eqLen, numberRange);
        return TestEq;
    }
}

public class EquationNode
{
    private EquationNode leftEq;
    private EquationNode rightEq;
    private string eqOperator;
    private int value;
    public string[] operators;
    private string[] nextOperators;
    public void GenerateEq(int eqLength, int targetNum)
    {
        eqOperator = operators[Random.Range(0, operators.Length)];
        value = int.MinValue;
        switch (eqLength)
        {
            case 1:
                leftEq = null;
                rightEq = null;
                value = targetNum;
                eqOperator = null;
                break;
            case > 1:
                int randVar = Random.Range(0, 1);
                int[] nums = IntFromOp(eqOperator, targetNum);
                leftEq = new EquationNode();
                leftEq.operators = nextOperators;
                leftEq.GenerateEq(1, nums[0]);

                rightEq = new EquationNode();
                rightEq.operators = nextOperators;
                rightEq.GenerateEq(eqLength - 1, nums[1]);
                break;
        }
    }

    private int[] IntFromOp(string inputOperator, int targetNum)
    {
        int result1 = 0;
        int result2 = 0;
        switch (inputOperator)
        {
            case "+":
                result1 = Random.Range(1, targetNum - 1);
                result2 = targetNum - result1;
                nextOperators = new string[] { "+", "-", "*" };
                break;
            case "-":
                result1 = Random.Range(targetNum + 1, targetNum * 2 - 1);
                result2 = result1 - targetNum;
                nextOperators = new string[] { "*" };
                break;
            case "*":
                List<int> possibilities = new List<int>();
                nextOperators = new string[] { "*" };
                for (int i = 1; i <= targetNum; i++)
                {
                    if (targetNum % i == 0)
                    {
                        possibilities.Add(i);
                    }
                }

                result1 = possibilities[Random.Range(0, possibilities.Count)];
                result2 = targetNum / result1;
                break;
        }

        int[] results = { result1, result2 };
        return results;
    }

    public string eqToString()
    {
        string returnString = "";
        if (value != int.MinValue)
        {
            returnString += value;
        }
        if (leftEq != null)
        {
            returnString += leftEq.eqToString();
        }
        if (eqOperator != null)
        {
            returnString += eqOperator;
        }
        if (rightEq != null)
        {
            returnString += rightEq.eqToString();
        }
        return returnString;
    }
}