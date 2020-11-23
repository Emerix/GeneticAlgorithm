using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneticSingleTestBase<P, S> : AbstractGeneticTest where S : IScenario, new() where P : ParametersBase
{
    [SerializeField] protected P parameterPrefab;

    [SerializeField] private Transform goal;
    [SerializeField] private Transform start;
    private Transform currentTestedObject;

    protected override IEnumerator GenerationDoneCheck()
    {
        while (IsDone() == false && CurrentSlot < scenarios.Count)
        {
            CurrentTime = timeOutSeconds;
            while (scenarios[CurrentSlot].IsDone() == false && CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
                yield return null;
            }

            scenarios[CurrentSlot].SaveScore();
            Debug.Log($"Reached score: {scenarios[CurrentSlot].GetScore()}");
            KillSnake();
            CurrentSlot++;
            StartSnake();
        }
    }

    private void StartSnake()
    {
        if (CurrentSlot < scenarios.Count)
        {
            scenarios[CurrentSlot].Proceed();
            currentTestedObject = scenarios[CurrentSlot].GetTestedObject();
            currentTestedObject.transform.position = start.position;
        }
    }

    private void KillSnake()
    {
        currentTestedObject.gameObject.SetActive(false);
        currentTestedObject.GetComponent<SnakeParameters>()?.Reset();
    }

    protected override void InitializeTest()
    {
        scenarios = new List<IScenario>();
        for (int i = 0; i < PoolCount; i++)
        {
            P parameterObject = CreateParameterObject();
            float[] scenarioParameters = GetRandomFactors();
            S scenario = new S();
            scenario.Construct(parameterObject, goal, scenarioParameters);
            scenarios.Add(scenario);
        }
    }

    private P CreateParameterObject()
    {
        P snake = Instantiate<P>(parameterPrefab);
        snake.transform.position = start.position;
        snake.gameObject.SetActive(false);
        return snake;
    }

    protected override void StartGeneration()
    {
        CurrentSlot = 0;
        scenarios[CurrentSlot].Proceed();
        currentTestedObject = scenarios[CurrentSlot].GetTestedObject();
    }

    public float CurrentTime { get; private set; }
    public int CurrentSlot { get; private set; }
}