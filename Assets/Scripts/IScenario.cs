using UnityEngine;

public interface IScenario
{
    float[] Parameters { get; }

    void Clear();
    void Proceed();
    float GetScore();
    void SaveScore();
    Transform GetTestedObject();
    void InitValues(float[] newValues);
    bool IsDone();
}
