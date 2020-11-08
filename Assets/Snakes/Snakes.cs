using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakes : AbstractGeneticTest
{

    [SerializeField]
    private Transform goal;
    [SerializeField]
    private Transform start;
    [SerializeField]
    private SnakeParameters snakePrefab;
    private Transform currentSnake;
    private int snakeJoints = 3;

    public float CurrentTime { get; private set; }
    public int CurrentSlot { get; private set; }

    protected override IEnumerator GenerationDoneCheck()
    {
        while (IsDone() == false)
        {
            CurrentTime = timeOutSeconds;
            while (scenarios[CurrentSlot].IsDone() == false && CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
                yield return null;
            }
            scenarios[CurrentSlot].SaveScore();
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
            currentSnake = scenarios[CurrentSlot].GetTestedObject();

        }
    }

    private void KillSnake()
    {
        Destroy(currentSnake.gameObject);
    }

    protected override Func<float>[] GetRandomFunctions()
    {
        return new System.Func<float>[] { GetRandomMotorPower, GetRandomOffset, GetRandomSpeed };
    }

    protected override void InitializeTest()
    {
        scenarios = new List<IScenario>();
        for (int i = 0; i < PoolCount; i++)
        {
            var snake = CreateSnake();
            float[] scenarioParameters = GetRandomSnakeScenario();
            SnakeScenario scenario = new SnakeScenario(snake, goal, scenarioParameters);
            scenarios.Add(scenario);
        }
    }

    private float[] GetRandomSnakeScenario()
    {
        Func<float>[] randomFunctions = GetRandomFunctions();
        float[] randomFactors = new float[randomFunctions.Length * snakeJoints];
        for (int joint = 0; joint < snakeJoints; joint++)
        {
            for (int i = 0; i < randomFunctions.Length; i++)
            {
                randomFactors[joint + i] = randomFunctions[i]();
            }
        }

        return randomFactors;
    }

    private SnakeParameters CreateSnake()
    {
        SnakeParameters snake = Instantiate(snakePrefab);
        snake.transform.position = start.position;
        snake.gameObject.SetActive(false);
        return snake;
    }

    protected override void StartGeneration()
    {
        scenarios[CurrentSlot].Proceed();
        currentSnake = scenarios[CurrentSlot].GetTestedObject();
    }

    private float GetRandomSpeed()
    {
        return UnityEngine.Random.Range(0, 100);
    }

    private float GetRandomOffset()
    {
        return UnityEngine.Random.Range(0, 100);
    }

    private float GetRandomMotorPower()
    {
        return UnityEngine.Random.Range(0, 2000);
    }
}
