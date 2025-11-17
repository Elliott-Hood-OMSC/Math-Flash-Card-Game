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
    protected abstract QuestionInfo GetQuestionInfo(int numAnswers);
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
    
    protected override QuestionInfo GetQuestionInfo(int numAnswers)
    {
        QuestionInfo questionInfo = new QuestionInfo()
        {
            CorrectAnswer = 0,
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
/// All info needed for a presented question and its answers
/// </summary>
public struct QuestionInfo
{
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
