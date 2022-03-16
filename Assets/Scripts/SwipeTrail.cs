using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
    ParticleSystem particles;

    TrailRenderer trail;

    Camera camera;

    bool doOnce = false, doOnce1 = false; //Programação fuleira q precisaria ser refeita após conseguir fazer funcionar

    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public Vector3 endPos;
    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("DrawCamera").GetComponent<Camera>();
        trail = gameObject.GetComponent<TrailRenderer>();
        particles = gameObject.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)) && !Menu.isPaused)
        {
            Plane objPlane = new Plane(camera.transform.forward * -1, transform.position);

            Ray mRay = camera.ScreenPointToRay(Input.mousePosition);
            float rayDistance;

            if (objPlane.Raycast(mRay, out rayDistance))
            {
                transform.position = mRay.GetPoint(rayDistance);
            }

            if (!doOnce)
            {
                trail.Clear();
                particles.Clear();
                AudioManager.Singleton.PlaySoundEffectOnLoop(3);
                startPos = transform.position;
                doOnce1 = true;
                particles.Play();
            }
            if (transform.position == mRay.GetPoint(rayDistance))
            {
                doOnce = true;
            }
        }
        else if (Input.touchCount == 0 || Input.GetKeyUp(0))
        {
            AudioManager.Singleton.StopSoundEffect();
            if (doOnce1)
            {
                doOnce1 = false;
                endPos = transform.position;
                DrawManager.canTestDraw = true;
            }
            trail.Clear();
            particles.Clear();
            doOnce = false;
        }
    }
}