using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager eManager;
    public static EnvironmentManager Singleton
    {
        get
        {
            if (eManager == null)
                eManager = FindObjectOfType<EnvironmentManager>();
            return eManager;
        }
    }


    public GameObject[] environmentType;

    private bool canSpawnEnvironment;
    public Transform spawnerTransform;
    public static int randEnvironmentType;

    private void Start()
    {
        spawnerTransform.position = new Vector3(-0.11f, 10.2f, 218);
        canSpawnEnvironment = false;
    }

    void SpawnEnvironment()
    {
        Instantiate(environmentType[randEnvironmentType], spawnerTransform.position, spawnerTransform.rotation);

        canSpawnEnvironment = false;
    }

    private void Update()
    {
        if (canSpawnEnvironment)
        {
            SpawnEnvironment();
        }
    }

    public void AskForSpawnEnvironment()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        randEnvironmentType = Random.Range(0, environmentType.Length);

        spawnerTransform.position += new Vector3(0, 0, 100);
        canSpawnEnvironment = true;
    }
}