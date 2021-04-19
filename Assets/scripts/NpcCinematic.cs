using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCinematic : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public float range;
    private bool isPlayerHere;
    public GameObject cinematic;
    public GameObject freelook;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       isPlayerHere=  Physics.CheckSphere(transform.position, range, whatIsPlayer);

        if (isPlayerHere)
        {
            freelook.SetActive(false);
            cinematic.SetActive(true);
        }
        else
        {
            freelook.SetActive(true);
            cinematic.SetActive(false);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
       

    }
}
