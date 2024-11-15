using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invertControlsEffect : Effect
{
    public override void ActivateEffect(GameObject target){
        if(target.CompareTag("player1")){
            GameObject.FindWithTag("player2").GetComponent<PlayerMovement>().order = -1;
        }
        if(target.CompareTag("player2")){
            GameObject.FindWithTag("player1").GetComponent<PlayerMovement>().order = -1;
        }
        StartCoroutine(RevertAfterTime(5f));
    }
    protected override IEnumerator RevertAfterTime(float duration){
        yield return new WaitForSeconds(duration);
        if(target.CompareTag("player1")){
            GameObject.FindWithTag("player2").GetComponent<PlayerMovement>().order = 1;
        }
        if(target.CompareTag("player2")){
            GameObject.FindWithTag("player1").GetComponent<PlayerMovement>().order = 1;
        }
        yield return new WaitForSeconds(duration * 2);
        GetComponent<Renderer>().material = originalMaterial;
        isActivated = false;
    }

}
