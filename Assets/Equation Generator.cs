using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class EquationGenerator : MonoBehaviour
{
    public int equationLength = 5;
    public int numberRange = 20;
    public GameObject equationText;
    public GameObject scoreText;
    private List<string> numberList;
    private List<string> operatorList;
    private List<string> inputList;

    public Button addButton;
    public Button subButton;
    public Button multButton;
    public Button eqButton;

    private int Score;

    void Start()
    {
        BuildNewEquation();

        addButton.onClick.AddListener(() => AddInput("+"));
        subButton.onClick.AddListener(() => AddInput("-"));
        multButton.onClick.AddListener(() => AddInput("*"));
        eqButton.onClick.AddListener(() => AddInput("="));
    }

    void BuildNewEquation()
    {
        int eq1Len = Random.Range(1, equationLength);
        string eq1String = GenerateEquation(eq1Len).eqToString();
        string eq2String = GenerateEquation(equationLength - eq1Len).eqToString();
        string equation = eq1String + "=" + eq2String;

        numberList = new List<string>();
        operatorList = new List<string>();

        string currentNumber = "";

        for (int i = 0; i < equation.Length; i++)
        {
            char c = equation[i];

            if (char.IsDigit(c))
            {
                currentNumber += c;
            }
            else
            {
                if (currentNumber != "")
                {
                    numberList.Add(currentNumber);
                    Debug.Log(currentNumber);
                    currentNumber = "";
                }

                operatorList.Add(c.ToString());
                Debug.Log(c.ToString());
            }
        }

        if (currentNumber != "")
        {
            numberList.Add(currentNumber);
            Debug.Log(currentNumber);
        }

        inputList = new List<string>(new string[operatorList.Count]);
    }

    void AddInput(string symbol)
    {
        for (int i = 0; i < inputList.Count; i++)
        {
            if (inputList[i] == null)
            {
                inputList[i] = symbol;
                break;
            }
        }
    }

    bool EvaluateExpression(string expression)
    {
        string[] sides = expression.Split('=');

        if (sides.Length != 2) return false;

        int left = EvaluateSide(sides[0]);
        int right = EvaluateSide(sides[1]);

        return left == right;
    }

    int EvaluateSide(string expr)
    {
        List<int> numbers = new List<int>();
        List<char> ops = new List<char>();

        string currentNumber = "";

        // Parse numbers and operators
        for (int i = 0; i < expr.Length; i++)
        {
            char c = expr[i];

            if (char.IsDigit(c))
            {
                currentNumber += c;
            }
            else
            {
                numbers.Add(int.Parse(currentNumber));
                currentNumber = "";
                ops.Add(c);
            }
        }

        // Add last number
        if (currentNumber != "")
        {
            numbers.Add(int.Parse(currentNumber));
        }

        // Handle multiplication first
        for (int i = 0; i < ops.Count; i++)
        {
            if (ops[i] == '*')
            {
                int result = numbers[i] * numbers[i + 1];
                numbers[i] = result;
                numbers.RemoveAt(i + 1);
                ops.RemoveAt(i);
                i--;
            }
        }

        // Then handle + and -
        int total = numbers[0];

        for (int i = 0; i < ops.Count; i++)
        {
            if (ops[i] == '+')
            {
                total += numbers[i + 1];
            }
            else if (ops[i] == '-')
            {
                total -= numbers[i + 1];
            }
        }

        return total;
    }

    void Update()
    {
        string displayString = "";
        bool isFull = true;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + Score.ToString();

        for (int i = 0; i < numberList.Count; i++)
        {
            displayString += numberList[i];

            if (i < inputList.Count)
            {
                if (inputList[i] == null)
                {
                    displayString += "_";
                    isFull = false;
                }
                else
                {
                    displayString += inputList[i];
                }
            }
        }

        equationText.GetComponent<TextMeshProUGUI>().text = displayString;

        if (isFull)
        {
            int equalsCount = 0;

            for (int i = 0; i < displayString.Length; i++)
            {
                if (displayString[i] == '=')
                {
                    equalsCount++;
                }
            }

            if (equalsCount != 1)
            {
                inputList = new List<string>(new string[operatorList.Count]);
            }
            else if (EvaluateExpression(displayString))
            {
                Score += 1;
                BuildNewEquation();
            }
            else
            {
                inputList = new List<string>(new string[operatorList.Count]);
            }
        }
    }

    EquationNode GenerateEquation(int eqLen)
    {
        EquationNode TestEq = new EquationNode();
        TestEq.operators = new List<string> { "+", "-", "*" };
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
    public List<string> operators;
    private List<string> nextOperators;

    public void GenerateEq(int eqLength, int targetNum)
    {
        eqOperator = operators[Random.Range(0, operators.Count)];
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
                List<int> nums = IntFromOp(eqOperator, targetNum);

                leftEq = new EquationNode();
                leftEq.operators = nextOperators;
                leftEq.GenerateEq(1, nums[0]);

                rightEq = new EquationNode();
                rightEq.operators = nextOperators;
                rightEq.GenerateEq(eqLength - 1, nums[1]);
                break;
        }
    }

    private List<int> IntFromOp(string inputOperator, int targetNum)
    {
        int result1 = 0;
        int result2 = 0;

        switch (inputOperator)
        {
            case "+":
                result1 = Random.Range(1, targetNum - 1);
                result2 = targetNum - result1;
                nextOperators = new List<string> { "+", "-", "*" };
                break;

            case "-":
                result1 = Random.Range(targetNum + 1, targetNum * 2 - 1);
                result2 = result1 - targetNum;
                nextOperators = new List<string> { "*" };
                break;

            case "*":
                List<int> possibilities = new List<int>();
                nextOperators = new List<string> { "*" };

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

        return new List<int> { result1, result2 };
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