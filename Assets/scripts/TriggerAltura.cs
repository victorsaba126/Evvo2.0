using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAltura : MonoBehaviour
{
    public GameObject altura;
    private float counterTime;
    private float coldown = 2f;
    private bool coldown2 = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coldown2 == true)
        {
            counterTime += Time.deltaTime;
            if (counterTime >= coldown)
            {
                altura.SetActive(false);
                counterTime = 0;
                coldown2 = false;
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            altura.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            coldown2 = true;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            altura.SetActive(true);

        }
    }

}
