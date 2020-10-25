using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    [SerializeField]
    private float MaxPower = 100;
    [SerializeField]
    private float MaxRotation = 360;
    [SerializeField]
    private Turret turretPrefab;
    [SerializeField]
    private Transform goalPrefab;
    [SerializeField]
    private Transform boxPrefab
;
    [SerializeField]
    private int iterationCount = 10;
    [SerializeField]
    private float timeOutSeconds = 10;
    [SerializeField]
    private float PoolCount = 10;

    [SerializeField]
    private float boxDistance = 11;

    [SerializeField]
    private Vector3 goalPosition = new Vector3(0, 0, 4);

    [SerializeField]
    private Transform bestMarker;
    [SerializeField]
    private float pauseBetweenIterations = 1f;
    [Range(0, 1)]
    [SerializeField]
    private float mutationChance = 0.1f;

    private int currentIteration = 0;

    List<Scenario> scenarios;

    // Start is called before the first frame update
    void Start()
    {
        scenarios = new List<Scenario>();
        for (int i = 0; i < PoolCount; i++)
        {
            Vector3 boxposition = new Vector3(i * boxDistance, 0, 0);
            var box = Instantiate(boxPrefab, boxposition, Quaternion.identity, null);
            var scenarioGoal = Instantiate(goalPrefab);
            var scenarioTurret = Instantiate(turretPrefab);
            scenarioGoal.transform.SetParent(box);
            scenarioTurret.transform.SetParent(box);
            scenarioGoal.transform.localPosition = goalPosition;
            scenarioTurret.transform.localPosition = Vector3.zero;

            float[] scenarioParameters = new float[] { GetRandomPower(), GetRandomRotation() };
            Scenario scenario = new Scenario(scenarioGoal, scenarioTurret, scenarioParameters);
            scenarios.Add(scenario);
        }

        StartCoroutine(DoIterations());
    }


    IEnumerator DoIterations()
    {
        for (currentIteration = 0; currentIteration < iterationCount; currentIteration++)
        {
            scenarios.ForEach(scenario => scenario.Proceed());
            float currentTime = timeOutSeconds;
            while (IsDone() == false && currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                yield return null;
            }
            scenarios.Sort((scenarioA, scenarioB) => scenarioA.GetScore().CompareTo(scenarioB.GetScore()));
            bestMarker.position = scenarios[0].ScenarioTurret.transform.position;
            string result = "";
            scenarios.ForEach(s => result += $" {s.GetScore()} ; ");
            Debug.Log($"Scores: {result}");
            yield return new WaitForSeconds(pauseBetweenIterations);
            if (currentIteration < iterationCount - 1)
            {
                ClearAllScenarios();
                DoGenetics();
            }
        }
    }

    private void ClearAllScenarios()
    {
        foreach (Scenario scenario in scenarios)
        {
            scenario.Clear();
        }
    }

    private void DoGenetics()
    {
        // leave best 2
        int leaveBest = 2;
        var firstArray = scenarios[0].Parameters;
        var secondArray = scenarios[1].Parameters;
        for (int i = leaveBest; i < PoolCount; i++)
        {
            // do crossover and stuff
            var newValues = Genetics.DoCrossOver(firstArray, secondArray);
            // do mutation
            newValues = Genetics.DoMutation(newValues, mutationChance, new System.Func<float>[]{GetRandomPower,GetRandomRotation});
            // do next iteration
            scenarios[i].InitValues(newValues);
        }
    }

    private bool IsDone()
    {
        return scenarios.TrueForAll(item => item.IsDone());
    }

    private float GetRandomPower()
    {
        return Random.Range(0, MaxPower);
    }
    private float GetRandomRotation()
    {
        return Random.Range(0, MaxRotation);
    }
}
