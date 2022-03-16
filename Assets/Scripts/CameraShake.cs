using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake cManager;
    public static CameraShake Singleton
    {
        get
        {
            if (cManager == null)
                cManager = FindObjectOfType<CameraShake>();
            return cManager;
        }
    }

    public IEnumerator Shake(Transform mainCameraPosition, float shakeTime, float shakeX, float shakeY)
    {
        float elapsedTime = 0;

        while (elapsedTime < shakeTime)
        {
            float x = Random.Range(-shakeX, shakeX);
            float y = Random.Range(-shakeY, shakeY);

            Camera.main.transform.position += new Vector3(x, y, 0);

            elapsedTime += Time.deltaTime;

            yield return null; //espera um frame
        }

        Camera.main.transform.position = mainCameraPosition.position;
    }
}
