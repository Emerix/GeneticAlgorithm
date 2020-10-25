using UnityEngine;

public class SimpleTurretScenario
{
    public Transform ScenarioGoal { get; set; }
    public Turret ScenarioTurret { get; set; }

    public float[] Parameters {get;set;} 

    private Bullet bullet;
    private float score = float.PositiveInfinity;

    public SimpleTurretScenario(Transform goal, Turret turret, float[] values)
    {
        ScenarioGoal = goal;
        ScenarioTurret = turret;
        Parameters = values;
        InitValues(values);
    }

    public void InitValues(float[] values)
    {
        Parameters = values;
        ScenarioTurret.power = values[0];
        ScenarioTurret.degreeRotation = values[1];
    }

    public void Proceed()
    {
        ScenarioTurret.Rotate();
        bullet = ScenarioTurret.Shoot();
    }

    public bool IsDone()
    {
        return bullet.DidHitSomething;
    }

    public float GetScore()
    {
        score = Vector3.Distance(ScenarioGoal.transform.position, bullet.transform.position);
        return score;
    }

    public void Clear()
    {
        GameObject.Destroy(bullet.gameObject);
        Debug.Log("Destroying Bullet");
        bullet = null;
    }
}