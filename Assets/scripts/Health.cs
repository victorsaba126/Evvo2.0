using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sound;
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            player.SumarVida();
            if (player.consumir == true)
            {
                sound.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
