using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Effect : MonoBehaviour
{
    protected GameObject target;
    protected bool isActivated = false;
    public Material disabledMaterial;
    public Material originalMaterial;
    public float speedPlayer1;
    public float speedPlayer2;

    protected  virtual IEnumerator RevertAfterTime(float duration){
        yield return new WaitForSeconds(duration);
        GetComponent<Renderer>().material = originalMaterial;
        isActivated = false;
    }
    public virtual void ActivateEffect(GameObject target){}

    void OnTriggerEnter(Collider other){
        if((other.CompareTag("player1") || other.CompareTag("player2")) && !isActivated){
            target = other.gameObject;
            ActivateEffect(other.gameObject);
            transform.GetComponent<Renderer>().material = disabledMaterial;
            isActivated = true;
        
        } 
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
