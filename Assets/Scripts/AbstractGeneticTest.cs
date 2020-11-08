using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGeneticTest : MonoBehaviour
{
    [SerializeField]
    protected float PoolCount = 10;
    [SerializeField]
    private int iterationCount = 10;
    [SerializeField]
    protected float timeOutSeconds = 10;
    [SerializeField]
    private Transform bestMarker;
    [SerializeField]
    private float pauseBetweenIterations = 1f;
    [Range(0, 1)]
    [SerializeField]
    private float mutationChance = 0.1f;

    private int currentIteration = 0;
    protected List<IScenario> scenarios;

    public int CurrentIteration { get => currentIteration; private set => currentIteration = value; }

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

    protected float[] GetRandomFactors()
    {
        Func<float>[] randomFunctions = GetRandomFunctions();
        float[] randomFactors = new float[randomFunctions.Length];
        for (int i = 0; i < randomFunctions.Length; i++)
        {
            randomFactors[i] = randomFunctions[i]();
        }
        return randomFactors;
    }

    protected abstract void InitializeTest();

    private void ClearAllScenarios()
    {
        foreach (IScenario scenario in scenarios)
        {
            scenario.Clear();
        }
    }

    IEnumerator DoIterations()
    {
        for (CurrentIteration = 0; CurrentIteration < iterationCount; CurrentIteration++)
        {
            StartGeneration();

            yield return GenerationDoneCheck();

            SortResultsByScore();
            PlaceMarkerAtBestScore();
            DebugOutput();
            yield return new WaitForSeconds(pauseBetweenIterations);
            if (CurrentIteration < iterationCount - 1)
            {
                ClearAllScenarios();
                DoGenetics();
            }
        }
    }

    private void PlaceMarkerAtBestScore()
    {
        bestMarker.position = scenarios[0].GetTestedObject().position;
    }

    private void SortResultsByScore()
    {
        scenarios.Sort((scenarioA, scenarioB) => scenarioA.GetScore().CompareTo(scenarioB.GetScore()));
    }

    protected abstract IEnumerator GenerationDoneCheck();

    protected abstract void StartGeneration();

    private void DebugOutput()
    {
        string result = "";
        scenarios.ForEach(s => result += $" {s.GetScore()} ; ");
        Debug.Log($"Scores: {result}");
    }

    protected bool IsDone()
    {
        return scenarios.TrueForAll(item => item.IsDone());
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeTest();

        StartCoroutine(DoIterations());
    }
}
