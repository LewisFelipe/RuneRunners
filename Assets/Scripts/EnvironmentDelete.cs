using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDelete : MonoBehaviour
{
    private Transform cameraTransform;
    private const float OFFSET = -50;

    private void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    void CheckIfPassedPlayer()
    {
        if(gameObject.transform.position.z <= (cameraTransform.position.z + OFFSET))
        {
            EnvironmentManager.Singleton.AskForSpawnEnvironment();

            Destroy(gameObject);
        }
    }
    private void Update()
    {
        CheckIfPassedPlayer();
    }
}
