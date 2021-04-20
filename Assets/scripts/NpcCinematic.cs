using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCinematic : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public float range;
    private bool isPlayerHere, pressF = false;
    public GameObject cinematic;
    public GameObject freelook;
    public GameObject sprite;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       isPlayerHere=  Physics.CheckSphere(transform.position, range, whatIsPlayer);

        if (isPlayerHere)
        {
            if (!pressF)
            {
                sprite.SetActive(true);
            }
          
            if (Input.GetKey("f"))
            {
                sprite.SetActive(false);
                freelook.SetActive(false);
                cinematic.SetActive(true);
                pressF = true;
            }
            
            
        }
        else
        {
            sprite.SetActive(false);
            freelook.SetActive(true);
            cinematic.SetActive(false);
            pressF = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
       

    }
}
