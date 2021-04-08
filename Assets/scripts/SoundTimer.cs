using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTimer : MonoBehaviour
{

    public float soundTimer;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.pitch = (Random.Range(0.7f, 1.3f));
        Destroy(gameObject, soundTimer);
    }
}
