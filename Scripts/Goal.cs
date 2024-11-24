using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool contactWithPlayer;
    public GameObject winner;
    void Start()
    {
        contactWithPlayer = false;
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("player1") || other.CompareTag("player2")){
            contactWithPlayer = true;
            winner = other.gameObject;
        }
    }
    void Update()
    {
        
    }
}
