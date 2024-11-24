using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : Effect
{
   
    public override void ActivateEffect(GameObject target){
        target.GetComponent<PlayerMovement>().speed  += 2f;
        StartCoroutine(RevertAfterTime(5f));
    }
    protected override IEnumerator RevertAfterTime(float duration){
        yield return new WaitForSeconds(duration);
        if(target.CompareTag("player1")) target.GetComponent<PlayerMovement>().speed  = speedPlayer1;
        else target.GetComponent<PlayerMovement>().speed  = speedPlayer2;

        yield return new WaitForSeconds(duration);
        GetComponent<Renderer>().material = originalMaterial;
        isActivated = false;
    }

    void Start()
    {
        speedPlayer1 = GameObject.FindWithTag("player1").GetComponent<PlayerMovement>().speed;
        speedPlayer2 = GameObject.FindWithTag("player2").GetComponent<PlayerMovement>().speed;
    }

    void Update()
    {
        
    }
}
