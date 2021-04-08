using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing_move : MonoBehaviour
{
    [SerializeField] Transform origin;
    [SerializeField] Transform destination;
    [SerializeField] float time;
    [SerializeField] AnimationCurve curve;
    public GameObject Player;

    float timespeed = 1f;
    float currentTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        while (currentTime > time)
        {
            currentTime -= time;
        }
        float progress = currentTime / time;
        float curvedProgress = curve.Evaluate(progress);
        transform.position = Vector3.Lerp(origin.position, destination.position, curvedProgress);


    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

}
