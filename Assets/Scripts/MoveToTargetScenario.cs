using UnityEngine;

public class MoveToTargetScenario<P> : IScenario where P : ParametersBase
{
    protected P trackedObject;
    protected float score = float.PositiveInfinity;
    private float previousScore = float.MaxValue;
    private float timeLeft = float.MaxValue;
    private bool isDone = false;
    public float[] Parameters { get; set; }
    public Transform goal { get; set; }

    public void Construct(ParametersBase parametersBase, Transform goal, float[] parameters)
    {
        trackedObject = (P) parametersBase;
        this.goal = goal;
        InitValues(parameters);
        isDone = false;
    }

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
        return trackedObject.transform;
    }

    public void InitValues(float[] newValues)
    {
        Parameters = newValues;
        trackedObject.SetParameters(newValues);
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

    public float GetCurrentScore()
    {
        return CalculateNewScore();
    }

    public void Proceed()
    {
        trackedObject.gameObject.SetActive(true);

        ResetMovementTimeout();
    }

    public void SaveScore()
    {
        float newScore = CalculateNewScore();
        score = newScore;
    }

    private void ResetMovementTimeout()
    {
        timeLeft = AdditionalTestParameters.TimeOutWithoutMovement;
    }

    private void UpdateMovementTimeout()
    {
        float newScore = CalculateNewScore();
        if (Mathf.Abs(newScore - previousScore) > AdditionalTestParameters.MinMovementInTimeframeToPreventTimeout)
        {
            ResetMovementTimeout();
            previousScore = newScore;
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }

    private float CalculateNewScore()
    {
        float newScore = float.MaxValue;
        foreach (Transform child in trackedObject.transform)
        {
            float childDistance = Vector3.Distance(goal.transform.position, child.position);
            newScore = Mathf.Min(score, childDistance);
        }

        return newScore;
    }
}

public class SnakeScenario : MoveToTargetScenario<SnakeParameters>
{
}

public class CarScenario : MoveToTargetScenario<CarParameters>
{
}