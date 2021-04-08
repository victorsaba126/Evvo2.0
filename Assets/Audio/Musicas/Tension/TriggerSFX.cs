using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{

    public GameObject tension;
    public AudioSource MusicEnviroment;
  
    void OnTriggerEnter(Collider other)
    {
        MusicEnviroment.Stop();
        tension.SetActive(true);
    }
    public void SemibossDead()
    {
        tension.SetActive(false);
        MusicEnviroment.Play();
        this.gameObject.SetActive(false);

    }

    
}
