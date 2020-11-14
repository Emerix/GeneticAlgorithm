using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakes : AbstractGeneticTest
{
    [SerializeField]
    private int MinSinusMultiplier = 10;
    [SerializeField]
    private int MaxSinusMultiplier = 100;
    [SerializeField]
    private int MaxOffset = 100;
    [SerializeField]
    private int MinMotorPower = 100;
    [SerializeField]
    private int MaxMotorPower = 2000;

    [SerializeField]
    private Transform goal;
    [SerializeField]
    private Transform start;
    [SerializeField]
    private SnakeParameters snakePrefab;
    [SerializeField]
    public float minMovementInTimeframeToPreventTimeout = 0.4f;
    [SerializeField]
    public float timeOutWithoutMovement = 2.0f;
    private Transform currentSnake;
    private int snakeJoints = 3;

    public float CurrentTime { get; private set; }
    public int CurrentSlot { get; private set; }

    public static Snakes instance;

    protected override void Start()
    {
        instance = this;
        base.Start();
    }

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
            currentSnake = scenarios[CurrentSlot].GetTestedObject();
            currentSnake.transform.position = start.position;
        }
    }

    private void KillSnake()
    {
        currentSnake.gameObject.SetActive(false);
        currentSnake.GetComponent<SnakeParameters>()?.Reset();
    }

    protected override Func<float>[] GetRandomFunctions()
    {
        int parameterPerJoint = 3;
        System.Func<float>[] functions = new System.Func<float>[snakeJoints * parameterPerJoint];
        for (int joint = 0; joint < snakeJoints; joint++)
        {
            functions[joint * parameterPerJoint + 0] = GetRandomMotorPower;
            functions[joint * parameterPerJoint + 1] = GetRandomOffset;
            functions[joint * parameterPerJoint + 2] = GetRandomSpeed;
        }
        return functions;
    }

    protected override void InitializeTest()
    {
        scenarios = new List<IScenario>();
        for (int i = 0; i < PoolCount; i++)
        {
            var snake = CreateSnake();
            float[] scenarioParameters = GetRandomFactors();
            SnakeScenario scenario = new SnakeScenario(snake, goal, scenarioParameters);
            scenarios.Add(scenario);
        }
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
        CurrentSlot = 0;
        scenarios[CurrentSlot].Proceed();
        currentSnake = scenarios[CurrentSlot].GetTestedObject();
    }

    private float GetRandomSpeed()
    {
        return UnityEngine.Random.Range(MinSinusMultiplier, MaxSinusMultiplier);
    }

    private float GetRandomOffset()
    {
        return UnityEngine.Random.Range(0, MaxOffset);
    }

    private float GetRandomMotorPower()
    {
        return UnityEngine.Random.Range(MinMotorPower, MaxMotorPower);
    }
}
