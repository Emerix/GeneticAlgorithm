using System;
using UnityEngine;

public class CarScenario : IScenario
{
    public void Construct(ParametersBase parametersBase, Transform goal, float[] parameters)
    {
        throw new NotImplementedException();
    }

    public float[] Parameters { get; }
    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Proceed()
    {
        throw new NotImplementedException();
    }

    public float GetScore()
    {
        throw new NotImplementedException();
    }

    public void SaveScore()
    {
        throw new NotImplementedException();
    }

    public Transform GetTestedObject()
    {
        throw new NotImplementedException();
    }

    public void InitValues(float[] newValues)
    {
        throw new NotImplementedException();
    }

    public bool IsDone()
    {
        throw new NotImplementedException();
    }
}