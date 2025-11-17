// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

/// <summary>
/// A Command class responsible for creating and calling PresentQuestion on the questionPresenter.
/// Can be overridden to generate any kind of question info (for now, just multiplication)
/// </summary>
public abstract class QuestionGenerator : Command
{
    protected QuestionPresenter _questionPresenter;
    
    protected const int NUM_NUMBERS_IN_EQUATION = 2;
    public abstract QuestionInfo GetQuestionInfo(int numAnswers);
    public override void Execute()
    {
        QuestionInfo questionInfo = GetQuestionInfo(_questionPresenter.NumAnswerCards);
        _questionPresenter.PresentQuestion(questionInfo);
    }

    public override void Undo()
    {
        // For future implementation
    }
}

/// <summary>
/// Creates 2 possible wrong answers, and 1 correct answer.
/// All answers are unique. 
/// </summary>
public class QuestionGeneratorMultiplication : QuestionGenerator
{
    public QuestionGeneratorMultiplication(QuestionPresenter questionPresenter)
    {
        _questionPresenter = questionPresenter;
    }

    public override QuestionInfo GetQuestionInfo(int numAnswers)
    {
        QuestionInfo questionInfo = new QuestionInfo()
        {
            CorrectAnswer = 0,
            OperationSign = "x",
            WrongAnswers = new int[numAnswers - 1],
            QuestionDisplay = new QuestionDisplay
            {
                Numbers = new int[NUM_NUMBERS_IN_EQUATION]
            }
        };
        
        HashSet<int> usedValues = new HashSet<int>();

        for (int index = 0; index < numAnswers; index++)
        {
            int num1, num2, product;

            // Keep generating until we find a unique product
            do
            {
                num1 = Random.Range(1, 13);
                num2 = Random.Range(1, 13);
                product = num1 * num2;
            }
            while (usedValues.Contains(product));

            usedValues.Add(product);

            // Assign the last one as the correct card
            if (index == numAnswers - 1)
            {
                questionInfo.CorrectAnswer = product;
                questionInfo.QuestionDisplay.Numbers[0] = num1;
                questionInfo.QuestionDisplay.Numbers[1] = num2;
            }
            else
            {
                questionInfo.WrongAnswers[index] = product;
            }
        }

        return questionInfo;
    }
}

/// <summary>
/// Creates 2 possible wrong answers, and 1 correct answer for addition problems.
/// All answers are unique. Solutions range from 2 to 144.
/// </summary>
public class QuestionGeneratorAddition : QuestionGenerator
{
    public QuestionGeneratorAddition(QuestionPresenter questionPresenter)
    {
        _questionPresenter = questionPresenter;
    }

    public override QuestionInfo GetQuestionInfo(int numAnswers)
    {
        QuestionInfo questionInfo = new QuestionInfo()
        {
            CorrectAnswer = 0,
            OperationSign = "+",
            WrongAnswers = new int[numAnswers - 1],
            QuestionDisplay = new QuestionDisplay
            {
                Numbers = new int[NUM_NUMBERS_IN_EQUATION]
            }
        };

        HashSet<int> usedValues = new HashSet<int>();

        for (int index = 0; index < numAnswers; index++)
        {
            int num1, num2, sum;

            // Keep generating until we find a unique sum (2-144)
            do
            {
                num1 = Random.Range(1, 144); // 1 to 143
                int maxNum2 = Mathf.Min(143, 144 - num1); // Ensure sum <= 144
                num2 = Random.Range(1, maxNum2 + 1);
                sum = num1 + num2;
            }
            while (usedValues.Contains(sum));

            usedValues.Add(sum);

            // Assign the last one as the correct card
            if (index == numAnswers - 1)
            {
                questionInfo.CorrectAnswer = sum;
                questionInfo.QuestionDisplay.Numbers[0] = num1;
                questionInfo.QuestionDisplay.Numbers[1] = num2;
            }
            else
            {
                questionInfo.WrongAnswers[index] = sum;
            }
        }

        return questionInfo;
    }
}

/// <summary>
/// Creates 2 possible wrong answers, and 1 correct answer for subtraction problems.
/// All answers are unique. Solutions range from 1 to 144.
/// </summary>
public class QuestionGeneratorSubtraction : QuestionGenerator
{
    public QuestionGeneratorSubtraction(QuestionPresenter questionPresenter)
    {
        _questionPresenter = questionPresenter;
    }

    public override QuestionInfo GetQuestionInfo(int numAnswers)
    {
        QuestionInfo questionInfo = new QuestionInfo()
        {
            CorrectAnswer = 0,
            OperationSign = "-",
            WrongAnswers = new int[numAnswers - 1],
            QuestionDisplay = new QuestionDisplay
            {
                Numbers = new int[NUM_NUMBERS_IN_EQUATION]
            }
        };

        HashSet<int> usedValues = new HashSet<int>();

        for (int index = 0; index < numAnswers; index++)
        {
            int num1, num2, difference;

            // Keep generating until we find a unique difference (1-144)
            do
            {
                difference = Random.Range(1, 145); // 1 to 144
                num2 = Random.Range(1, 145); // Keep within reasonable range
                num1 = difference + num2; // Ensures difference = num1 - num2
            }
            while (usedValues.Contains(difference));

            usedValues.Add(difference);

            // Assign the last one as the correct card
            if (index == numAnswers - 1)
            {
                questionInfo.CorrectAnswer = difference;
                questionInfo.QuestionDisplay.Numbers[0] = num1;
                questionInfo.QuestionDisplay.Numbers[1] = num2;
            }
            else
            {
                questionInfo.WrongAnswers[index] = difference;
            }
        }

        return questionInfo;
    }
}

/// <summary>
/// Creates 2 possible wrong answers, and 1 correct answer for division problems.
/// All answers are unique and result in whole numbers.
/// </summary>
public class QuestionGeneratorDivision : QuestionGenerator
{
    public QuestionGeneratorDivision(QuestionPresenter questionPresenter)
    {
        _questionPresenter = questionPresenter;
    }

    public override QuestionInfo GetQuestionInfo(int numAnswers)
    {
        QuestionInfo questionInfo = new QuestionInfo()
        {
            CorrectAnswer = 0,
            OperationSign = "/",
            WrongAnswers = new int[numAnswers - 1],
            QuestionDisplay = new QuestionDisplay
            {
                Numbers = new int[NUM_NUMBERS_IN_EQUATION]
            }
        };

        HashSet<int> usedValues = new HashSet<int>();

        for (int index = 0; index < numAnswers; index++)
        {
            int divisor, quotient, dividend;

            // Keep generating until we find a unique quotient
            do
            {
                divisor = Random.Range(1, 13);
                quotient = Random.Range(1, 13);
                dividend = divisor * quotient; // Ensures whole number division
            }
            while (usedValues.Contains(quotient));

            usedValues.Add(quotient);

            // Assign the last one as the correct card
            if (index == numAnswers - 1)
            {
                questionInfo.CorrectAnswer = quotient;
                questionInfo.QuestionDisplay.Numbers[0] = dividend;
                questionInfo.QuestionDisplay.Numbers[1] = divisor;
            }
            else
            {
                questionInfo.WrongAnswers[index] = quotient;
            }
        }

        return questionInfo;
    }
}

/// <summary>
/// Creates questions by randomly selecting from Addition, Subtraction, Multiplication, or Division.
/// </summary>
public class QuestionGeneratorAll : QuestionGenerator
{
    private QuestionGenerator[] _generators;

    public QuestionGeneratorAll(QuestionPresenter questionPresenter)
    {
        _questionPresenter = questionPresenter;
        _generators = new QuestionGenerator[]
        {
            new QuestionGeneratorAddition(questionPresenter),
            new QuestionGeneratorSubtraction(questionPresenter),
            new QuestionGeneratorMultiplication(questionPresenter),
            new QuestionGeneratorDivision(questionPresenter)
        };
    }

    public override QuestionInfo GetQuestionInfo(int numAnswers)
    {
        // Randomly select one of the four generator types
        int randomIndex = Random.Range(0, _generators.Length);
        return _generators[randomIndex].GetQuestionInfo(numAnswers);
    }

    public override void Execute()
    {
        QuestionInfo questionInfo = GetQuestionInfo(_questionPresenter.NumAnswerCards);
        _questionPresenter.PresentQuestion(questionInfo);
    }
}

/// <summary>
/// All info needed for a presented question and its answers
/// </summary>
public struct QuestionInfo
{
    public string OperationSign;
    public int CorrectAnswer;
    public int[] WrongAnswers;
    public QuestionDisplay QuestionDisplay;
}

/// <summary>
/// All info needed for the actual question display
/// </summary>
public struct QuestionDisplay
{
    public int[] Numbers;
}
