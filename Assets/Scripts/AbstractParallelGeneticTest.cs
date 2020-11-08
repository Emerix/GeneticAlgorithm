using System.Collections;
using UnityEngine;

public abstract class AbstractParallelGeneticTest : AbstractGeneticTest
{
    protected override void StartGeneration()
    {
        scenarios.ForEach(scenario => scenario.Proceed());
    }

    protected override IEnumerator GenerationDoneCheck()
    {
        float currentTime = timeOutSeconds;
        while (IsDone() == false && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }
        foreach (IScenario scenario in scenarios)
        {
            scenario.SaveScore();
        }
    }
}
