using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHability : MonoBehaviour
{
    public bool isActivated = false;

    void Start()
    {
        
    }
    void Hability(){
        isActivated = true;
        GetComponent<PlayerMovement>().isVulnerable = false;
        GetComponent<PlayerMovement>().speed += 1;

        StartCoroutine(StopHabilityOnTime(5f));
        StartCoroutine(UnlockHability(15f));

    }

    IEnumerator StopHabilityOnTime(float seconds){
        yield return new WaitForSeconds(seconds);
        GetComponent<PlayerMovement>().isVulnerable = true;
        GetComponent<PlayerMovement>().speed -= 1;
    }
    IEnumerator UnlockHability(float seconds){
        yield return new WaitForSeconds(seconds);
        isActivated = false;
    }
    void Update()
    {
        if(!isActivated && Input.GetKeyDown(GetComponent<PlayerMovement>().specialKey)){
            Hability();
        }
    }
}
