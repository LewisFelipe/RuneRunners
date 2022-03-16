using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static bool canTestDraw;
    public float threshold;
    SwipeTrail st;

    private void Start()
    {
        canTestDraw = false;
        st = gameObject.GetComponent<SwipeTrail>();
    }

    private void Update()
    {
        if(canTestDraw)
        {
            canTestDraw = false;
            TestDraw();
        }
    }

    void TestDraw()
    {
        if ((Mathf.Abs(st.startPos.y) >= Mathf.Abs(st.endPos.y) 
            && Mathf.Abs(st.startPos.y) < Mathf.Abs(st.endPos.y) + threshold 
            || Mathf.Abs(st.startPos.y) <= Mathf.Abs(st.endPos.y) 
            && Mathf.Abs(st.startPos.y) > Mathf.Abs(st.endPos.y) - threshold)
            && ((Mathf.Abs(st.startPos.x) - Mathf.Abs(st.endPos.x) > threshold
            && !(Mathf.Abs(st.startPos.x) - Mathf.Abs(st.endPos.x) <= 0))
            || Mathf.Abs(st.startPos.x) - Mathf.Abs(st.endPos.x) < threshold
            && !(Mathf.Abs(st.startPos.x) - Mathf.Abs(st.endPos.x) >= 0)))
        {
            if (GameManager.obstacleTypeIndexer[0])
            {
                GameManager.obstacleTypeIndexer[0] = false;
                HealthSystem.destroyedObstacle = true;
                AudioManager.Singleton.PlaySoundEffect(1);
                StartCoroutine(CameraShake.Singleton.Shake(GameObject.FindGameObjectWithTag("MainCameraPosition").transform, 0.15f, 0.1f, 0.05f));
                GameManager.Singleton.DeleteObstacle();
            }
        }
        else 
        if((Mathf.Abs(st.startPos.x) >= Mathf.Abs(st.endPos.x)
            && Mathf.Abs(st.startPos.x) < Mathf.Abs(st.endPos.x) + threshold
            || Mathf.Abs(st.startPos.x) <= Mathf.Abs(st.endPos.x)
            && Mathf.Abs(st.startPos.x) > Mathf.Abs(st.endPos.x) - threshold)
            && ((Mathf.Abs(st.startPos.y) - Mathf.Abs(st.endPos.y) > threshold
            && !(Mathf.Abs(st.startPos.y) - Mathf.Abs(st.endPos.y) <= 0))
            || Mathf.Abs(st.startPos.y) - Mathf.Abs(st.endPos.y) < threshold
            && !(Mathf.Abs(st.startPos.y) - Mathf.Abs(st.endPos.y) >= 0)))
        {
            if (GameManager.obstacleTypeIndexer[1])
            {
                GameManager.obstacleTypeIndexer[1] = false;
                HealthSystem.destroyedObstacle = true;
                AudioManager.Singleton.PlaySoundEffect(1);
                StartCoroutine(CameraShake.Singleton.Shake(GameObject.FindGameObjectWithTag("MainCameraPosition").transform, 0.15f, 0.05f, 0.1f));
                GameManager.Singleton.DeleteObstacle();
            }
        }
    }
}