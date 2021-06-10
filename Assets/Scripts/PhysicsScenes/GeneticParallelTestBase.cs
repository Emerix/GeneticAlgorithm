using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsScenes
{
    public abstract class GeneticParallelTestBase<P, S> : AbstractGeneticTest
        where S : IScenario, new() where P : ParametersBase
    {
        [SerializeField] protected P parameterPrefab;

        [SerializeField] private Transform goal;
        [SerializeField] private Transform start;
        [SerializeField] private int sceneIndex = 1;
        public float CurrentTime { get; private set; }

        private List<Transform> currentTestedObjects = new List<Transform>();
        private List<PhysicsScene2D> physicsScenes = new List<PhysicsScene2D>();

        protected override IEnumerator GenerationDoneCheck()
        {
            while (IsDone() == false)
            {
                CurrentTime = timeOutSeconds;
                while (IsDone() == false && CurrentTime > 0)
                {
                    CurrentTime -= Time.deltaTime;
                    Transform best = FindBest();
                    SnakeCameraFollower.instance.SetTarget(best);
                    yield return null;
                }

                foreach (IScenario scenario in scenarios)
                {
                    scenario.SaveScore();
                    Debug.Log($"Reached score: {scenario.GetScore()}");
                }

                KillTestedObject();
            }
        }

        protected override void StartGeneration()
        {
            for (var index = 0; index < scenarios.Count; index++)
            {
                IScenario scenario = scenarios[index];
                scenario.Proceed();
                currentTestedObjects[index] = scenario.GetTestedObject();
            }
        }

        protected override void InitializeTest()
        {
            scenarios = new List<IScenario>();
            currentTestedObjects = new List<Transform>();
            for (int i = 0; i < PoolCount; i++)
            {
                P parameterObject = CreateParameterObject();
                float[] scenarioParameters = GetRandomFactors();
                S scenario = new S();
                scenario.Construct(parameterObject, goal, scenarioParameters);
                scenarios.Add(scenario);
                currentTestedObjects.Add(parameterObject.transform);

                LoadSceneParameters parameters =
                    new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics2D);
                Scene loadedScene = SceneManager.LoadScene(sceneIndex, parameters);
                PhysicsScene2D physicsScene = loadedScene.GetPhysicsScene2D();
                physicsScenes.Add(physicsScene);
                SceneManager.MoveGameObjectToScene(parameterObject.gameObject, loadedScene);
            }
        }

        private void FixedUpdate()
        {
            foreach (PhysicsScene2D physicsScene2D in physicsScenes)
            {
                physicsScene2D.Simulate(Time.fixedDeltaTime);
            }
        }

        private P CreateParameterObject()
        {
            P testedObject = Instantiate(parameterPrefab);
            testedObject.transform.position = start.position;
            testedObject.gameObject.SetActive(false);
            return testedObject;
        }

        private Transform FindBest()
        {
            IScenario best = scenarios.OrderBy(scenario => scenario.GetCurrentScore()).First();
            return best.GetTestedObject().GetChild(0);
        }

        private void KillTestedObject()
        {
            foreach (Transform currentTestedObject in currentTestedObjects)
            {
                currentTestedObject.gameObject.SetActive(false);
                currentTestedObject.GetComponent<P>()?.Reset();
            }
        }
    }
}