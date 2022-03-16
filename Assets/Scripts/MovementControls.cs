using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControls : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(!Menu.isPaused)
        gameObject.transform.position += new Vector3(0, 0, (GameManager.Singleton.velocity * Time.deltaTime));
    }
}