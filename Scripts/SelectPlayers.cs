using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayers : MonoBehaviour
{
    public bool PlayersReady {get; private set;}
    public void SelectPlayersButton(){
        if(transform.parent.GetChild(0).GetComponent<Text>().text == "SELECT PLAYER 1:"){
            GameObject.FindWithTag(name).tag = "player1";
            transform.parent.GetChild(0).GetComponent<Text>().text = "SELECT PLAYER 2:";
            gameObject.SetActive(false);
        }
        else{
            GameObject.FindWithTag(name).tag = "player2";
            transform.parent.gameObject.SetActive(false);
            PlayersReady = true;
        }
    }
}
