﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;

    public Image[] HeartUI;

    private PlayerController player;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
       
    }

    void Update()
    {
        /*
        HeartUI.sprite = HeartSprites[player.curHealth];
        */
        for(int i = 0; i<HeartUI.Length; i++)
        {
            HeartUI[i].sprite = HeartSprites[player.curSprite];
        }
            
       
    }
}
