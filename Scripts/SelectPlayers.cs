using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayers : MonoBehaviour
{   
    GameObject player1;
    GameObject player2;
    public bool PlayersReady {get; private set;}
    public void SelectPlayersButton(){
        if(transform.parent.GetChild(0).GetComponent<Text>().text == "SELECT PLAYER 1:"){
            player1 = GameObject.FindWithTag(name);
            player1.tag = "player1";
            player1.GetComponent<PlayerMovement>().specialKey = KeyCode.RightShift;
            transform.parent.GetChild(0).GetComponent<Text>().text = "SELECT PLAYER 2:";
            gameObject.SetActive(false);
        }
        else{
            player2 = GameObject.FindWithTag(name);
            player2.tag = "player2";
            player2.GetComponent<PlayerMovement>().specialKey = KeyCode.E;
            transform.parent.gameObject.SetActive(false);
            PlayersReady = true;
        }
    }
}
