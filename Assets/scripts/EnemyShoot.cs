using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyShoot : MonoBehaviour
{

    public float speed;
    public float highRotation;

    private Vector3 target;
    private Vector3 rotation;

    private PlayerController evvo;
    private Rigidbody rb;


    void Start()
    {
        evvo = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();

        target = new Vector3(evvo.player.transform.position.x, evvo.player.transform.position.y +0.8f, evvo.player.transform.position.z-0.5f);
        rotation = new Vector3(evvo.player.transform.position.x, highRotation, evvo.player.transform.position.z);

        transform.LookAt(rotation);

    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Vector3 lookatposition = transform.position + rb.velocity;

        transform.LookAt(lookatposition);


        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Scene" || other.tag == "lose" || other.tag == "win")
        {
            
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    
}
