using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effect : MonoBehaviour
{
    protected bool isActivated = false;
    public virtual void ActivateEffect(GameObject target){
        Debug.Log($"{target.tag} ha activado un efecto");
        isActivated = true;
    }


    void OnTriggerEnter(Collider other){
       if((other.CompareTag("player1") || other.CompareTag("player2")) && !isActivated){
            ActivateEffect(other.gameObject);
       } 
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated){
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
