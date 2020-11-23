using UnityEngine;

public interface IScenario
{
    void Construct(ParametersBase parametersBase, Transform goal, float[] parameters);
    float[] Parameters { get; }

    void Clear();
    void Proceed();
    float GetScore();
    void SaveScore();
    Transform GetTestedObject();
    void InitValues(float[] newValues);
    bool IsDone();
}
