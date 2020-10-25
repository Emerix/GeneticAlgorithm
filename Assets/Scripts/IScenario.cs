using UnityEngine;

public interface IScenario
{
    float[] Parameters { get; }

    void Clear();
    void Proceed();
    float GetScore();
    Transform GetTurret();
    void InitValues(float[] newValues);
    bool IsDone();
}