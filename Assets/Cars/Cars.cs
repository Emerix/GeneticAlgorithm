using System;
using System.Collections;
using UnityEngine;

public class Cars : GeneticSingleTestBase<CarParameters,CarScenario>
{
    [Range(0.1f,5)]
    [SerializeField] private float maxBoxSizeX = 5;
    [Range(0.1f,2)]
    [SerializeField] private float maxBoxSizeY = 5;
    [Range(0.1f, 5f)]
    [SerializeField] private float maxWheelSize = 5;
    // Start is called before the first frame update
    protected override Func<float>[] GetRandomFunctions()
    {
        throw new NotImplementedException();
    }

    protected override void InitializeTest()
    {
        CarParameters carParameters;
        
        
    }

    protected override IEnumerator GenerationDoneCheck()
    {
        throw new NotImplementedException();
    }

    protected override void StartGeneration()
    {
        throw new NotImplementedException();
    }
}