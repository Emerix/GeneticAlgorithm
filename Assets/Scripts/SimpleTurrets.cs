using System.Collections.Generic;
using UnityEngine;

public class SimpleTurrets : AbstractParallelGeneticTest
{
    [SerializeField]
    private float MaxPower = 100;
    [SerializeField]
    private float MaxRotation = 360;
    [SerializeField]
    private TurretParameters turretParametersPrefab;
    [SerializeField]
    private Transform goalPrefab;
    [SerializeField]
    private Transform boxPrefab;

    [SerializeField]
    private float boxDistance = 11;
    [SerializeField]
    private int boxesInARow = 5;

    [SerializeField]
    private Vector3 goalPosition = new Vector3(-2, 0, 4);

    private float GetRandomPower()
    {
        return Random.Range(0, MaxPower);
    }
    private float GetRandomRotation()
    {
        return Random.Range(0, MaxRotation);
    }

    protected override System.Func<float>[] GetRandomFunctions()
    {
        return new System.Func<float>[] { GetRandomPower, GetRandomRotation };
    }

    protected override void InitializeTest()
    {        
        scenarios = new List<IScenario>();
        for (int i = 0; i < PoolCount; i++)
        {
            float z = (i / boxesInARow) * boxDistance;
            Vector3 boxposition = new Vector3((i % boxesInARow) * boxDistance, 0, z);
            var box = Instantiate(boxPrefab, boxposition, Quaternion.identity, null);
            var scenarioGoal = Instantiate(goalPrefab);
            var scenarioTurret = Instantiate(turretParametersPrefab);
            scenarioGoal.transform.SetParent(box);
            scenarioTurret.transform.SetParent(box);
            scenarioGoal.transform.localPosition = goalPosition;
            scenarioTurret.transform.localPosition = Vector3.zero;

            float[] scenarioParameters = new float[] { GetRandomPower(), GetRandomRotation() };
            SimpleTurretScenario scenario = new SimpleTurretScenario();
            scenario.Construct(scenarioTurret, scenarioGoal, scenarioParameters);
            scenarios.Add(scenario);
        }
    }
}
