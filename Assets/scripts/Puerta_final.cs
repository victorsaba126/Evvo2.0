using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_final : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void puertaFinal()
    {
        Debug.Log("Entro");
        animator.SetTrigger("Final");
    }
}
