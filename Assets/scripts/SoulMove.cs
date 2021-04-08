using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMove : MonoBehaviour
{

    Soul soulScript;
    

    public float radius = 5f;
    private Transform playerTransform;
    private GameObject player_move;

    // Start is called before the first frame update

    void Start()
    {
        soulScript = gameObject.GetComponent<Soul>();
        player_move = GameObject.Find("Player");
        playerTransform = player_move.transform;
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= radius)
        {
            transform.position = Vector3.MoveTowards(transform.position, soulScript.playerTransform.position,
            soulScript.moveSpeed * Time.deltaTime);
        }
       
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player Bubble")
        {
            Destroy(gameObject);
        }
    }
}
