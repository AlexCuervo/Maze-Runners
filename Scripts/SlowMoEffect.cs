using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoEffect : Effect
{

    public override void ActivateEffect(GameObject target){
        target.GetComponent<PlayerMovement>().speed = 1f;
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
