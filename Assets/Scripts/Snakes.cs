using System;
using UnityEngine;

public class Snakes : GeneticSingleTestBase<SnakeParameters, SnakeScenario>
{
    [SerializeField] private int MinSinusMultiplier = 10;
    [SerializeField] private int MaxSinusMultiplier = 100;
    [SerializeField] private int MaxOffset = 100;
    [SerializeField] private int MinMotorPower = 100;
    [SerializeField] private int MaxMotorPower = 2000;
    private Transform currentSnake;
    private int snakeJoints = 4;

    protected override Func<float>[] GetRandomFunctions()
    {
        int parameterPerJoint = 3;
        Func<float>[] functions = new Func<float>[snakeJoints * parameterPerJoint];
        for (int joint = 0; joint < snakeJoints; joint++)
        {
            functions[joint * parameterPerJoint + 0] = GetRandomMotorPower;
            functions[joint * parameterPerJoint + 1] = GetRandomOffset;
            functions[joint * parameterPerJoint + 2] = GetRandomSpeed;
        }

        return functions;
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