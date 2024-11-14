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
        target.GetComponent<PlayerMovement>().speed  += -2f;

        yield return new WaitForSeconds(duration);
        GetComponent<Renderer>().material = originalMaterial;
        isActivated = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
