using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTurretGeneticTest : MonoBehaviour
{
    [SerializeField]
    private int iterationCount = 10;
    [SerializeField]
    private float timeOutSeconds = 10;
    [SerializeField]
    protected float PoolCount = 10;


    [SerializeField]
    private Transform bestMarker;
    [SerializeField]
    private float pauseBetweenIterations = 1f;
    [Range(0, 1)]
    [SerializeField]
    private float mutationChance = 0.1f;

    private int currentIteration = 0;

    protected List<IScenario> scenarios;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTest();

        StartCoroutine(DoIterations());
    }

    protected abstract void InitializeTest();

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
            bestMarker.position = scenarios[0].GetTurret().position;
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
        foreach (IScenario scenario in scenarios)
        {
            scenario.Clear();
        }
    }

    protected void DoGenetics()
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
            newValues = Genetics.DoMutation(newValues, mutationChance, GetRandomFunctions());
            // do next iteration
            scenarios[i].InitValues(newValues);
        }
    }

    protected abstract Func<float>[] GetRandomFunctions();

    private bool IsDone()
    {
        return scenarios.TrueForAll(item => item.IsDone());
    }
}
