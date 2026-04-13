using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EquationGenerator : MonoBehaviour
{
    public int equationLength = 3;
    private int numberRange = 10;
    private List<int> numbersList = new List<int>();
    private List<string> operatorsList = new List<string>();
    private string[] operators;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        operators = new string[] { "+", "=", "-" };
        GenerateEquation();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateEquation()
    {
        EquationNode TestEq = new EquationNode();
        TestEq.GenerateEq(5,10);
        TestEq.printResults();
    }
}

public class EquationNode
{
    private EquationNode leftEq;
    private EquationNode rightEq;
    private string eqOperator;
    private int value;

    private const int NoValue = int.MinValue;

    public void GenerateEq(int eqLength, int targetNum)
    {
        value = NoValue;
        leftEq = null;
        rightEq = null;
        eqOperator = null;

        // Base case: leaf node
        if (eqLength <= 1)
        {
            value = targetNum;
            return;
        }

        // Build list of operators that are safe for this target
        List<string> validOperators = GetValidOperators(targetNum);

        // If nothing is valid, stop early as a leaf
        if (validOperators.Count == 0)
        {
            value = targetNum;
            return;
        }

        eqOperator = validOperators[Random.Range(0, validOperators.Count)];

        int[] nums = IntFromOp(eqOperator, targetNum);

        leftEq = new EquationNode();
        leftEq.GenerateEq(1, nums[0]);

        rightEq = new EquationNode();
        rightEq.GenerateEq(eqLength - 1, nums[1]);
    }

    private List<string> GetValidOperators(int targetNum)
    {
        List<string> valid = new List<string>();

        // + needs room to split target into two positive ints
        if (targetNum >= 2)
        {
            valid.Add("+");
        }

        // - is always possible if you allow positive integers
        valid.Add("-");

        // * only makes sense when target has a non-trivial factorization
        if (HasNonTrivialFactor(targetNum))
        {
            valid.Add("*");
        }

        return valid;
    }

    private bool HasNonTrivialFactor(int targetNum)
    {
        if (targetNum < 2)
        {
            return false;
        }

        for (int i = 2; i < targetNum; i++)
        {
            if (targetNum % i == 0)
            {
                return true;
            }
        }

        return false;
    }

    private int[] IntFromOp(string inputOperator, int targetNum)
    {
        int result1 = 0;
        int result2 = 0;

        switch (inputOperator)
        {
            case "+":
                // targetNum >= 2 guaranteed by GetValidOperators
                result1 = Random.Range(1, targetNum);
                result2 = targetNum - result1;
                break;

            case "-":
                // left - right = targetNum
                // choose a left value larger than targetNum
                result1 = Random.Range(targetNum + 1, targetNum + 11);
                result2 = result1 - targetNum;
                break;

            case "*":
                List<int> possibilities = new List<int>();

                for (int i = 2; i < targetNum; i++)
                {
                    if (targetNum % i == 0)
                    {
                        possibilities.Add(i);
                    }
                }

                // Safe because GetValidOperators only allows * if non-trivial factors exist
                result1 = possibilities[Random.Range(0, possibilities.Count)];
                result2 = targetNum / result1;
                break;
        }

        return new int[] { result1, result2 };
    }

    public int Evaluate()
    {
        if (value != NoValue)
        {
            return value;
        }

        int leftValue = leftEq.Evaluate();
        int rightValue = rightEq.Evaluate();

        switch (eqOperator)
        {
            case "+":
                return leftValue + rightValue;
            case "-":
                return leftValue - rightValue;
            case "*":
                return leftValue * rightValue;
            default:
                Debug.LogError("Unknown operator: " + eqOperator);
                return 0;
        }
    }

    public string ToExpressionString()
    {
        if (value != NoValue)
        {
            return value.ToString();
        }

        return "(" + leftEq.ToExpressionString() + " " + eqOperator + " " + rightEq.ToExpressionString() + ")";
    }

    public void printResults()
    {
        Debug.Log(ToExpressionString());
        Debug.Log("= " + Evaluate());
    }
}
