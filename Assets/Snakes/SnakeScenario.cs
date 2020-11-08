using UnityEngine;

public class SnakeScenario : IScenario
{
    private readonly SnakeParameters snake;
    private float score = float.PositiveInfinity;
    public SnakeScenario(SnakeParameters snake, Transform goal, float[] scenarioParameters)
    {
        this.snake = snake;
        this.goal = goal;
        InitValues(scenarioParameters);
    }

    public float[] Parameters { get; set; }
    public Transform goal { get; set; }

    public void Clear()
    {

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
        return snake.gameObject == null;
    }

    public void Proceed()
    {
        snake.gameObject.SetActive(true);
    }

    public void SaveScore()
    {
        score = Vector3.Distance(goal.transform.position, snake.transform.position);
    }
}