using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;

        

    }
}
