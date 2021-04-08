using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public float moveSpeed = 17f;

    SoulMove soulMoveScript;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        soulMoveScript = gameObject.GetComponent<SoulMove>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Soul Detector")
        {
            soulMoveScript.enabled = true;
        }
    }
}
