using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsScenesCreator : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField]
    private int sceneCount = 1;

    [SerializeField]
    private int sceneIndex;

    [SerializeField]
    private GameObject prefabToPlaceInScene;
    
    private List<PhysicsScene2D> physicsScenes = new List<PhysicsScene2D>();
    

    // Start is called before the first frame update
    private void Start()
    {
        Physics.autoSimulation = false;

        //create x scenes
        for (int i = 0; i < sceneCount; i++)
        {
            LoadSceneParameters parameters =
                new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics2D);
            Scene loadedScene = SceneManager.LoadScene(sceneIndex, parameters);
            PhysicsScene2D physicsScene = loadedScene.GetPhysicsScene2D();
            physicsScenes.Add(physicsScene);
            
            GameObject go = Instantiate(prefabToPlaceInScene, Random.onUnitSphere, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(go,loadedScene);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (PhysicsScene2D physicsScene in physicsScenes)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
        }
    }
}