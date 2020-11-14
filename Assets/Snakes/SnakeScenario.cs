using System;
using UnityEngine;

public class SnakeScenario : IScenario
{
    private readonly SnakeParameters snake;
    private float score = float.PositiveInfinity;

    private float previousScore = float.MaxValue;
    private float timeLeft = float.MaxValue;
    private bool isDone = false;
    public SnakeScenario(SnakeParameters snake, Transform goal, float[] scenarioParameters)
    {
        this.snake = snake;
        this.goal = goal;
        InitValues(scenarioParameters);
        isDone = false;
    }

    public float[] Parameters { get; set; }
    public Transform goal { get; set; }

    public void Clear()
    {
        isDone = false;
        score = float.PositiveInfinity;
        timeLeft = float.MaxValue;
        previousScore = float.MaxValue;
    }

    public float GetScore()
    {
        return score;
    }

    public Transform GetTestedObject()
    {
        return snake.transform;
    }

    public void InitValues(float[] newValues)
    {
        Parameters = newValues;
        snake.SetParameters(newValues);
    }

    public bool IsDone()
    {
        if (isDone)
        {
            return true;
        }
        UpdateMovementTimeout();
        if (timeLeft < 0)
        {
            Debug.Log($"Timed out");
            isDone = true;
        }
        return isDone;
    }

    public void Proceed()
    {
        snake.gameObject.SetActive(true);
        ResetMovementTimetout();
    }

    public void SaveScore()
    {
        float newScore = CalculateNewScore();
        score = newScore;
    }

    private float CalculateNewScore()
    {
        float newScore = float.MaxValue;
        foreach (Transform child in snake.transform)
        {
            float childDistance = Vector3.Distance(goal.transform.position, child.position);
            newScore = Mathf.Min(score, childDistance);
        }

        return newScore;
    }

    private void ResetMovementTimetout()
    {
        timeLeft = Snakes.instance.timeOutWithoutMovement;
    }

    private void UpdateMovementTimeout()
    {
        float newScore = CalculateNewScore();
        if (Mathf.Abs(newScore - previousScore) > Snakes.instance.minMovementInTimeframeToPreventTimeout)
        {
            ResetMovementTimetout();
            previousScore = newScore;
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }
}