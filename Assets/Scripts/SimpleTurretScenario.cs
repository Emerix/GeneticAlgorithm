using UnityEngine;

public class SimpleTurretScenario : IScenario
{
    public Transform ScenarioGoal { get; set; }
    public TurretParameters ScenarioTurretParameters { get; set; }

    public void Construct(ParametersBase parametersBase, Transform goal, float[] parameters)
    {
        ScenarioGoal = goal;
        ScenarioTurretParameters = parametersBase.GetComponent<TurretParameters>();
        Parameters = parameters;
        InitValues(parameters);
    }

    public float[] Parameters {get;set;} 

    private Bullet bullet;
    private float score = float.PositiveInfinity;

    public void InitValues(float[] newValues)
    {
        Parameters = newValues;
        ScenarioTurretParameters.SetParameters(newValues);
    }

    public void Proceed()
    {
        ScenarioTurretParameters.Rotate();
        bullet = ScenarioTurretParameters.Shoot();
    }

    public bool IsDone()
    {
        return bullet.DidHitSomething;
    }

    public float GetScore()
    {
        return score;
    }

    public void Clear()
    {
        GameObject.Destroy(bullet.gameObject);
        Debug.Log("Destroying Bullet");
        bullet = null;
    }

    public Transform GetTestedObject()
    {
        return ScenarioTurretParameters.transform;
    }

    public void SaveScore()
    {
        score = Vector3.Distance(ScenarioGoal.transform.position, bullet.transform.position);
    }
}