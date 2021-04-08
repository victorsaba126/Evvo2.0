using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_move : MonoBehaviour
{
    // Start is called before the first frame update
    public float mSpeed;
    private GameObject playerObj = null;

    void Start()
    {
        if (playerObj == null)
            playerObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(mSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, mSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
