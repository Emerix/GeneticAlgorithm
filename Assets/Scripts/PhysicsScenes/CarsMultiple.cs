using System;
using Cars;
using PhysicsScenes;
using UnityEngine;

public class CarsMultiple : GeneticParallelTestBase<CarParameters, CarScenario>, ICarsData
{
    public const int WHEEL_COUNT = 2;
    public const int PARAMETERS_PER_WHEEL = 3;
    
    public const int PARAMETERS_PER_BOX = 3;

    [Range(0.5f,5)]
    [SerializeField] private float maxBoxSizeX = 5;
    [Range(0.5f,2)]
    [SerializeField] private float maxBoxSizeY = 5;
    [Range(0.1f, 5f)]
    [SerializeField] private float maxWheelSize = 5;
    [Range(0.1f, 20f)]
    [SerializeField] private float maxSuspensionFrequency = 5;

    // Start is called before the first frame update
    protected override Func<float>[] GetRandomFunctions()
    {
        Func<float>[] functions = new Func<float>[PARAMETERS_PER_BOX + WHEEL_COUNT * PARAMETERS_PER_WHEEL];
        functions[0] = GetRandomBoxSizeX;
        functions[1] = GetRandomBoxSizeY;
        functions[2] = GetRandomSuspensionFrequency;
        
        for (int wheel = 0; wheel < WHEEL_COUNT; wheel++)
        {
            int index = wheel * PARAMETERS_PER_WHEEL + PARAMETERS_PER_BOX;
            functions[index] = GetRandomAnchorValue;
            functions[index + 1] = GetRandomAnchorValue;
            functions[index + 2] = GetRandomWheelSize;
        }
        return functions;
    }

    private float GetRandomBoxSizeX()
    {
        return UnityEngine.Random.Range(0.5f, maxBoxSizeX);
    }

    private float GetRandomBoxSizeY()
    {
        return UnityEngine.Random.Range(0.5f, maxBoxSizeY);
    }

    private float GetRandomSuspensionFrequency()
    {
        return UnityEngine.Random.Range(0, maxSuspensionFrequency);
    }

    private float GetRandomWheelSize()
    {
        return UnityEngine.Random.Range(0, maxWheelSize);
    }

    private float GetRandomAnchorValue()
    {
        return UnityEngine.Random.Range(-0.5f, 0.5f);
    }

    public int CurrentSlot { get; }
}